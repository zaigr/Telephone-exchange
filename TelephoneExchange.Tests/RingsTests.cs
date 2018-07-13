using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATS.Interfaces;


namespace ATS.Tests.TelephoneExchange
{
    [TestFixture]
    public class RingsTests
    {
        private IList<Phone> _phones;
        private ITelephoneExchange _exchange;

        [SetUp]
        public void Init()
        {
            var phoneNumbers = new int[] { 111, 222, 333 };
            _phones = phoneNumbers.Select(numb => new Phone(numb)).ToList();

            var portNumbers = new List<int>() { 10, 20, 30 };
            var ports = portNumbers.Select(numb => new Port(numb));

            var exchangeBiling = new Mocks.ExchangeBillingMock(p => true);

            _exchange = new ATS.TelephoneExchange(new HashSet<IPort>(ports), new HashSet<Phone>(_phones), exchangeBiling);
        }

        [Test]
        public void SuccessfulRingTest()
        {
            var sender = _phones[0];
            _exchange.ConnectToExchange(sender);

            var reciver = _phones[1];
            _exchange.ConnectToExchange(reciver);

            Assert.That(_exchange.ConnectAbonents(sender, reciver) == CallState.Connected);
            Assert.That(_exchange.DisconnectAbonents(sender, reciver) == CallState.Disconnected);
        }

        [Test]
        public void RingFromDisconnectedUserTest()
        {
            var sender = _phones[0]; // Not connected to exchange

            var reciver = _phones[1];
            _exchange.ConnectToExchange(reciver);

            Assert.That(_exchange.ConnectAbonents(sender, reciver) == CallState.Error);
        }

        [Test]
        public void RingToDisconnectedUserTest()
        {
            var sender = _phones[0];
            _exchange.ConnectToExchange(sender);

            var reciver = _phones[1]; // Not connected to exchange

            Assert.That(_exchange.ConnectAbonents(sender, reciver) == CallState.Error);
        }

        [Test]
        public void EngagetRingTest()
        {
            var sender = _phones[0];
            _exchange.ConnectToExchange(sender);

            var reciver = _phones[1];
            _exchange.ConnectToExchange(reciver);

            _exchange.ConnectAbonents(sender, reciver);

            var engaget = _phones[2];
            _exchange.ConnectToExchange(engaget);

            Assert.That(_exchange.ConnectAbonents(engaget, sender) == CallState.Engaget);
            Assert.That(_exchange.ConnectAbonents(engaget, reciver) == CallState.Engaget);
        }

        [Test]
        public void DoubleConnectionTest()
        {
            var sender = _phones[0];
            _exchange.ConnectToExchange(sender);

            var reciver = _phones[1];
            _exchange.ConnectToExchange(reciver);

            Assert.That(_exchange.ConnectAbonents(sender, reciver) == CallState.Connected);
            Assert.That(_exchange.ConnectAbonents(sender, reciver) == CallState.Engaget);

            Assert.That(_exchange.DisconnectAbonents(reciver, sender) == CallState.Disconnected);
        }

        [Test]
        public void DoubleDisconnectionTest()
        {
            var sender = _phones[0];
            _exchange.ConnectToExchange(sender);

            var reciver = _phones[1];
            _exchange.ConnectToExchange(reciver);

            _exchange.ConnectAbonents(sender, reciver);
            _exchange.DisconnectAbonents(reciver, sender);

            Assert.That(_exchange.DisconnectAbonents(sender, reciver) == CallState.Error);
        }

        [Test]
        public void SelfConnectionTest()
        {
            var sender = _phones[0];
            var reciver = _phones[0];

            _exchange.ConnectToExchange(sender);

            Assert.That(_exchange.ConnectAbonents(sender, reciver) == CallState.Error);
        }

        [Test]
        public void SelfDisconnection()
        {
            var sender = _phones[0];
            var reciver = _phones[0];

            _exchange.ConnectToExchange(sender);

            Assert.That(_exchange.DisconnectAbonents(sender, reciver) == CallState.Error);
        }
    }
}
