using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public interface ITelephoneExchange
    {
        event EventHandler<RingEventArgs> AbonentsConnected;
        CallState ConnectAbonents(RingEventArgs eventArgs);

        event EventHandler<RingEventArgs> AbonentsDisconnected;
        CallState DisconnectAbonents(RingEventArgs eventArgs);

        bool ConnectPortToExchange(PortExchangeEventArgs eventArgs);
        bool DisconnectPortFromExchange(PortExchangeEventArgs eventArgs);
    }
}
