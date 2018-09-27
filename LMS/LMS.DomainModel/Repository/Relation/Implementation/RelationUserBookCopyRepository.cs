using System.Collections.Generic;
using LMS.DomainModel.DomainObject.Relation;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Relation.Interfaces;
using System.Data.SqlClient;
using LMS.DomainModel.ConversionHelper;
using System.Data;

namespace LMS.DomainModel.Repository.Relation.Implementation
{
    public class RelationUserBookCopyRepository : Repository<RelationUserBookCopyData>, IRelationUserBookCopyRepository
    {
        public RelationUserBookCopyRepository() : base()
        {

        }

        public List<RelationUserBookCopyData> GetLoansForUser(int userId)
        {
            var loans = new List<RelationUserBookCopyData>();
            SqlCommand sqlCommand = CreateSqlCommandGetLoansForUser(userId);
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollection(sqlCommand);

            loans = ConvertToList(dataRowCollection);

            return loans;
        }

        private SqlCommand CreateSqlCommandGetLoansForUser(int userId)
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
