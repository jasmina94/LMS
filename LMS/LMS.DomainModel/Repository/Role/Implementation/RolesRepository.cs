using LMS.DomainModel.ConversionHelper;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Infrastructure.ORM.Model.Implementation;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Role.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.Repository.Role.Implementation
{
    public class RolesRepository : Repository<RoleData>, IRoleRepository
    {
        public RolesRepository() : base()
        {

        }

        public RoleData GetRoleByCode(string code)
        {
            RoleData role = null;
            SqlCommand sqlCommand = CreateCommandGetRoleByCode(code);
            DataRow dataRow = DataSource.GetDataRow(sqlCommand);

            if (dataRow != null)
            {
                var mapping = (MappingModel<RoleData>)mappingModel;
                role = mapping.ExecuteConversion(dataRow);
            }

            return role;
        }

        private SqlCommand CreateCommandGetRoleByCode(string code)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      CodeRole LIKE @CodeRole
         ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@CodeRole", code);

            return sqlCommand;
        }
    }
}
