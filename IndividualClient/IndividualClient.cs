using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;

namespace ATS.Billing.Data
{
    public class IndividualClient : Client
    {
        public string Name { get; set; }

        public IndividualClient(int id, ITariff tariff, Phone phone, ClientStatus status, string name)
            : base(id, tariff, phone, status)
        {  }
    }
}
