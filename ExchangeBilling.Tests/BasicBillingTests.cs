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

        private Client firstClient;
        private Client secondClient;

        [SetUp]
        public void Init()
        {
            var phones = new List<Phone> { new Phone(111), new Phone(222) };
            var tariff = new BasicTariff(1, costPerMinute: 1000, name: "1");

            firstClient = new IndividualClient(1, tariff, phones[0], 0, ClientStatus.Avalible, "111");
            secondClient = new IndividualClient(2, tariff, phones[1], 0, ClientStatus.Avalible, "222");
            var clients = new List<Client>
            {
                firstClient,
                secondClient
            };

            this.unitOfWork = new BillingUnitOfWork(
                new PhoneRepository(phones), 
                new BasicTariffRepository(new List<BasicTariff> { tariff }),
                null, 
                new ClientRepository(clients), 
                new BillingRepository(new List<Data.Billing>()));

            this.exchangeBilling = new ExchangeBilling(unitOfWork, TimeSpan.FromSeconds(5),
                    (uint)DateTime.Now.Day, 1);
        }

        [Test]
        public void StandartCallHandlingTest()
        {
            var callEventArgs = new RingEventArgs(firstClient.Phone, secondClient.Phone);
            exchangeBilling.AbonentsConnectedEventHandler(null, callEventArgs);

            Task.Delay(20000).Wait();

            exchangeBilling.AbonentsDisconnectedEventHandler(null, callEventArgs);
            Task.Delay(21000).Wait();

            Assert.That(unitOfWork.Billing.Count > 0);
            Assert.That(unitOfWork.Billing.GetEntities(b => b.Checked).Count() > 0);
            Task.Delay(5000).Wait();

            Assert.That(firstClient.Balance < 0);
            Task.Delay(3000).Wait();

            Assert.That(secondClient.Balance == 0);
        }
    }
}
