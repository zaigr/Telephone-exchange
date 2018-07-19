using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;

namespace ATS.Billing.Interfaces
{
    public interface IExchangeBilling
    {
        void AbonentsConnectedEventHandler(object sender, RingEventArgs e);
        void AbonentsDisconnectedEventHandler(object sender, RingEventArgs e);

        bool AbonentIsAvalible(Phone abonent);
    }
}
