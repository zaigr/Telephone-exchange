using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Interfaces
{
    public interface ITelephoneExchange
    {
        event EventHandler<RingEventArgs> AbonentsConnected;
        CallState ConnectAbonents(Phone sender, Phone reciver);

        event EventHandler<RingEventArgs> AbonentsDisconnected;
        CallState DisconnectAbonents(Phone sender, Phone reciver);

        bool ConnectToExchange(Phone phone);
        bool DisconnectFromExchange(Phone phone);

        // ITariffPlan TariffPlan { get; }
        // void Report();
    }
}
