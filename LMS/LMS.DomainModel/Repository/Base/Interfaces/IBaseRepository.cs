using System.Data.SqlClient;

namespace LMS.DomainModel.Repository.Base.Interfaces
{
    public interface IBaseRepository
    {
        int GetNextId();

        int GetNextIdInTransaction(SqlTransaction sqlTransaction);

        SqlCommand CreateSqlCommandGetAll();

        SqlCommand CreateSqlCommandGetAllActive();

        SqlCommand CreateSqlCommandGetById(int id);

        SqlCommand CreateSqlCommandDeleteAllData();

        SqlCommand CreateSqlCommandDeleteById(int id);
    }
}
