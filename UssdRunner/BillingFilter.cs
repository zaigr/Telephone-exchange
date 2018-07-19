using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ATS.Interfaces;
using ATS.Billing.Data;
using ATS.Billing.Data.Interfaces;
using ATS.Billing.Interfaces;

namespace ATS.Billing
{
    internal class BillingFilter
    {
        private Expression _selector;    

        public IDictionary<string, MemberExpression> Arguments { get; private set; }

        public BillingFilter()
        {
            var dateProperty = Expression.Property(Expression.Variable(typeof(Data.Billing)), "Date");
            Arguments.Add("-d", dateProperty);

            var abonentProperty = Expression.Property(Expression.Variable(typeof(Data.Billing)), "ToClient.Phone");
            Arguments.Add("-a", abonentProperty);

            var durationProperty = Expression.Property(Expression.Variable(typeof(Data.Billing)), "Duration");
        }
    }
}
