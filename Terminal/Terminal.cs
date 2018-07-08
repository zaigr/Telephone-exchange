using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;

namespace ATS
{
    public class Terminal : ITerminal
    {
        public Phone PhoneNumber { get; private set; }

        private ITelephoneExchange _telephoneExchange;
        private Phone _currentCollocutor;


        public Terminal(Phone phoneNumber, ITelephoneExchange exchange)
        {
            this.PhoneNumber = phoneNumber;
            this._telephoneExchange = exchange;
        }

        public CallState MakeCall(Phone reciverNumber)
        {
            var status = _telephoneExchange.ConnectAbonents(PhoneNumber, reciverNumber);
            return status;
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

        private void ExchangeCallStartEventHandler(object sender, RingEventArgs e)
        {
            if (PhoneNumber == e.Sender)
            {
                _currentCollocutor = e.Reciver;
            }

            if (PhoneNumber == e.Reciver) {
                _currentCollocutor = e.Sender;
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
