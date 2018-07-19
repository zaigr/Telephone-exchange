using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using ATS.Interfaces;
using ATS.Billing.Interfaces;

namespace ATS
{
    public class Terminal : ITerminal
    {
        public Phone PhoneNumber { get; private set; }

        private ITelephoneExchange _telephoneExchange;
        private IUssdRunner _ussdRunner;
        private Phone _currentCollocutor;
        private bool? _isReceiver;

        private readonly int _callReceivingDelayMs;  // milliseconds
        private CancellationTokenSource _callReceivingDelayCancellator;

        public Terminal(Phone phoneNumber, ITelephoneExchange exchange, IUssdRunner ussdRunner, TimeSpan callReceivingDelay)
        {
            this.PhoneNumber = phoneNumber;
            this._telephoneExchange = exchange;
            this._ussdRunner = ussdRunner;
            this._callReceivingDelayMs = (int)callReceivingDelay.TotalMilliseconds;

            this._callReceivingDelayCancellator = new CancellationTokenSource(); 
        }

        public CallState MakeCall(Phone reciverNumber)
        {
            var status = _telephoneExchange.ConnectAbonents(PhoneNumber, reciverNumber);
            return status;
        }

        public CallState ReceiveCall()
        {
            if (_currentCollocutor == null) {
                return CallState.Disconnected;
            }

            _callReceivingDelayCancellator.Cancel();
            return CallState.Connected;
        }

        public CallState CloseCall()
        {
            // To Exchange the sender and reciver roles are important during disconnection
            if (this._isReceiver == true) {
                return _telephoneExchange.DisconnectAbonents(_currentCollocutor, PhoneNumber);
            }
            else if (this._isReceiver == false) {
                return _telephoneExchange.DisconnectAbonents(PhoneNumber, _currentCollocutor);
            }
            else {
                return CallState.Error;
            }
        }


        public bool ConnectToExchange()
        {
            _telephoneExchange.AbonentsConnected += ExchangeCallStartEventHandler;
            _telephoneExchange.AbonentsDisconnected += ExchangeCallEndEventHandler;
            
            return _telephoneExchange.ConnectToExchange(PhoneNumber);
        }

        private async void ExchangeCallStartEventHandler(object sender, RingEventArgs e)
        {
            if (PhoneNumber == e.Sender) {
                this._isReceiver = false;
                _currentCollocutor = e.Reciver;
            }

            if (PhoneNumber == e.Reciver) {
                this._isReceiver = true;
                _currentCollocutor = e.Sender;

                //var awaiter = Task.Delay(_callReceivingDelayMs, _callReceivingDelayCancellator.Token).GetAwaiter();
                //awaiter.OnCompleted(() => this.CloseCall());

                await Task.Delay(_callReceivingDelayMs, _callReceivingDelayCancellator.Token)
                    .ContinueWith((task) =>
                    {
                        if (!task.IsCanceled) {
                            this.CloseCall();  // Disconnect if not received
                        }
                    });  
            }
        }

        private void ExchangeCallEndEventHandler(object sender, RingEventArgs e)
        {
            if (PhoneNumber == e.Sender || PhoneNumber == e.Reciver) {
                _currentCollocutor = null;
                this._isReceiver = false;
            }
        }


        public bool DisconnectFromExchange()
        {
            _telephoneExchange.AbonentsConnected -= ExchangeCallStartEventHandler;
            _telephoneExchange.AbonentsDisconnected -= ExchangeCallEndEventHandler;

            return _telephoneExchange.DisconnectFromExchange(PhoneNumber);
        }


        public string RunUSSD(string request)
        {
            var result = _ussdRunner.RunCommand(request);
            return String.Join(Environment.NewLine, result);
        }
    }
}
