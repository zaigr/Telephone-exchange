using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public class PortExchangeEventArgs : EventArgs
    {
        public int PortId { get; private set; }
        public Phone TerminalPhone { get; private set; }

        public PortExchangeEventArgs(int portId, Phone terminalPhone)
        {
            PortId = portId;
            TerminalPhone = terminalPhone;
        }
    }
}
