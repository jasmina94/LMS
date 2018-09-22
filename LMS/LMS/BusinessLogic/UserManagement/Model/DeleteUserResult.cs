namespace LMS.BusinessLogic.UserManagement.Model
{
    public class DeleteUserResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public DeleteUserResult()
        {
            Success = false;
            Message = "Please select user for delete.";
        }

        public DeleteUserResult(int id, string name)
        {
            Success = true;
            Message = "Successfully deleted user with id: " + id + " and name: " + name + ".";
            Id = id;
            Name = name;
        }
    }
}