using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public interface IPortConnectionFactory
    {
        IPort Connect(ITerminal terminal);
        void Disconnect(ITerminal terminal, IPort port);
    }
}
