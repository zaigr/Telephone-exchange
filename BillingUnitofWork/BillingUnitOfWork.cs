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
        public BillingUnitOfWork(IRepository<Phone> phones, IRepository<Tariff> tariffs, IRepository<IndividualClient> individualClients, IRepository<Client> clients, IRepository<Billing> billing)
        {
            Phones = phones;
            Tariffs = tariffs;
            IndividualClients = individualClients;
            Clients = clients;
            Billing = billing;
        }

        public IRepository<Phone> Phones { get; }
        public IRepository<Tariff> Tariffs { get; }
        public IRepository<IndividualClient> IndividualClients { get; }
        public IRepository<Client> Clients { get; }

        //IRepository<Actions> Actions;
        //IRepository<ClientActions> ClientActions;

        public IRepository<Billing> Billing { get; }
    }
}
