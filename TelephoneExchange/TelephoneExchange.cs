using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;


namespace ATS
{
    public class TelephoneExchange : ITelephoneExchange
    {
        private IDictionary<Phone, int> _mappedPorts;
        private ISet<RingEventArgs> _currentConnections;
        private ISet<Phone> _avaliblePhones;

        public TelephoneExchange(IEnumerable<Phone> avaliblePhones)
        {
            this._mappedPorts = new Dictionary<Phone, int>();
            this._currentConnections = new HashSet<RingEventArgs>();
            this._avaliblePhones = new HashSet<Phone>(avaliblePhones);
        }

        public event EventHandler<RingEventArgs> AbonentsConnected;
        public event EventHandler<RingEventArgs> AbonentsDisconnected;

        public CallState ConnectAbonents(RingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public CallState DisconnectAbonents(RingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public bool ConnectPortToExchange(PortExchangeEventArgs eventArgs)
        {
            if (!_avaliblePhones.Contains(eventArgs.TerminalPhone))
            {
                return false;
            }
                
            if (!_mappedPorts.ContainsKey(eventArgs.TerminalPhone))
            {
                return true;
            }

            _mappedPorts.Add(eventArgs.TerminalPhone, eventArgs.PortId);

            return true;
        }

        public bool DisconnectPortFromExchange(PortExchangeEventArgs eventArgs)
        {
            if (!_mappedPorts.ContainsKey(eventArgs.TerminalPhone))
            {
                return false;
            }

            return true;
        }
    }
}
