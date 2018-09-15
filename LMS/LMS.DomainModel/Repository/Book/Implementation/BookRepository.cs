using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Implementation;
using LMS.DomainModel.Repository.Book.Interfaces;

namespace LMS.DomainModel.Repository.Book.Implementation
{
    public class BookRepository : Repository<BookData>, IBookRepository
    {
        public BookRepository() : base()
        {
        }
    }
}
