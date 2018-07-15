using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Billing.Data.Interfaces;


namespace ATS.Billing.Data
{
    public class BillingRepository : IRepository<Billing>
    {
        private ISet<Billing> _billingSet;

        public BillingRepository(IEnumerable<Billing> billing)
        {
            this._billingSet = new HashSet<Billing>(billing);
        }

        public void AddEntity(Billing entity)
        {
            _billingSet.Add(entity);
        }

        public IEnumerable<Billing> GetAllEntities()
        {
            return _billingSet.ToList();
        }

        public IEnumerable<Billing> GetEntities(Func<Billing, bool> selector)
        {
            return _billingSet.Where(selector);
        }

        public Billing GetEntityOrDefault(Func<Billing, bool> selector)
        {
            return _billingSet.FirstOrDefault(selector);
        }

        public bool RemoveEntity(Billing entity)
        {
            return _billingSet.Remove(entity);
        }

        public bool RemoveEntity(Func<Billing, bool> selector)
        {
            var entity = _billingSet.FirstOrDefault(selector);
            return entity != null ? _billingSet.Remove(entity) : false;
        }
    }
}
