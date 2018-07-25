using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public interface ITerminal
    {
        Phone PhoneNumber { get; }

        CallState MakeCall(Phone abonent);
        CallState RecieveCall();
        CallState CloseCall();

        event Func<RingEventArgs, CallState> StartCalling;
        event Func<CallState> CallRecieved;
        event Func<CallState> CallClosed;
        
        bool ConnectToPort();
        void DisconnectFromPort();

        event Func<ITerminal, IPort> ConnectingToPort;
        event Action<ITerminal, IPort> DisconnectingFromPort;

        string RunUSSD(string request);
    }
}
