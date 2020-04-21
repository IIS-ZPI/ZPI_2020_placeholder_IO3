using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace USASales.Tests
{
    public abstract class DatabaseTest
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _dbTransaction;

        protected DatabaseTest(IDbConnection connection)
        {
            _connection = connection;
        }

        [TestInitialize]
        public void Initialize()
        {
            _dbTransaction = _connection.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbTransaction.Rollback();
        }
    }
}
