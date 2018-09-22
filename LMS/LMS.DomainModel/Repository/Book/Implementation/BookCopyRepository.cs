using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Book.Interfaces;

namespace LMS.DomainModel.Repository.Book.Implementation
{
    public class BookCopyRepository : Repository<BookCopyData>, IBookCopyRepository
    {
        public BookCopyRepository() : base()
        {

        }
    }
}
