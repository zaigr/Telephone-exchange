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

        public event Func<RingEventArgs, CallState> StartCalling;
        public event Func<RingEventArgs, CallState> CallRecieved;
        public event Func<RingEventArgs, CallState> CallClosed;

        public event Func<ITerminal, IPort> ConnectingToPort;
        public event Action<ITerminal, IPort> DisconnectingFromPort;

        private IUssdRunner _ussdRunner;
        private IPort _port;

        public Terminal(Phone phoneNumber, IUssdRunner ussdRunner)
        {
            PhoneNumber = phoneNumber;
            _ussdRunner = ussdRunner;
        }

        #region ITerminal

        public bool ConnectToPort()
        {
            _port = OnTryingConnectToPort();

            return _port != null;
        }

        public void DisconnectFromPort()
        {
            OnDisconnectFromPort();
        }

        public CallState MakeCall(Phone abonent)
        {
            return OnStartCalling(abonent);
        }

        public CallState RecieveCall(RingEventArgs ea)
        {
            throw new NotImplementedException();
        }

        public CallState CloseCall()
        {
            throw new NotImplementedException();
        }

        public CallState ConnectionEstablished(RingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public string RunUSSD(string request)
        {
            var response = _ussdRunner.RunCommand(request);

            return String.Join(Environment.NewLine, response);
        }

        #endregion ITerminal

        #region EventCall

        protected IPort OnTryingConnectToPort()
        {
            if (ConnectingToPort != null)
            {
                return ConnectingToPort(this);
            } 
            else
            {
                return null;
            }
        }

        protected void OnDisconnectFromPort()
        {
            if (DisconnectingFromPort != null)
            {
                DisconnectingFromPort(this, _port);
            }
        }

        protected CallState OnStartCalling(Phone abonent)
        {
            if (StartCalling != null)
            {
                var eventArgs = new RingEventArgs(PhoneNumber, abonent);
                return StartCalling(eventArgs);
            }
            else
            {
                return CallState.Disconnected;
            }
        }

        #endregion EventCall
    }
}
