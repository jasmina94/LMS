namespace LMS.BusinessLogic.CategoryManagement.Model
{
    public class SaveCategoryResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public SaveCategoryResult()
        {
            Success = false;
            Message = "Server error while saving category.";
        }

        public SaveCategoryResult(int id, string name)
        {
            Success = true;
            Message = "Successfully saved category with id: " + id + " and name: " + name + ".";
            Id = id;
            Name = name;
        }
    }
}