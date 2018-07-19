using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Billing.Interfaces
{
    public interface IUssdRunner
    {
        IEnumerable<string> RunCommand(string command);
        IEnumerable<string> GetListOfCommands();
    }
}
