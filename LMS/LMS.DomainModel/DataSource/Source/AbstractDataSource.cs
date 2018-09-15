using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.DataSource.Source
{
    public abstract class AbstractDataSource : IDataSource
    {
        public abstract void SetSqlConnection(string sqlConnectionString);

        public abstract SqlConnection CreateSqlConnection();

        public abstract void ExecuteNonQuery(SqlCommand sqlCommand);

        public abstract void ExecuteNonQueryInTransaction(SqlCommand sqlCommand);

        public abstract void ExecuteNonQueryInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);

        public abstract DataRow GetDataRow(SqlCommand sqlCommand);

        public abstract DataRowCollection GetDataRowCollection(SqlCommand sqlCommand);

        public abstract DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand);

        public abstract DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);

        public abstract DataRow GetDataRowInTransaction(SqlCommand sqlCommand);

        public abstract DataRow GetDataRowInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);

        public abstract DataTable GetDataTable(SqlCommand sqlCommand);

        public abstract DataTable GetDataTableInTransaction(SqlCommand sqlCommand);

        public abstract DataTable GetDataTableInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);

        public abstract object GetScalarValue(SqlCommand sqlCommand);

        public abstract object GetScalarValueInTransaction(SqlCommand sqlCommand);

        public abstract object GetScalarValueInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);
    }
}
