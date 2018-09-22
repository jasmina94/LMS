namespace LMS.BusinessLogic.BookManagement.Model
{
    public class SaveBookResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public SaveBookResult()
        {
            Success = false;
            Message = "Server error while saving book.";
        }

        public SaveBookResult(int id, string name)
        {
            Success = true;
            Message = "Successfully saved book with id: " + id + " and name: " + name + ".";
            Id = id;
            Name = name;
        }

        public SaveBookResult(int id, string name, string message)
        {
            Success = true;
            Message = message;
            Id = id;
            Name = name;
        }
    }
}