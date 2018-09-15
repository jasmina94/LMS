using System;
using System.Data.SqlClient;
using LMS.DomainModel.Repository.Base.Interfaces;
using LMS.DomainModel.DataSource.Source;
using LMS.DomainModel.Service;
using LMS.DomainModel.ConversionHelper;

namespace LMS.DomainModel.Repository.Base.Implementation
{
    public class BaseRepository<T> : IBaseRepository where T : class
    {
        protected string tableName;
        protected string idName;

        public IDataSource DataSource { get; set; }

        public BaseRepository()
        {
            tableName = NamingService.GetTableNameFromRepository(this.GetType());
            idName = NamingService.GetIdName(typeof(T));
        }

        public int GetNextId()
        {
            SqlCommand sqlCommand = CreateSqlCommandGetMaxId();
            int id = Convert.ToInt32(DataSource.GetScalarValue(sqlCommand));
            int nextId = CalculateNextId(id);

            return nextId;
        }

        public int GetNextIdInTransaction(SqlTransaction sqlTransaction)
        {
            SqlCommand sqlCommand = CreateSqlCommandGetMaxId();
            int id = Convert.ToInt32(DataSource.GetScalarValueInTransaction(sqlCommand, sqlTransaction));
            int nextId = CalculateNextId(id);

            return nextId;
        }

        public SqlCommand CreateSqlCommandGetAll()
        {
            string query = @"SELECT * FROM {0}";
            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            return sqlCommand;
        }

        public SqlCommand CreateSqlCommandGetAllActive()
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
         ";
            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());

            return sqlCommand;
        }

        public SqlCommand CreateSqlCommandGetById(int id)
        {
            string query = @"
               SELECT   TOP 1 * 
               FROM     {0} 
               WHERE    {1} = @Id
         ";
            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName, idName));

            sqlCommand.Parameters.AddWithValue("@Id", id);

            return sqlCommand;
        }

        public SqlCommand CreateSqlCommandDeleteAllData()
        {
            string query = @"TRUNCATE TABLE {0}";
            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            return sqlCommand;
        }

        public SqlCommand CreateSqlCommandDeleteById(int id)
        {
            string query = @"
               UPDATE   {0}
               SET      IsActive = @IsActive, DateTimeDeletedOn = @DateTimeDeletedOn
               WHERE    {1} = @Id 
         ";
            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName, idName));

            sqlCommand.Parameters.AddWithValue("@IsActive", false.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@DateTimeDeletedOn", DateTime.Now);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            return sqlCommand;
        }

        #region Private methods

        private SqlCommand CreateSqlCommandGetMaxId()
        {
            string query = @"SELECT MAX({0}) FROM {1}";
            SqlCommand sqlCommand = new SqlCommand(string.Format(query, idName, tableName));

            return sqlCommand;
        }

        private int CalculateNextId(int id)
        {
            int calculatedNextId = 1;

            if (id != 0)
            {
                calculatedNextId = id;
                calculatedNextId++;
            }

            return calculatedNextId;
        }

        private SqlCommand CreateSqlCommandGetIdFromSystemTable(string codeSystemTable)
        {
            string query = @"
               SELECT  IdSystemTable
               FROM    SystemTable
               WHERE   CodeSystemTable = '{0}'
         ";
            SqlCommand sqlCommand = new SqlCommand(string.Format(query, codeSystemTable));

            return sqlCommand;
        }

        private int ParseIdFromSystemTable(object id)
        {
            int idFromSystemTable = 0;
            int idParsed;

            if (id != null && Int32.TryParse(id.ToString(), out idParsed))
            {
                idFromSystemTable = (int)id;
            }

            return idFromSystemTable;
        }
        #endregion
    }
}
