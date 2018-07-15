using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Billing.Data.Interfaces;

namespace ATS.Billing.Data
{
    public class IndividualClientRepository : IRepository<IndividualClient>
    {
        private ISet<IndividualClient> _clientSet;

        public IndividualClientRepository(IEnumerable<IndividualClient> clients)
        {
            this._clientSet = new HashSet<IndividualClient>(clients);
        }

        public void AddEntity(IndividualClient entity)
        {
            _clientSet.Add(entity);
        }

        public IEnumerable<IndividualClient> GetEntities(Func<IndividualClient, bool> selector)
        {
            return _clientSet.Where(selector);
        }

        public IEnumerable<IndividualClient> GetAllEntities()
        {
            return _clientSet.ToList();
        }

        public IndividualClient GetEntityOrDefault(Func<IndividualClient, bool> selector)
        {
            return _clientSet.FirstOrDefault(selector);
        }

        public bool RemoveEntity(IndividualClient entity)
        {
            return _clientSet.Remove(entity);
        }

        public bool RemoveEntity(Func<IndividualClient, bool> selector)
        {
            var client = _clientSet.FirstOrDefault(selector);
            return client != null ? _clientSet.Remove(client) : false;
        }
    }
}
