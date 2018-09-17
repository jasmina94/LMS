using LMS.DomainModel.ConversionHelper;
using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Infrastructure.ORM.Model.Implementation;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Relation.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LMS.DomainModel.Repository.Relation.Implementation
{
    public class RelationUserRoleRepository : Repository<RelationUserRoleData>, IRelationUserRoleRepository
    {
        public RelationUserRoleRepository() : base()
        {

        }

        public List<RelationUserRoleData> GetRelationUserRoleFor(int userId)
        {
            List<RelationUserRoleData> data = null;
            SqlCommand sqlCommand = CreateCommandGetRelationUserRole(userId);
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollection(sqlCommand);

            data = ConvertToList(dataRowCollection);

            return data;
        }

        public RelationUserRoleData GetRelationUserRoleFor(int userId, int roleId)
        {
            RelationUserRoleData relation = null;
            SqlCommand sqlCommand = CreateCommandGetRelationUserRole(userId, roleId);

            DataRow dataRow = DataSource.GetDataRow(sqlCommand);
            if (dataRow != null)
            {
                var mapping = (MappingModel<RelationUserRoleData>)mappingModel;
                relation = mapping.ExecuteConversion(dataRow);
            }

            return relation;
        }

        private SqlCommand CreateCommandGetRelationUserRole(int userId, int roleId)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      RefUser LIKE @UserId
               AND      RefRole LIKE @RoleId
         ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@UserId", userId);
            sqlCommand.Parameters.AddWithValue("@RoleId", roleId);

            return sqlCommand;
        }

        private SqlCommand CreateCommandGetRelationUserRole(int userId)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      RefUser LIKE @UserId
         ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@UserId", userId);

            return sqlCommand;
        }
    }
}
