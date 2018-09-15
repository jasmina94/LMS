using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.DomainModel.DataSource.Source;

namespace LMS.DomainModel.DataSource.Transaction
{
    public class TransactionManager : ITransactionManager
    {
        public SqlTransaction StartTransaction(IDataSource dataSource)
        {
            SqlConnection sqlConnection = dataSource.CreateSqlConnection();

            sqlConnection.Open();

            return sqlConnection.BeginTransaction();
        }

        public SqlTransaction StartTransaction(IDataSource dataSource, IsolationLevel isolationLevel)
        {
            SqlConnection sqlConnection = dataSource.CreateSqlConnection();

            sqlConnection.Open();

            return sqlConnection.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction(SqlTransaction sqlTransaction)
        {
            SqlConnection sqlConnection = sqlTransaction.Connection;

            sqlTransaction.Commit();
            sqlConnection.Close();
        }

        public void RollbackTransaction(SqlTransaction sqlTransaction)
        {
            SqlConnection sqlConnection = sqlTransaction.Connection;

            sqlTransaction.Rollback();
            sqlConnection.Close();
        }
    }
}
