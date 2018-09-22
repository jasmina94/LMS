using LMS.BusinessLogic.BookManagement.Model;
using LMS.Models.ViewModels.Relation;

namespace LMS.BusinessLogic.BookManagement.Interfaces
{
    public interface ILoanService
    {
        RelationUserBookCopyViewModel CreateLoanModel(int bookCopyId);

        BorrowResult BorrowBook(RelationUserBookCopyViewModel viewModel);
    }
}