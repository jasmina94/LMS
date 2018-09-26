using LMS.BusinessLogic.BookManagement.Model;
using LMS.Models.ViewModels.Relation;
using System.Collections.Generic;

namespace LMS.BusinessLogic.BookManagement.Interfaces
{
    public interface ILoanService
    {
        RelationUserBookCopyViewModel CreateLoanModel(int bookCopyId);

        BorrowResult BorrowBook(RelationUserBookCopyViewModel viewModel);

        List<RelationUserBookCopyViewModel> GetActiveLoans();

        BorrowResult ReturnBook(int loanId);
    }
}