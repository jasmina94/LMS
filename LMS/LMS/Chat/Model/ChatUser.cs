namespace LMS.Chat.Model
{
    public class ChatUser
    {
        public string ConnectionId { get; set; }        
        public int UserId { get; set; }        
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsOnline { get; set; }

        public ChatUser() { }

        public ChatUser(string connectionId, int userId, string username, string firstname, string lastname)
        {
            ConnectionId = connectionId;
            UserId = userId;
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            IsOnline = true;
        }
    }
}