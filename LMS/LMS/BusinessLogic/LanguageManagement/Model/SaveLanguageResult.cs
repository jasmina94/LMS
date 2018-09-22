namespace LMS.BusinessLogic.LanguageManagement.Model
{
    public class SaveLanguageResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public SaveLanguageResult()
        {
            Success = false;
            Message = "Server error while saving language.";
        }

        public SaveLanguageResult(int id, string name)
        {
            Success = true;
            Message = "Successfully saved language with id: " + id + " and name: " + name + ".";
            Id = id;
            Name = name;
        }
    }
}