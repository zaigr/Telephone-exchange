using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public class RingEventArgs : EventArgs
    {
        public Phone Sender { get; private set; }
        public Phone Reciver { get; private set; }

        public RingEventArgs(Phone sender, Phone reciver)
        {
            Sender = sender;
            Reciver = reciver;
        }
    }
}
