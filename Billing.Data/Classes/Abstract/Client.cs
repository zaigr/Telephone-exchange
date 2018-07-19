using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;
using ATS.Billing.Tariffication.Interfaces;

namespace ATS.Billing.Data
{
    public abstract class Client
    {
        public int Id { get; set; }
        public ITariff Tariff { get; set; }
        public Phone Phone { get; set; }
        public decimal Balance { get; set; }
        public ClientStatus Status { get; set; }

        public Client(int id, ITariff tariff, Phone phone, decimal balance, ClientStatus status)
        {
            Id = id;
            Tariff = tariff;
            Phone = phone;
            Balance = balance;
            Status = status;
        }
    }
}
