using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using ATS.Interfaces;

namespace ATS
{
    public class Terminal : ITerminal
    {
        public Phone PhoneNumber { get; private set; }

        private ITelephoneExchange _telephoneExchange;
        private Phone _currentCollocutor;

        private readonly int _callReceivingDelayMs;  // milliseconds
        private CancellationTokenSource _callReceivingDelayCancellator;

        public Terminal(Phone phoneNumber, ITelephoneExchange exchange, TimeSpan callReceivingDelay)
        {
            this.PhoneNumber = phoneNumber;
            this._telephoneExchange = exchange;
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
            var status = _telephoneExchange.DisconnectAbonents(PhoneNumber, _currentCollocutor);
            return status;
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
                _currentCollocutor = e.Reciver;
            }

            if (PhoneNumber == e.Reciver) {
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
            throw new NotImplementedException();
        }
    }
}
