using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Billing.Data.Interfaces;

namespace ATS.Billing.Data
{
    public class TariffRepository : IRepository<Tariff>
    {
        private ISet<Tariff> _tariffSet;

        public int Count { get => _tariffSet.Count; }
        
        public TariffRepository(IEnumerable<Tariff> tariffs)
        {
            this._tariffSet = new HashSet<Tariff>(tariffs);
        }

        public void AddEntity(Tariff tariff)
        {
            _tariffSet.Add(tariff);
        }

        public IEnumerable<Tariff> GetEntities(Func<Tariff, bool> selector)
        {
            return _tariffSet.Where(selector);
        }

        public IEnumerable<Tariff> GetAllEntities()
        {
            return _tariffSet.ToList();
        }

        public Tariff GetEntityOrDefault(Func<Tariff, bool> selector)
        {
            return _tariffSet.FirstOrDefault(selector);
        }

        public bool RemoveEntity(Tariff tariff)
        {
            return _tariffSet.Remove(tariff);
        }

        public bool RemoveEntity(Func<Tariff, bool> selector)
        {
            var selectedTariff = _tariffSet.FirstOrDefault(selector);
            return selectedTariff != null ? _tariffSet.Remove(selectedTariff) : false;
        }
    }
}
