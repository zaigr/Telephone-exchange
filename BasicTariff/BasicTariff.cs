using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;


namespace ATS.Billing
{
    public class BasicTariff : ITariff
    {
        public BasicTariff(int id, decimal costPerMinute, string name)
        {
            Id = id;
            CostPerMinute = costPerMinute;
            Name = name;
        }

        public int Id { get; }
        public decimal CostPerMinute { get; set; }
        public string Name { get; set; }

        public decimal GetCallCost(Phone sender, Phone reciver, TimeSpan durability)
        {
            return CostPerMinute * (decimal)durability.TotalMinutes;
        }
    }
}
