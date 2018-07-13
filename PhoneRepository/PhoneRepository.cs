using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;

namespace ATS.Billing
{
    public class PhoneRepository : IRepository<Phone>
    {
        private ISet<Phone> _phonesSet;

        public PhoneRepository(IEnumerable<Phone> phones)
        {
            _phonesSet = new HashSet<Phone>(phones);
        }

        public void AddEntity(Phone entity)
        {
            _phonesSet.Add(entity);
        }

        public IEnumerable<Phone> GetAllEntities()
        {
            return _phonesSet.ToList();
        }

        public Phone GetEntity(Func<Phone, bool> selector)
        {
            return _phonesSet.First(selector);
        }

        public bool RemoveEntity(Phone entity)
        {
            return _phonesSet.Remove(entity);
        }

        public bool RemoveEntity(Func<Phone, bool> selector)
        {
            var item = _phonesSet.First();
            return item != null ? _phonesSet.Remove(item) : false;
        }
    }
}
