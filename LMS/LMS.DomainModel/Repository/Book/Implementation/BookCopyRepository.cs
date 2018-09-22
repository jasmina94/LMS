using System.Collections.Generic;
using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Book.Interfaces;
using System.Linq;

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
    }
}
