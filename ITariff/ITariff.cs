using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;

namespace ATS.Billing.Interfaces
{
    public interface ITariff
    {
        int Id { get; }

        decimal GetCallCost(Phone sender, Phone reciver, TimeSpan durability);
    }
}
