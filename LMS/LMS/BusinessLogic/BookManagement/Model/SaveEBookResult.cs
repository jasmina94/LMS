namespace LMS.BusinessLogic.BookManagement.Model
{
    public class SaveEBookResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public string BookAuthorAndTitle { get; set; }

        public SaveEBookResult()
        {
            Success = false;
            Message = "Server error while saving e-book.";
        }

        public SaveEBookResult(int id, string bookAuthorAndTitle)
        {
            Success = true;           
            Id = id;
            BookAuthorAndTitle = bookAuthorAndTitle;
            Message = "Successfully saved e-book [" + bookAuthorAndTitle  + " id: " + id + "]";
        }

        public SaveEBookResult(int id, string bookAuthorAndTitle, string message)
        {
            Id = id;
            Success = true;
            Message = message;            
            BookAuthorAndTitle = bookAuthorAndTitle;
        }

        public SaveEBookResult(bool isSuccess, int id, string message, string bookAuthorAndTitle)
        {
            Success = isSuccess;
            Id = id;
            Message = message;
            BookAuthorAndTitle = bookAuthorAndTitle;
        }
    }
}