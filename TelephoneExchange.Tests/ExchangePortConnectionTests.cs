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
    public class ExchangePortConnectionTests
    {
        private IList<Phone> _phones;
        private ITelephoneExchange _exchange;

        [SetUp]
        public void Init()
        {
            var phoneNumbers = new int[] { 111, 222, 333 };
            _phones = phoneNumbers.Select(numb => new Phone(numb)).ToList();

            var portNumbers = new List<int>() { 10, 20 };
            var ports = portNumbers.Select(numb => new Port(numb));

            var exchangeBilling = new Mocks.ExchangeBillingMock(p => true);

            _exchange = new ATS.TelephoneExchange(new HashSet<IPort>(ports), new HashSet<Phone>(_phones), exchangeBilling);
        }
        
        [Test]
        public void SuccessfulExchangeConnectDisconnectTest()
        {
            var user = _phones.First();

            Assert.That(_exchange.ConnectToExchange(user) == true);
            Assert.That(_exchange.DisconnectFromExchange(user) == true);
        }

        [Test]
        public void DoubleConnectionTest()
        {
            var user = _phones.First();

            Assert.That(_exchange.ConnectToExchange(user) == true);
            Assert.That(_exchange.ConnectToExchange(user) == true);
            
            Assert.That(_exchange.DisconnectFromExchange(user) == true);
        }

        [Test]
        public void DoubleDisconnetionTest()
        {
            var user = _phones.First();

            Assert.That(_exchange.ConnectToExchange(user) == true);
            
            Assert.That(_exchange.DisconnectFromExchange(user) == true);
            Assert.That(_exchange.DisconnectFromExchange(user) == false);
        }

        [Test]
        public void NoFreePortsToConnectTest()
        {
            _exchange.ConnectToExchange(_phones[0]);
            _exchange.ConnectToExchange(_phones[1]);

            Assert.That(_exchange.ConnectToExchange(_phones[2]) == false);
        }

        [Test]
        public void NotSupportedPhoneTest()
        {
            var unsupported = new Phone(-100);

            Assert.That(_exchange.ConnectToExchange(unsupported) == false);
        }
    }
}
