using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;

namespace ATS
{
    public class PortsConnectedEventArgs : EventArgs
    {
        public Phone SenderPhone { get; private set; }
        public IPort SenderPort { get; private set; }

        public Phone ReciverPhone { get; private set; }
        public IPort ReciverPort { get; private set; }

        public PortsConnectedEventArgs(Phone senderPhone, IPort senderPort, Phone reciverPhone, IPort reciverPort)
        {
            SenderPhone = senderPhone;
            SenderPort = senderPort;

            ReciverPhone = reciverPhone;
            ReciverPort = reciverPort;
        }
    }
}
