using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;


namespace ATS
{
    public class TelephoneExchange : ITelephoneExchange
    {
        private ISet<IPort> _freePorts;
        private ISet<Phone> _avaliblePhones;
        private IDictionary<Phone, IPort> _mappedPorts;

        private Func<Phone, bool> CheckRingAvalible;

        public event EventHandler<RingEventArgs> AbonentsConnected;
        public event EventHandler<RingEventArgs> AbonentsDisconnected;

        public event EventHandler<PortsConnectedEventArgs> PortsConnected; 

        public TelephoneExchange(ISet<IPort> ports, ISet<Phone> phones, IExchangeBilling exchangeBilling)
        {
            this._freePorts = ports;
            this._avaliblePhones = phones;
            _mappedPorts = new Dictionary<Phone, IPort>();

            this.CheckRingAvalible = exchangeBilling.AbonentIsAvalible;
            this.AbonentsConnected += exchangeBilling.AbonentsConnectedEventHandler;
            this.AbonentsDisconnected += exchangeBilling.AbonentsDisconnectedEventHandler;
        }

        protected void OnAbonentsConnected(RingEventArgs e)
        {
            if (AbonentsConnected != null) {
                AbonentsConnected(this, e);
            }
        }

        protected void OnAbonentsDisconnected(RingEventArgs e)
        {
            if (AbonentsDisconnected != null) {
                AbonentsDisconnected(this, e);
            }
        }

        protected void OnPortsConnected(PortsConnectedEventArgs e)
        {
            if (PortsConnected != null) {
                PortsConnected(this, e);
            }
        }

        public bool AvalibleForServe(Phone sender, Phone reciver)
        {
            var selfConnection = sender == reciver;
            var phonesAvalible = _avaliblePhones.Contains(sender) && _avaliblePhones.Contains(reciver);
            var connectedToPorts = _mappedPorts.ContainsKey(sender) && _mappedPorts.ContainsKey(reciver);
            
            if (phonesAvalible && connectedToPorts && !selfConnection) {
                return true;
            }
            else {
                return false;
            }
        }

        public CallState ConnectAbonents(Phone sender, Phone reciver)
        {
            if (!AvalibleForServe(sender, reciver)) {
                return CallState.Error;
            }

            if (!CheckRingAvalible(sender)) {
                return CallState.Locked;
            }

            if (!CheckRingAvalible(reciver)) {
                return CallState.Engaget;
            }

            var callState = EstablishConnection(sender, reciver);
            if (callState == CallState.Connected) {
                OnAbonentsConnected(new RingEventArgs(sender, reciver));
            }
            
            return callState;
        }

        private CallState EstablishConnection(Phone sender, Phone reciver)
        {
            var reciverPort = _mappedPorts[reciver];
            var senderPort = _mappedPorts[sender];

            if (senderPort.State == PortState.Listened && reciverPort.State == PortState.Listened) {
                reciverPort.State = PortState.Connected;
                senderPort.State = PortState.Connected;

                OnPortsConnected(new PortsConnectedEventArgs(sender, senderPort, reciver, reciverPort));
                return CallState.Connected;
            } 
            else {
                return CallState.Engaget;
            }
        }


        public CallState DisconnectAbonents(Phone sender, Phone reciver)
        {
            if (!AvalibleForServe(sender, reciver)) {
                return CallState.Error;
            }

            var senderPort = _mappedPorts[sender];
            var reciverPort = _mappedPorts[reciver];

            if (senderPort.State == PortState.Connected && senderPort.State == PortState.Connected) {
                _mappedPorts[sender].State = PortState.Listened;
                _mappedPorts[reciver].State = PortState.Listened;

                OnAbonentsDisconnected(new RingEventArgs(sender, reciver));
                return CallState.Disconnected;
            }
            else {
                return CallState.Error;
            }
        }


    #region PortUsing

        public bool ConnectToExchange(Phone abonent)
        {
            if (!_avaliblePhones.Contains(abonent)) {
                return false;
            }

            if (!_mappedPorts.ContainsKey(abonent)) {
                var isMapped = MapToPort(abonent);
                return isMapped;
            } 
            else {
                return true;
            }
        }

        private bool MapToPort(Phone phone)
        {
            if (_freePorts.Count == 0) {
                return false;
            }

            var avaliblePort = _freePorts.First();
            _freePorts.Remove(avaliblePort);

            avaliblePort.State = PortState.Listened;
            _mappedPorts.Add(phone, avaliblePort);
            
            return true;
        }

        public bool DisconnectFromExchange(Phone phone)
        {
            if (!_mappedPorts.ContainsKey(phone)) {
                return false;
            }

            var mappedPort = _mappedPorts[phone];
            _mappedPorts.Remove(phone);

            mappedPort.State = PortState.Unused;
            _freePorts.Add(mappedPort);

            return true;
        }

    #endregion
    }
}
