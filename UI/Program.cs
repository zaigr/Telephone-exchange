using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;

namespace ATS.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var phoneNumbers = new List<int>() { 511131, 434435 };
            var phones = new HashSet<Phone>();

            foreach (var number in phoneNumbers) {
                phones.Add(new Phone(number));
            }

            var ports = new HashSet<IPort>();
            for (int i = 0; i < 3; ++i) {
                ports.Add(new Port(i));
            }

            ITelephoneExchange exchange = new TelephoneExchange(ports, phones, null);

            var terminals = new List<ITerminal>();
            foreach (var phone in phones) {
                terminals.Add(new ATS.Terminal(phone, exchange));
            }

            var user1 = terminals[0];
            var user2 = terminals[1];

            if (user1.ConnectToExchange())
            {
                var status = user1.MakeCall(user2.PhoneNumber);
                Console.WriteLine(status);
            }
        }
    }
}
