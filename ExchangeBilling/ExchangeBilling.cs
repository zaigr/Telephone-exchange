using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;
using ATS.Billing.Data;

namespace ATS.Billing
{
    public class ExchangeBilling : IExchangeBilling
    {
        private BillingUnitOfWork _data;
        private IBalanceCheck _balanceCheck;
 
        public TimeSpan CallReceivingDelay { get; private set; }
        private IDictionary<(Phone sender, Phone reciver), Data.Billing> _currentRings;
        
        public ExchangeBilling(BillingUnitOfWork data, TimeSpan callReceivingDelay, uint balanceCheckDayNumb = 1, double balanceCountIntervalSeconds = 30)
        {
            this._data = data;
            this.CallReceivingDelay = callReceivingDelay;
            
            this._balanceCheck = new BalanceCheck(_data.Clients, _data.Billing ,TimeSpan.FromSeconds(balanceCountIntervalSeconds));
            _balanceCheck.SetControlDay(balanceCheckDayNumb);

            this._currentRings = new Dictionary<(Phone sender, Phone reciver), Data.Billing>();     
        }

        public bool AbonentIsAvalible(Phone abonent)
        {
            var client = _data.IndividualClients.GetEntityOrDefault(c => c.Phone == abonent);
            if (client != null && client.Status == ClientStatus.Avalible) {
                return true;
            }
            else {
                return false;
            }
        }

        public void AbonentsConnectedEventHandler(object sender, RingEventArgs e)
        {
            var senderClient = _data.Clients.GetEntityOrDefault(c => c.Phone == e.Sender);
            var receiverClient = _data.Clients.GetEntityOrDefault(c => c.Phone == e.Reciver);

            var billing = new Data.Billing(senderClient, receiverClient, DateTimeOffset.Now);
            _currentRings.Add((sender: e.Sender, reciver: e.Reciver), billing);
        }

        public void AbonentsDisconnectedEventHandler(object sender, RingEventArgs e)
        {
            if (_currentRings.ContainsKey((sender: e.Sender, reciver: e.Reciver))) {
                var billing = _currentRings[(sender: e.Sender, reciver: e.Reciver)];

                var callTariff = billing.FromClient.Tariff;

                var duration = DateTimeOffset.Now - billing.Date;
                decimal cost = 0;
                if (duration > CallReceivingDelay) {
                    cost = callTariff.GetCallCost(e.Sender, e.Reciver, duration);
                }

                billing.Duration = duration;
                billing.Cost = cost;
                _data.Billing.AddEntity(billing);

                _currentRings.Remove((sender: e.Sender, reciver: e.Reciver));
            }
            else {
                // Here could be error log
            }
        }
    }
}
