using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;

namespace ATS
{
    public class Port : IPort
    {
        public int Number { get; private set; }

        public event EventHandler<PortState> PortStateChanged;

        private PortState _portState;
        public PortState State 
        { 
            get => _portState; 
            set {
                _portState = value;
                OnPortStateChanget(value);
            }  
        }

        public Port(int number)
        {
            Number = number;
            _portState = PortState.Unused;
        }

        protected void OnPortStateChanget(PortState state)
        {
            if (PortStateChanged != null ) {
                PortStateChanged(this, state);
            }
        }

        public int CompareTo(IPort port)
        {
            return Number.CompareTo(port.Number);
        }

        public override bool Equals(object obj)
        {
            return Number.Equals((obj as Port)?.Number);
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
