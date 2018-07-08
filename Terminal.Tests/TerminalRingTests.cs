using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATS.Interfaces;

namespace ATS.Tests
{
    [TestFixture]
    public class TerminalRingTests
    {
        private ITerminal _terminal;
        private IList<Phone> _phones;

        [SetUp]
        public void Init()
        {
            var phoneNumbers = new int[] { 111, 222 };
            _phones = phoneNumbers.Select(numb => new Phone(numb)).ToList();

            var portNumbers = new List<int>() { 10, 20 };
            var ports = portNumbers.Select(numb => new Port(numb));

            var exchange = new TelephoneExchange(new HashSet<IPort>(ports), new HashSet<Phone>(_phones), null);

            _terminal = new Terminal(_phones[0], exchange);
        }

        [Test]
        public void SuccessfulCall()
        {
            Assert.That(_terminal.ConnectToExchange() == true);

            var reciverPhone = _phones[1];
            Assert.That(_terminal.MakeCall(reciverPhone) == CallState.Connected);
            Assert.That(_terminal.CloseCall() == CallState.Disconnected);

            Assert.That(_terminal.DisconnectFromExchange() == true);
        }

        [Test]
        public void NonexistentPhoneCall()
        {
            _terminal.ConnectToExchange();

            var nonexistentPhone = new Phone(000);
            Assert.That(_terminal.MakeCall(nonexistentPhone) == CallState.Error);
        }
    }
}
