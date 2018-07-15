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
            var phoneNumbers = new int[] { 111, 222 };
            var phones = phoneNumbers.Select(numb => new Phone(numb)).ToList();

            var portNumbers = new List<int>() { 10, 20 };
            var ports = portNumbers.Select(numb => new Port(numb));

            var exchange = new TelephoneExchange(new HashSet<IPort>(ports), new HashSet<Phone>(phones), null);

            var callReceiveDelay = TimeSpan.FromMilliseconds(5000);
            var senderTerminal = new Terminal(phones[0], exchange, callReceiveDelay);
            var reciverTerminal = new Terminal(phones[1], exchange, callReceiveDelay);

            Console.WriteLine(senderTerminal.ConnectToExchange());
            Console.WriteLine(reciverTerminal.ConnectToExchange());

            var callStatus = senderTerminal.MakeCall(reciverTerminal.PhoneNumber);
            var disconnectStatus = reciverTerminal.CloseCall();

            Console.WriteLine(senderTerminal.DisconnectFromExchange());
            Console.WriteLine(reciverTerminal.DisconnectFromExchange());
        }
    }
}
