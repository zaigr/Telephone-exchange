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
    public class PortConnectionTest
    {
        private ITerminal _terminal;
        private IEnumerable<IPort> _ports;

        [SetUp]
        public void Init()
        {
            var portsId = new int[] { 1, 2, 3 };
            _ports = portsId.Select(id => new Port(id)).ToList();

            IPortConnectionFactory connectionFactory = new PortConnectionFactory(_ports);

            _terminal = new Terminal(new Phone(111), null);
            _terminal.ConnectingToPort += connectionFactory.Connect;
            _terminal.DisconnectingFromPort += connectionFactory.Disconnect;
        }

        [Test]
        public void ConnectingTest()
        {
            Assert.IsTrue(_terminal.ConnectToPort());

            Assert.That(_ports.Count(p => p.State == PortState.Listened) == 1);

            _terminal.DisconnectFromPort();

            Assert.That(_ports.Count(p => p.State == PortState.Unused) == _ports.Count());
        }
    }
}
