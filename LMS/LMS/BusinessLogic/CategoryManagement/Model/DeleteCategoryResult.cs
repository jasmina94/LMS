namespace LMS.BusinessLogic.CategoryManagement.Model
{
    public class DeleteCategoryResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public DeleteCategoryResult()
        {
            Success = false;
            Message = "Please select category for delete.";
        }

        public DeleteCategoryResult(int id, string name)
        {
            Success = true;
            Message = "Successfully deleted category with id: " + id + " and name: " + name + ".";
            Id = id;
            Name = name;
        }
    }
}