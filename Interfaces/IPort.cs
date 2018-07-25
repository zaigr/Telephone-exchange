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

        event EventHandler<RingEventArgs> TryingEstablishIncoming;
        event EventHandler<RingEventArgs> TryingEstablishOutgoing;

        CallState OpenConnection(RingEventArgs ea);
    }
}
