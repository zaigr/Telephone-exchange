using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Billing.Interfaces;
using ATS.Interfaces;

namespace ATS.Tests.TelephoneExchange.Mocks
{
    public class ExchangeBillingMock : IExchangeBilling
    {
        private Func<Phone, bool> _abonentAvalibleCheck;

		public ExchangeBillingMock(Func<Phone, bool> abonentAvalibilityStrategy)
        {
            this._abonentAvalibleCheck = abonentAvalibilityStrategy;
        }

        public bool AbonentIsAvalible(Phone abonent)
        {
            return _abonentAvalibleCheck(abonent);
        }

        public void AbonentsConnectedEventHandler(object sender, RingEventArgs e)
        {
            return;
        }

        public void AbonentsDisconnectedEventHandler(object sender, RingEventArgs e)
        {
            return;
        }
    }
}
