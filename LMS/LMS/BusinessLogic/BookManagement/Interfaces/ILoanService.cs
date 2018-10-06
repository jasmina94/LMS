using LMS.BusinessLogic.BookManagement.Model;
using LMS.Models.ViewModels.Relation;
using System.Collections.Generic;

namespace LMS.BusinessLogic.BookManagement.Interfaces
{
    public interface ILoanService
    {
        RelationUserBookCopyViewModel CreateLoanModel(int bookCopyId);

        BorrowResult BorrowBook(RelationUserBookCopyViewModel viewModel);

        List<RelationUserBookCopyViewModel> GetLoans(bool active);

        List<RelationUserBookCopyViewModel> GetLoansForUser(bool active, int userId);

        BorrowResult ReturnBook(int loanId);
    }
}