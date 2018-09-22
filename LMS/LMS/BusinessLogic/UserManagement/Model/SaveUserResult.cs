namespace LMS.BusinessLogic.UserManagement.Model
{
    public class SaveUserResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public SaveUserResult()
        {
            Success = false;
            Message = "Server error while saving user.";
        }

        public SaveUserResult(int id, string name)
        {
            Success = true;
            Message = "Successfully saved user with id: " + id + " and name: " + name + ".";
            Id = id;
            Name = name;
        }
    }
}