using LMS.DomainModel.ConversionHelper;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Infrastructure.ORM.Model.Implementation;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.User.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.Repository.User.Implementation
{
    public class UsersRepository : Repository<UserData>, IUserRepository
    {
        public UsersRepository() : base()
        {
        }

        public UserData GetUserByUsername(string username)
        {
            UserData user = null;
            SqlCommand sqlCommand = CreateSqlCommandGetUserByUsername(username);
            DataRow dataRow = DataSource.GetDataRow(sqlCommand);

            if (dataRow != null)
            {
                var mapping = (MappingModel<UserData>)mappingModel;
                user = mapping.ExecuteConversion(dataRow);
            }

            return user;
        }

        private SqlCommand CreateSqlCommandGetUserByUsername(string username)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      Username LIKE @Username
         ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@Username", username);

            return sqlCommand;
        }
    }
}
