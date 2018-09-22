using LMS.DomainModel.DomainObject;
using LMS.DomainModel.Repository.Base.Interfaces;
using System.Collections.Generic;

namespace LMS.DomainModel.Repository.Book.Interfaces
{
    public interface IBookCopyRepository : IRepository<BookCopyData>
    {
        List<BookCopyData> GetAvailableCopies();
    }
}
