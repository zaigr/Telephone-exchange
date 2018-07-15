using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Billing;

namespace ATS.Billing.Data
{
    public class Billing
    {
        public Client FromClient { get; }
        public Client ToClient { get; }
        public DateTimeOffset Date { get; }
        public TimeSpan Duration { get; set; }
        public decimal Cost { get; set; }
        public bool Checked { get; set; }

        public Billing(Client fromClient, Client toClient, DateTimeOffset date)
        {
            FromClient = fromClient;
            ToClient = toClient;
            Date = date;

            Checked = false;
        }

        public Billing(Client fromClient, Client toClient, DateTimeOffset date, TimeSpan duration, decimal cost)
            : this(fromClient, toClient, date)
        {
            Duration = duration;
            Cost = cost;
        }
    }
}
