using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;


namespace ATS.Billing
{
    public class BasicTariff : Tariff, ITariff
    {
        public Phone FavoriteNumber { get; set; }

        public BasicTariff(int id, decimal costPerMinute, string name, Phone favoriteNumber)
            : base(id, costPerMinute, name)
        {
            FavoriteNumber = favoriteNumber;
        }

        public override decimal GetCallCost(Phone sender, Phone reciver, TimeSpan durability)
        {
            if (reciver == FavoriteNumber) {
                return 0;
            }

            return base.GetCallCost(sender, reciver, durability);
        }
    }
}
