using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Transactions;

namespace Application.Tests.Attributes
{
    internal class DatabseTransactionDisposeAttribute : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public ActionTargets Targets => ActionTargets.Test;

        public void AfterTest(ITest test)
        {
            _transactionScope.Dispose();
        }

        public void BeforeTest(ITest test)
        {
            _transactionScope = new TransactionScope();
        }
    }
}
