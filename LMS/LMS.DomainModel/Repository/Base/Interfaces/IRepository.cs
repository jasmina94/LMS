using LMS.DomainModel.DomainObject;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.Repository.Base.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAllData();

        List<T> GetAllDataInTransaction(SqlTransaction sqlTransaction);

        List<T> GetAllActiveData();

        List<T> GetAllActiveDataInTransaction(SqlTransaction sqlTransaction);

        List<T> ConvertToList(DataRowCollection dataRowCollection);

        T GetDataById(int id);

        T GetDataByIdInTransaction(int id, SqlTransaction sqlTransaction);

        void DeleteAllData();

        void DeleteAllDataInTransaction(SqlTransaction sqlTransaction);

        void DeleteById(int id);

        void DeleteByIdInTransaction(int id, SqlTransaction sqlTransaction);

        int SaveData(BaseData data);

        int SaveDataInTransaction(BaseData data, SqlTransaction sqlTransaction);
    }
}
