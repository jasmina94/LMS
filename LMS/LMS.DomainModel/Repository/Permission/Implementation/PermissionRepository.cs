using LMS.DomainModel.ConversionHelper;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Infrastructure.ORM.Model.Implementation;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Permission.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.Repository.Permission.Implementation
{
    public class PermissionRepository : Repository<PermissionData>, IPermissionRepository
    {
        public PermissionRepository() : base()
        {

        }

        public PermissionData GetPermissionByCode(string code)
        {
            PermissionData permission = null;
            SqlCommand sqlCommand = CreateCommandGetPermissionByCode(code);
            DataRow dataRow = DataSource.GetDataRow(sqlCommand);

            if (dataRow != null)
            {
                var mapping = (MappingModel<PermissionData>)mappingModel;
                permission = mapping.ExecuteConversion(dataRow);
            }

            return permission;
        }

        private SqlCommand CreateCommandGetPermissionByCode(string code)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      CodePermission LIKE @CodePermission
         ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@CodePermission", code);

            return sqlCommand;
        }
    }
}
