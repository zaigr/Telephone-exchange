using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;
using ATS.Billing.Data;

namespace ATS.Billing
{
    public class ExchangeBilling : IExchangeBilling
    {
        private BillingUnitOfWork _data;
        
        public ExchangeBilling(BillingUnitOfWork data)
        {
            this._data = data;
        }

        public bool AbonentIsAvalible(Phone abonent)
        {
            throw new NotImplementedException();
        }

        public void AbonentsConnectedEventHandler(object sender, RingEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void AbonentsDisconnectedEventHandler(object sender, RingEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
