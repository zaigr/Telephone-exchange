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
        IRepository<Phone> Phones { get; }
        IRepository<BasicTariff> Tariffs { get; }
        IRepository<IndividualClient> Clients { get; }

        //IRepository<Actions> Actions;
        //IRepository<ClientActions> ClientActions;


        //IRepository<IBilling> Billing;
    }
}
