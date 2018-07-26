using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public interface IPort : IDisposable
    {
        int Id { get; }

        PortState State { get; set; }
        event EventHandler<PortState> PortStateChanged;

        event Func<RingEventArgs, CallState> ConnectionEstablished;
        event Func<RingEventArgs, CallState> ConnectionReceived;

        event Func<PortExchangeEventArgs, bool> ConnectingToExchange;
        event Func<PortExchangeEventArgs, bool> DisconnectingFromExchange;

        CallState OpenConnection(RingEventArgs ea);
        CallState CloseConnection(RingEventArgs ea);
    }
}
