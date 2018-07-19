using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Interfaces;
using ATS.Billing.Data;
using ATS.Billing.Data.Interfaces;


namespace ATS.Billing
{ // fill myself like a GNU developer

    public class UssdRunner : IUssdRunner
    {
        private BillingUnitOfWork _unitOfWork;
        //private ICollection<Commands> _commands;


        public UssdRunner(BillingUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
            //this._commands = null;
        }

        public IEnumerable<string> GetListOfCommands()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> RunCommand(string command)
        {
            throw new NotImplementedException();
        }
    }
}
