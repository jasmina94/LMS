using System.Collections.Generic;
using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Relation.Interfaces;
using System.Data;
using System.Data.SqlClient;
using LMS.DomainModel.ConversionHelper;
using System;
using LMS.DomainModel.Infrastructure.ORM.Model.Implementation;

namespace LMS.DomainModel.Repository.Relation.Implementation
{
    public class RelationUserPermissionRepository : Repository<RelationUserPermissionData>, IRelationUserPermissionRepository
    {
        public RelationUserPermissionRepository() : base()
        {

        }

        public List<RelationUserPermissionData> GetRelationUserPermissionFor(int userId)
        {
            List<RelationUserPermissionData> data = null;
            SqlCommand sqlCommand = CreateCommandGetRelationUserPermissionFor(userId);
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollection(sqlCommand);

            data = ConvertToList(dataRowCollection);

            return data;
        }

        public RelationUserPermissionData GetRelationUserPermissionFor(int userId, int permissionId)
        {
            RelationUserPermissionData data = null;
            SqlCommand sqlCommand = CreateCommandGetRelationUserPermissionFor(userId, permissionId);
            DataRow dataRow = DataSource.GetDataRow(sqlCommand);

            if (dataRow != null)
            {
                var mapping = (MappingModel<RelationUserPermissionData>)mappingModel;
                data = mapping.ExecuteConversion(dataRow);
            }

            return data;
        }

        private SqlCommand CreateCommandGetRelationUserPermissionFor(int userId)
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

        private SqlCommand CreateCommandGetRelationUserPermissionFor(int userId, int permissionId)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      RefUser LIKE @UserId
               AND      RefPermission LIKE @PermissionId
            ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@UserId", userId);
            sqlCommand.Parameters.AddWithValue("@PermissionId", permissionId);

            return sqlCommand;
        }
    }
}
