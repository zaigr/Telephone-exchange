using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;


namespace ATS
{
    public class Port : IPort, IDisposable
    {
        public int Id { get; private set; }

        public PortState State { get; set; }

        public event EventHandler<PortState> PortStateChanged;

        public event Func<RingEventArgs, CallState> ConnectionEstablished;
        public event Func<RingEventArgs, CallState> ConnectionReceived;

        public event Func<PortExchangeEventArgs, bool> ConnectingToExchange;
        public event Func<PortExchangeEventArgs, bool> DisconnectingFromExchange;

        public CallState OpenConnection(RingEventArgs ea)
        {
            return CallState.Connected;
        }

        public CallState CloseConnection(RingEventArgs ea)
        {
            return CallState.Disconnected;
        }

        public Port(int id)
        {
            Id = id;
            State = PortState.Unused;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Id.Equals(obj);
        }

        public void Dispose()
        {
            PortStateChanged = null;
            

            State = PortState.Unused;
        }
    }
}
