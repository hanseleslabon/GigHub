using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace GigHub.IntegrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public void BeforeTest(ITest testDetails)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(ITest testDetails)
        {
            _transactionScope.Dispose();
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}
