using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;

namespace ATS
{
    public class PortConnectionFactory : Interfaces.IPortConnectionFactory
    {
        private readonly ISet<IPort> _freePorts;

        public PortConnectionFactory(IEnumerable<IPort> ports)
        {
            _freePorts = new HashSet<IPort>(ports);
        }

        public IPort Connect(ITerminal terminal)
        {
            var freePort = _freePorts.FirstOrDefault();

            if (freePort == null)
            {
                return null;
            }

            _freePorts.Remove(freePort);

            terminal.StartCalling += freePort.OpenConnection;
            freePort.State = PortState.Listened;

            return freePort;
        }

        public void Disconnect(ITerminal terminal, IPort port)
        { 
            int portId = port.Id;
            port.Dispose();

            _freePorts.Add(port);
        }
    }
}
