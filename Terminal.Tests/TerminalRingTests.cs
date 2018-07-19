using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using ATS.Interfaces;
using ATS.Billing.Interfaces;

namespace ATS.Tests.Terminal
{
    [TestFixture]
    public class TerminalRingTests
    {
        private TimeSpan _callReceivingDelay;
        private ITerminal _senderTerminal;
        private ITerminal _reciverTerminal;
        private IList<Phone> _phones;

        [SetUp]
        public void Init()
        {
            var phoneNumbers = new int[] { 111, 222 };
            _phones = phoneNumbers.Select(numb => new Phone(numb)).ToList();

            var portNumbers = new List<int>() { 10, 20 };
            var ports = portNumbers.Select(numb => new Port(numb));

            var exchangeBilling = new Mocks.ExchangeBillingMock(p => true);

            var exchange = new TelephoneExchange(new HashSet<IPort>(ports), new HashSet<Phone>(_phones), exchangeBilling);

            _callReceivingDelay = TimeSpan.FromMilliseconds(10000);
            _senderTerminal = new ATS.Terminal(_phones[0], exchange, null, _callReceivingDelay);
            _reciverTerminal = new ATS.Terminal(_phones[1], exchange, null, _callReceivingDelay);
        }

        [Test]
        public void SuccessfulCall()
        {
            Assert.That(_senderTerminal.ConnectToExchange() == true);
            Assert.That(_reciverTerminal.ConnectToExchange() == true);
            
            Assert.That(_senderTerminal.MakeCall(_reciverTerminal.PhoneNumber) == CallState.Connected);

            Assert.That(_reciverTerminal.ReceiveCall() == CallState.Connected);

            Task.Delay(1000).Wait();
            Assert.That(_reciverTerminal.CloseCall() == CallState.Disconnected);

            Assert.That(_senderTerminal.DisconnectFromExchange() == true);
            Assert.That(_reciverTerminal.DisconnectFromExchange() == true);
        }

        [Test]
        public void CallNotReceived()
        {
            Assert.That(_senderTerminal.ConnectToExchange() == true);
            Assert.That(_reciverTerminal.ConnectToExchange() == true);

            Assert.That(_senderTerminal.MakeCall(_reciverTerminal.PhoneNumber) == CallState.Connected);

            Task.Delay((int)_callReceivingDelay.TotalMilliseconds + 1000).Wait();
            Assert.That(_reciverTerminal.ReceiveCall() == CallState.Disconnected);
        }

        [Test]
        public void NonexistentPhoneCall()
        {
            _senderTerminal.ConnectToExchange();

            var nonexistentPhone = new Phone(000);
            Assert.That(_senderTerminal.MakeCall(nonexistentPhone) == CallState.Error);
        }
    }
}
