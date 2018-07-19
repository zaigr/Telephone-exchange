using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public interface IPort : IComparable<IPort>
    {
        PortState State { get; set; }
        event EventHandler<PortState> PortStateChanged;
        
        int Number { get; }
    }
}
