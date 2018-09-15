using LMS.DomainModel.DataSource.Source;
using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.DataSource.Transaction
{
    public interface ITransactionManager
    {
        SqlTransaction StartTransaction(IDataSource dataSource);

        SqlTransaction StartTransaction(IDataSource dataSource, IsolationLevel isolationLevel);

        void CommitTransaction(SqlTransaction sqlTransaction);

        void RollbackTransaction(SqlTransaction sqlTransaction);
    }
}
