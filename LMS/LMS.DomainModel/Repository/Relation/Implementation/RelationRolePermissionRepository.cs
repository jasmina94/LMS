using System;
using System.Collections.Generic;
using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Relation.Interfaces;
using System.Data.SqlClient;
using System.Data;
using LMS.DomainModel.ConversionHelper;

namespace LMS.DomainModel.Repository.Relation.Implementation
{
    public class RelationRolePermissionRepository : Repository<RelationRolePermissionData>, IRelationRolePermissionRepository
    {
        public RelationRolePermissionRepository() : base()
        {

        }

        public List<RelationRolePermissionData> GetRelationRolePermissionFor(int roleId)
        {
            List<RelationRolePermissionData> data = new List<RelationRolePermissionData>();
            SqlCommand sqlCommand = CreateCommandGetRelationByRole(roleId);
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollection(sqlCommand);

            data = ConvertToList(dataRowCollection);

            return data;
        }

        private SqlCommand CreateCommandGetRelationByRole(int id)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      RefRole LIKE @RefRoleId
         ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@RefRoleId", id);

            return sqlCommand;
        }
    }
}
