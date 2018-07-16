using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Billing.Data.Interfaces;

namespace ATS.Billing.Data
{
    public class BasicTariffRepository : IRepository<BasicTariff>
    {
        private ISet<BasicTariff> _tariffSet;

        public int Count { get => _tariffSet.Count; }
        
        public BasicTariffRepository(IEnumerable<BasicTariff> tariffs)
        {
            this._tariffSet = new HashSet<BasicTariff>(tariffs);
        }

        public void AddEntity(BasicTariff tariff)
        {
            _tariffSet.Add(tariff);
        }

        public IEnumerable<BasicTariff> GetEntities(Func<BasicTariff, bool> selector)
        {
            return _tariffSet.Where(selector);
        }

        public IEnumerable<BasicTariff> GetAllEntities()
        {
            return _tariffSet.ToList();
        }

        public BasicTariff GetEntityOrDefault(Func<BasicTariff, bool> selector)
        {
            return _tariffSet.FirstOrDefault(selector);
        }

        public bool RemoveEntity(BasicTariff tariff)
        {
            return _tariffSet.Remove(tariff);
        }

        public bool RemoveEntity(Func<BasicTariff, bool> selector)
        {
            var selectedTariff = _tariffSet.FirstOrDefault(selector);
            return selectedTariff != null ? _tariffSet.Remove(selectedTariff) : false;
        }
    }
}
