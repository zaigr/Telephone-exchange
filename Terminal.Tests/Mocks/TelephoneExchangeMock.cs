using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;

namespace ATS.Tests.Mocks
{
    public class TelephoneExchangeMock : ITelephoneExchange
    {
        public event EventHandler<RingEventArgs> AbonentsConnected;
        public event EventHandler<RingEventArgs> AbonentsDisconnected;

        public CallState ConnectAbonents(RingEventArgs eventArgs)
        {
            return CallState.Connected;
        }

        public bool ConnectPortToExchange(PortExchangeEventArgs eventArgs)
        {
            return true;
        }

        public CallState DisconnectAbonents(RingEventArgs eventArgs)
        {
            return CallState.Disconnected;
        }

        public bool DisconnectPortFromExchange(PortExchangeEventArgs eventArgs)
        {
            return true;
        }
    }
}
