using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using ATS.Billing.Interfaces;
using ATS.Billing.Data;
using ATS.Billing.Data.Interfaces;


namespace ATS.Billing
{
    public class BalanceCheck : IBalanceCheck
    {
        private Timer _balanceControlTimer;

        private Timer _balanceCountTimer;
        private readonly TimeSpan _balanceCountTimerInterval;

        private IRepository<Client> _clients;
        private IRepository<Data.Billing> _billing;

        public BalanceCheck(IRepository<Client> clients, IRepository<Data.Billing> billing, TimeSpan balanceCountTimerInterval)
        {
            _clients = clients;
            _billing = billing;
            _balanceCountTimerInterval = balanceCountTimerInterval;

            _balanceCountTimer = new Timer(_balanceCountTimerInterval.TotalMilliseconds);
            _balanceCountTimer.Elapsed += CountClientsBalance;
            _balanceCountTimer.Start();
        }

        public void SetControlDay(uint dayNumber)
        {
            if (!(dayNumber > 1 && dayNumber < 32)) {
                throw new ArgumentOutOfRangeException("Day number must be from 1 to 31");
            }

            var interval = TimeSpan.FromDays(1).TotalMilliseconds;
            _balanceControlTimer = new Timer(interval);
            _balanceControlTimer.Elapsed += BalanceControl;

            void BalanceControl(object sender, ElapsedEventArgs e)
            {
                if (DateTimeOffset.Now.Day != dayNumber) {
                    return;
                }

                foreach (var client in _clients.GetAllEntities()) {
                    if (client.Balance < 0) {
                        client.Status = ClientStatus.Locked;
                    }
                }
            }
        }

        protected void CountClientsBalance(object sender, ElapsedEventArgs e)
        {
            var newBilling = _billing.GetEntities(b => !b.Checked);
            var clientGroup = newBilling.GroupBy(b => b.FromClient);

            foreach (var clientBills in clientGroup) {
                var client = clientBills.Key;
                foreach (var bill in clientBills) {
                    client.Balance -= bill.Cost;
                    bill.Checked = true;
                }
            }
        }
    }
}
