using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATS.Interfaces;
using ATS.Billing.Data;
using ATS.Billing.Interfaces;


namespace ATS.Billing.Tests
{
    [TestFixture]
    public class NoAnswerCallTests
    {
        private BillingUnitOfWork unitOfWork;
        private IExchangeBilling exchangeBilling;

        private Client firstClient;
        private Client secondClient;
        private TimeSpan callWaitingSpan;

        [SetUp]
        public void Init()
        {
            var phones = new List<Phone> { new Phone(111), new Phone(222) };
            var favoritePhones = new Dictionary<Phone, Phone> { { phones[0], phones[1] } };
            var tariff = new BasicTariff(1, costPerMinute: 1000, name: "1", favoritePhones: favoritePhones);

            firstClient = new IndividualClient(1, tariff, phones[0], 0, ClientStatus.Avalible, "111");
            secondClient = new IndividualClient(2, tariff, phones[1], 0, ClientStatus.Avalible, "222");
            var clients = new List<Client>
            {
                firstClient,
                secondClient
            };

            this.unitOfWork = new BillingUnitOfWork(
                new PhoneRepository(phones),
                new TariffRepository(new List<BasicTariff> { tariff }),
                null,
                new ClientRepository(clients),
                new BillingRepository(new List<Data.Billing>())
            );

            this.callWaitingSpan = TimeSpan.FromSeconds(10);
            this.exchangeBilling = new ExchangeBilling(unitOfWork, callWaitingSpan, (uint)DateTime.Now.Day, 1);
        }

        [Test]
        public void NoAnswerToCallTest()
        {
            var callEventArgs = new RingEventArgs(firstClient.Phone, secondClient.Phone);
            exchangeBilling.AbonentsConnectedEventHandler(null, callEventArgs);

            Task.Delay((int)callWaitingSpan.TotalMilliseconds - 6000).Wait();

            exchangeBilling.AbonentsDisconnectedEventHandler(null, callEventArgs);
            Task.Delay(5000).Wait();

            Assert.That(unitOfWork.Billing.Count == 1);
            Assert.That(unitOfWork.Billing.GetEntities(e => e.Checked).Count() == 1);
            Task.Delay(5000).Wait();

            Assert.That(firstClient.Balance == 0);
            Assert.That(secondClient.Balance == 0);
        }
    }
}
