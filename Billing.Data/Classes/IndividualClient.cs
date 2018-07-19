using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;
using ATS.Billing.Tariffication.Interfaces;

namespace ATS.Billing.Data
{
    public class IndividualClient : Client
    {
        public string Name { get; set; }
        
        public IndividualClient(int id, ITariff tariff, Phone phone, decimal balance, ClientStatus status, string name)
            : base(id, tariff, phone, balance, status)
        {
            Name = name;
            Balance = balance;
        }
    }
}
