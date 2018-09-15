using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.DataSource.Server
{
    public interface ISqlServerDatabase
    {
        SqlConnection CreateSqlConnection();

        DataTable GetDataTable(SqlCommand sqlCommand);

        DataTable GetDataTableInTransaction(SqlCommand sqlCommand);

        DataTable GetDataTableInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);

        DataRowCollection GetDataRowCollection(SqlCommand sqlCommand);

        DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand);

        DataRowCollection GetDataRowCollectionInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);

        DataRow GetDataRow(SqlCommand sqlCommand);

        DataRow GetDataRowInTransaction(SqlCommand sqlCommand);

        DataRow GetDataRowInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);

        object GetScalarValue(SqlCommand sqlCommand);

        object GetScalarValueInTransaction(SqlCommand sqlCommand);

        object GetScalarValueInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);

        void ExecuteNonQuery(SqlCommand sqlCommand);

        void ExecuteNonQueryInTransaction(SqlCommand sqlCommand);

        void ExecuteNonQueryInTransaction(SqlCommand sqlCommand, SqlTransaction sqlTransaction);
    }
}
