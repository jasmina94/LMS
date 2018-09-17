using System;
using System.Collections.Generic;
using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Relation.Interfaces;
using System.Data;
using System.Data.SqlClient;
using LMS.DomainModel.ConversionHelper;

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
    }
}
