namespace LMS.BusinessLogic.BookManagement.Model
{
    public class BorrowResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int LoanId { get; set; }

        public int BookCopyId { get; set; }

        public string Username { get; set; }

        public BorrowResult()
        {
            Success = false;
            Message = "Server side error while borrowing book.";
        }

        public BorrowResult(int loanId, int bookCopyId, string username)
        {
            Success = true;
            Message = "Successfully created loan " + loanId + " for user " + username 
                            + " book copy identification " + bookCopyId;
            LoanId = loanId;
            Username = username;
         }
    }
}