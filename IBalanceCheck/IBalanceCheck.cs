using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Billing.Interfaces
{
    public interface IBalanceCheck
    {
        void SetControlDay(uint dayNumber);
    }
}
