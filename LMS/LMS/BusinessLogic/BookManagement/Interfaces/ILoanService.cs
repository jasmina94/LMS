using LMS.BusinessLogic.BookManagement.Model;
using LMS.Infrastructure.Authorization;
using LMS.Models.ViewModels.Relation;
using System.Collections.Generic;

namespace LMS.BusinessLogic.BookManagement.Interfaces
{
    public interface ILoanService
    {
        RelationUserBookCopyViewModel CreateLoanModel(int bookCopyId);

        BorrowResult BorrowBook(RelationUserBookCopyViewModel viewModel, UserSessionObject user);

        List<RelationUserBookCopyViewModel> GetLoans(bool active);

        List<RelationUserBookCopyViewModel> GetLoansForUser(bool active, int userId);

        BorrowResult ReturnBook(int loanId, UserSessionObject user);
    }
}