using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATS.Interfaces;
using ATS.Billing.Interfaces;


namespace ATS.Tests.TelephoneExchange
{
    [TestFixture]
    public class RingAvalibleCheckStrategyTests
    {
        private IList<Phone> _phones;
        private ITelephoneExchange _exchange;

        [SetUp]
        public void Init()
        {
            var phoneNumbers = new int[] { 100, 333 };
            _phones = phoneNumbers.Select(numb => new Phone(numb)).ToList();

            var portNumbers = new List<int>() { 10, 20 };
            var ports = portNumbers.Select(numb => new Port(numb));

            Func<Phone, bool> blockOddNumbersStrategy = (Phone p) => int.Parse($"{p}") % 2 == 0;
            var exchangeBilling = new Mocks.ExchangeBillingMock(blockOddNumbersStrategy);

            _exchange = new ATS.TelephoneExchange(new HashSet<IPort>(ports), new HashSet<Phone>(_phones), exchangeBilling);
        }

        [Test]
        public void SenderBlockedTest()
        {
            var sender = _phones[1]; // Odd
            _exchange.ConnectToExchange(sender);

            var reciver = _phones[0]; // Even
            _exchange.ConnectToExchange(reciver);

            Assert.That(_exchange.ConnectAbonents(sender, reciver) == CallState.Locked);
        }

        [Test]
        public void ReciverBlockerTest()
        {
            var sender = _phones[0]; // Even
            _exchange.ConnectToExchange(sender);

            var reciver = _phones[1]; // Odd
            _exchange.ConnectToExchange(reciver);

            Assert.That(_exchange.ConnectAbonents(sender, reciver) == CallState.Engaget);
        }
    }
}
