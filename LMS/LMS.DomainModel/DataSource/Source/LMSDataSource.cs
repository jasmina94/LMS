using LMS.DomainModel.DataSource.Server;
using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.DataSource.Source
{
    public class LMSDataSource : AbstractDataSource
    {
        public ISqlServerDatabase SqlServerDatabase { get; set; }
        
        public override void SetSqlConnection(string sqlConnectionString)
        {
            SqlServerDatabase = new SqlServerDatabase(sqlConnectionString);
        }

        public override SqlConnection CreateSqlConnection()
        {
            return SqlServerDatabase.CreateSqlConnection();
        }

        public override void ExecuteNonQuery(SqlCommand sqlCommand)
        {
            SqlServerDatabase.ExecuteNonQuery(sqlCommand);
        }

        public override void ExecuteNonQueryInTransaction(SqlCommand sqlCommand)
        {
            SqlServerDatabase.ExecuteNonQueryInTransaction(sqlCommand);
        }

        public override void ExecuteNonQueryInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            SqlServerDatabase.ExecuteNonQueryInTransaction(sqlCommand, sqlTransaction);
        }

        public override DataRow GetDataRow(SqlCommand sqlCommand)
        {
            return SqlServerDatabase.GetDataRow(sqlCommand);
        }

        public override DataRowCollection GetDataRowCollection(SqlCommand sqlCommand)
        {
            return SqlServerDatabase.GetDataRowCollection(sqlCommand);
        }

        public override DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand)
        {
            return SqlServerDatabase.GetDataRowCollectionInTransaction(sqlCommand);
        }

        public override DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            return SqlServerDatabase.GetDataRowCollectionInTransaction(sqlCommand, sqlTransaction);
        }

        public override DataRow GetDataRowInTransaction(SqlCommand sqlCommand)
        {
            return SqlServerDatabase.GetDataRowInTransaction(sqlCommand);
        }

        public override DataRow GetDataRowInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            return SqlServerDatabase.GetDataRowInTransaction(sqlCommand, sqlTransaction);
        }

        public override DataTable GetDataTable(SqlCommand sqlCommand)
        {
            return SqlServerDatabase.GetDataTable(sqlCommand);
        }

        public override DataTable GetDataTableInTransaction(SqlCommand sqlCommand)
        {
            return SqlServerDatabase.GetDataTableInTransaction(sqlCommand);
        }

        public override DataTable GetDataTableInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            return SqlServerDatabase.GetDataTableInTransaction(sqlCommand, sqlTransaction);
        }

        public override object GetScalarValue(SqlCommand sqlCommand)
        {
            return SqlServerDatabase.GetScalarValue(sqlCommand);
        }

        public override object GetScalarValueInTransaction(SqlCommand sqlCommand)
        {
            return SqlServerDatabase.GetScalarValueInTransaction(sqlCommand);
        }

        public override object GetScalarValueInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction)
        {
            return SqlServerDatabase.GetScalarValueInTransaction(sqlCommand, sqlTransaction);
        }
    }
}
