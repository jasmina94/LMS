using System.Collections.Generic;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Book.Interfaces;
using System.Data.SqlClient;
using System.Data;
using LMS.DomainModel.ConversionHelper;

namespace LMS.DomainModel.Repository.Book.Implementation
{
    public class BookRepository : Repository<BookData>, IBookRepository
    {
        public BookRepository() : base()
        {
        }

        public List<BookData> GetBooksByCategory(int categoryId)
        {
            List<BookData> books = null;
            SqlCommand sqlCommand = CreateSqlCommandGetBooksByCategory(categoryId);
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollection(sqlCommand);

            books = ConvertToList(dataRowCollection);

            return books;
        }

        public List<BookData> GetBooksByLanguage(int languageId)
        {
            List<BookData> books = null;
            SqlCommand sqlCommand = CreateSqlCommandGetBooksByLanguage(languageId);
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollection(sqlCommand);

            books = ConvertToList(dataRowCollection);

            return books;
        }

        private SqlCommand CreateSqlCommandGetBooksByCategory(int categoryId)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      RefCategory LIKE @CategoryId
         ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@CategoryId", categoryId);

            return sqlCommand;
        }

        private SqlCommand CreateSqlCommandGetBooksByLanguage(int languageId)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      RefLanguage LIKE @LanguageId
         ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@LanguageId", languageId);

            return sqlCommand;
        }
    }
}
