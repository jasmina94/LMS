using System.Collections.Generic;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Book.Interfaces;
using System.Linq;
using System.Data.SqlClient;
using LMS.DomainModel.ConversionHelper;
using System.Data;

namespace LMS.DomainModel.Repository.Book.Implementation
{
    public class BookCopyRepository : Repository<BookCopyData>, IBookCopyRepository
    {
        public BookCopyRepository() : base()
        {

        }

        public List<BookCopyData> GetAvailableCopies()
        {
            List<BookCopyData> copies =  GetAllActiveData();
            copies = copies.Where(x => !x.OnLoan).ToList();

            return copies;
        }

        public List<BookCopyData> GetCopiesForBook(int id)
        {
            List<BookCopyData> referencingBookCopies = null;
            SqlCommand sqlCommand = CreateSqlCommandGetCopiesForBook(id);
            DataRowCollection dataRowCollection = DataSource.GetDataRowCollection(sqlCommand);

            referencingBookCopies = ConvertToList(dataRowCollection);

            return referencingBookCopies;
        }

        private SqlCommand CreateSqlCommandGetCopiesForBook(int bookId)
        {
            string query = @"
               SELECT   * 
               FROM     {0}
               WHERE    IsActive = @IsActive
               AND      RefBook LIKE @BookId
         ";

            SqlCommand sqlCommand = new SqlCommand(string.Format(query, tableName));

            sqlCommand.Parameters.AddWithValue("@IsActive", true.ToDBFromBool());
            sqlCommand.Parameters.AddWithValue("@BookId", bookId);

            return sqlCommand;
        }
    }
}
