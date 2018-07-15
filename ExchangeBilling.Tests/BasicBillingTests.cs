using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATS.Interfaces;
using ATS.Billing.Data;
using ATS.Billing.Interfaces;
using ATS.Billing.Data.Interfaces;

namespace ATS.Billing.Tests
{
    [TestFixture]
    public class BasicBillingTests
    {
        private BillingUnitOfWork unitOfWork;
        private IExchangeBilling exchangeBilling;

        [SetUp]
        public void Init()
        {
            var phones = new List<Phone> { new Phone(111), new Phone(222) };
            var tariff = new BasicTariff(1, costPerMinute: 10, name: "1");
            var clients = new List<Client>
            {
                new IndividualClient(1, tariff, phones[0], 0, ClientStatus.Avalible, "111"),
                new IndividualClient(2, tariff, phones[1], 0, ClientStatus.Avalible, "222")
            };

            this.unitOfWork = new BillingUnitOfWork(
                new PhoneRepository(phones), 
                new BasicTariffRepository(new List<BasicTariff> { tariff }),
                null, 
                new ClientRepository(clients), 
                new BillingRepository(new List<Data.Billing>()));

            this.exchangeBilling = new ExchangeBilling(unitOfWork, TimeSpan.FromSeconds(30));
        }
    }
}
