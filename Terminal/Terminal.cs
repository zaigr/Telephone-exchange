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
        private Phone _currentCallReciver;


        public Terminal(Phone phoneNumber, ITelephoneExchange exchange)
        {
            this.PhoneNumber = phoneNumber;
            this._telephoneExchange = exchange;
        }

        public CallState MakeCall(Phone reciverNumber)
        {
            var status = _telephoneExchange.ConnectAbonents(PhoneNumber, reciverNumber);

            if (status == CallState.Connected) {
                _currentCallReciver = reciverNumber;
            }
            
            return status;
        }

        public CallState CloseCall()
        {
            var status = _telephoneExchange.DisconnectAbonents(PhoneNumber, _currentCallReciver);

            if (status == CallState.Disconnected) {
                _currentCallReciver = null;
            }

            return status;
        }

        public bool ConnectToExchange()
        {
            return _telephoneExchange.ConnectToExchange(PhoneNumber);
        }

        public bool DisconnectFromExchange()
        {
            return _telephoneExchange.DisconnectFromExchange(PhoneNumber);
        }

        public string RunUSSD(string request)
        {
            throw new NotImplementedException();
        }
    }
}
