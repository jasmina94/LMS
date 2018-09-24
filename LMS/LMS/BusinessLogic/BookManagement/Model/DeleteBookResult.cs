namespace LMS.BusinessLogic.BookManagement.Model
{
    public class DeleteBookResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public DeleteBookResult()
        {
            Success = false;
            Message = "Please select book for delete.";
        }

        public DeleteBookResult(int id, string name)
        {
            Success = true;
            Message = "Successfully deleted book with id: " + id + " and name: " + name + ".";
            Id = id;
            Name = name;
        }

        public DeleteBookResult(int id, string name, int copyId)
        {
            Id = id;
            Name = name;
            Success = true;
            Message = "Successfully deleted copy of book " + Name + 
                        " with id: " + id + " and copy id: " + copyId + ".";            
        }
    }
}