using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Data.Interfaces;

namespace ATS.Billing.Data
{
    public class BillingUnitOfWork
    {
        public IRepository<Phone> Phones { get; }
        public IRepository<BasicTariff> Tariffs { get; }
        public IRepository<IndividualClient> IndividualClients { get; }
        public IRepository<Client> Clients { get; }

        //IRepository<Actions> Actions;
        //IRepository<ClientActions> ClientActions;

        public IRepository<Billing> Billing { get; set; }
    }
}
