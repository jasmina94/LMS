namespace LMS.BusinessLogic.LanguageManagement.Model
{
    public class DeleteLanguageResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public DeleteLanguageResult()
        {
            Success = false;
            Message = "Please select language for delete.";
        }

        public DeleteLanguageResult(int id, string name)
        {
            Success = true;
            Message = "Successfully deleted language with id: " + id + " and name: " + name + ".";
            Id = id;
            Name = name;
        }
    }
}