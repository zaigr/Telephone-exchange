using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Billing.Data.Interfaces;

namespace ATS.Billing.Data
{
    public class ClientRepository : IRepository<Client>
    {
        private ISet<Client> _clientSet;

        public int Count { get => _clientSet.Count; }

        public ClientRepository(IEnumerable<Client> clients)
        {
            _clientSet = new HashSet<Client>(clients);
        }

        public void AddEntity(Client entity)
        {
            _clientSet.Add(entity);
        }

        public IEnumerable<Client> GetEntities(Func<Client, bool> selector)
        {
            return _clientSet.Where(selector);
        }

        public IEnumerable<Client> GetAllEntities()
        {
            return _clientSet;
        }

        public Client GetEntityOrDefault(Func<Client, bool> selector)
        {
            return _clientSet.FirstOrDefault(selector);
        }

        public bool RemoveEntity(Client entity)
        {
            return _clientSet.Remove(entity);
        }

        public bool RemoveEntity(Func<Client, bool> selector)
        {
            var client = _clientSet.FirstOrDefault(selector);
            return client != null ? _clientSet.Remove(client) : false;
        }
    }
}
