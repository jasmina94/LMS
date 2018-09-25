using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Interfaces;
using System.Collections.Generic;

namespace LMS.DomainModel.Repository.Book.Interfaces
{
    public interface IBookRepository : IRepository<BookData>
    {
        List<BookData> GetBooksByCategory(int categoryId);

        List<BookData> GetBooksByLanguage(int languageId);
    }
}
