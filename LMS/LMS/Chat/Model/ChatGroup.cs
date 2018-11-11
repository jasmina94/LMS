using System.Collections.Generic;

namespace LMS.Chat.Model
{
    public class ChatGroup
    {
        public string GroupName { get; set; }
        public ChatUser GroupCreator { get; set; }
        public List<ChatUser> GroupUsers { get; set; }
        public List<MessageDetails> GroupMessages { get; set; }

        public ChatGroup(string groupName, ChatUser groupCreator)
        {
            InitLists();
            GroupName = groupName;
            GroupCreator = groupCreator;
        }

        private void InitLists()
        {
            GroupUsers = new List<ChatUser>();
            GroupMessages = new List<MessageDetails>();
        }
    }
}