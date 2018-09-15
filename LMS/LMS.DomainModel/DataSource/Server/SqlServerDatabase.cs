using System;
using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.DataSource.Server
{
    public class SqlServerDatabase : ISqlServerDatabase
    {
        public string ConnectionString { get; set; }

        public SqlServerDatabase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public SqlConnection CreateSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public void ExecuteNonQuery(SqlCommand sqlCommand)
        {
            using (SqlConnection sqlConnection = CreateSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void ExecuteNonQueryInTransaction(SqlCommand sqlCommand)
        {
            sqlCommand.ExecuteNonQuery();
        }

        public void ExecuteNonQueryInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            sqlCommand.Connection = sqlTransaction.Connection;
            sqlCommand.Transaction = sqlTransaction;
            ExecuteNonQueryInTransaction(sqlCommand);
        }

        public DataRow GetDataRow(SqlCommand sqlCommand)
        {
            DataRow dataRow = null;
            DataTable dataTable = GetDataTable(sqlCommand);

            if (dataTable.Rows.Count > 0)
            {
                dataRow = dataTable.Rows[0];
            }

            return dataRow;
        }

        public DataRowCollection GetDataRowCollection(SqlCommand sqlCommand)
        {
            DataRowCollection dataRowCollection = null;
            DataTable dataTable = GetDataTable(sqlCommand);

            if (dataTable.Rows.Count > 0)
            {
                dataRowCollection = dataTable.Rows;
            }

            return dataRowCollection;
        }

        public DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand)
        {
            DataRowCollection dataRowCollection = null;
            DataTable dataTable = GetDataTableInTransaction(sqlCommand);

            if (dataTable.Rows.Count > 0)
            {
                dataRowCollection = dataTable.Rows;
            }

            return dataRowCollection;
        }

        public DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            DataRowCollection dataRowCollection = null;
            DataTable dataTable = GetDataTableInTransaction(sqlCommand, sqlTransaction);

            if (dataTable.Rows.Count > 0)
            {
                dataRowCollection = dataTable.Rows;
            }

            return dataRowCollection;
        }

        public DataRow GetDataRowInTransaction(SqlCommand sqlCommand)
        {
            DataRow dataRow = null;
            DataTable dataTable = GetDataTableInTransaction(sqlCommand);

            if (dataTable.Rows.Count > 0)
            {
                dataRow = dataTable.Rows[0];
            }

            return dataRow;
        }

        public DataRow GetDataRowInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            DataRow dataRow = null;
            DataTable dataTable = GetDataTableInTransaction(sqlCommand, sqlTransaction);

            if (dataTable.Rows.Count > 0)
            {
                dataRow = dataTable.Rows[0];
            }

            return dataRow;
        }

        public DataTable GetDataTable(SqlCommand sqlCommand)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = CreateSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                dataTable.Load(sqlCommand.ExecuteReader());
            }

            return dataTable;
        }

        public DataTable GetDataTableInTransaction(SqlCommand sqlCommand)
        {
            DataTable dataTable = new DataTable();

            dataTable.Load(sqlCommand.ExecuteReader());

            return dataTable;
        }

        public DataTable GetDataTableInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            DataTable dataTable = new DataTable();

            sqlCommand.Connection = sqlTransaction.Connection;
            sqlCommand.Transaction = sqlTransaction;
            dataTable.Load(sqlCommand.ExecuteReader());

            return dataTable;
        }

        public object GetScalarValue(SqlCommand sqlCommand)
        {
            object value = null;

            using (SqlConnection sqlConnection = CreateSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                value = sqlCommand.ExecuteScalar();
            }

            return value;
        }

        public object GetScalarValueInTransaction(SqlCommand sqlCommand)
        {
            return sqlCommand.ExecuteScalar();
        }

        public object GetScalarValueInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            sqlCommand.Connection = sqlTransaction.Connection;
            sqlCommand.Transaction = sqlTransaction;

            return sqlCommand.ExecuteScalar();
        }
    }
}
