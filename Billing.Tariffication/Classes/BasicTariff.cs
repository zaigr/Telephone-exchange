using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;


namespace ATS.Billing.Tariffication
{
    public class BasicTariff : Tariff, Interfaces.ITariff
    {
        public IDictionary<Phone, Phone> _favoritePhonesDict;

        public BasicTariff(int id, decimal costPerMinute, string name)
            : base(id, costPerMinute, name)
        {
            _favoritePhonesDict = new Dictionary<Phone, Phone>();
        }

        public BasicTariff(int id, decimal costPerMinute, string name, IDictionary<Phone, Phone> favoritePhones)
            : this(id, costPerMinute, name)
        {
            this._favoritePhonesDict = favoritePhones;
        }

        public override decimal GetCallCost(Phone sender, Phone reciver, TimeSpan durability)
        {
            if (_favoritePhonesDict.ContainsKey(sender) && _favoritePhonesDict[sender] == reciver) {
                return 0;
            }

            return base.GetCallCost(sender, reciver, durability);
        }
    }
}
