using Autofac;
using LMS.BusinessLogic.UserManagement.Interfaces;
using LMS.Chat.Model;
using LMS.Infrastructure.Authorization;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Chat
{
    public class LMSChatHub : Hub
    {
        private readonly IUserService userService;
        private readonly ILifetimeScope hubLifetimeScope;

        private static List<ChatGroup> chatGroups = new List<ChatGroup>();
        private static List<ChatUser> connectedChatUsers = new List<ChatUser>();
        private static List<MessageDetails> currentMessages = new List<MessageDetails>();

        public LMSChatHub(ILifetimeScope lifetimeScope)
        {
            hubLifetimeScope = lifetimeScope.BeginLifetimeScope();
            userService = hubLifetimeScope.Resolve<IUserService>();
        }

        public void Connect(UserSessionObject currentUser)
        {
            string connectionId = Context.ConnectionId;
            if (connectedChatUsers.Count(user => user.ConnectionId == connectionId) == 0)
            {
                if (connectedChatUsers.Count(user => user.UserId == currentUser.UserId) == 0)
                {
                    ChatUser chatUser = new ChatUser(connectionId, currentUser.UserId, currentUser.Username, currentUser.Firstname, currentUser.Lastname);
                    connectedChatUsers.Add(chatUser);
                }
            }
        }

        public void Disconnect(string username)
        {
            connectedChatUsers = connectedChatUsers.Where(x => x.Username != username).ToList();
        }

        public void SendMessage(string messageText, string receiverUsername, string senderUsername)
        {
            string connectionId = Context.ConnectionId;
            ChatUser sender = connectedChatUsers.Where(user => user.Username.Equals(senderUsername)).FirstOrDefault();
            ChatUser receiver = connectedChatUsers.Where(user => user.Username.Equals(receiverUsername)).FirstOrDefault();

            if (sender != null && receiver != null)
            {
                currentMessages.Add(LMSChatHelper.GenerateMessageDetails(messageText, sender, receiver));
                Clients.Clients(new List<string> { connectionId, receiver.ConnectionId }).UpdateChat(sender, messageText, "");
            }
            else
            {
                Clients.Caller.ShowMessage("Error while sending message.");
            }
        }

        public void SendGroupMessage(string messageText, string groupName, string senderUsername)
        {
            ChatGroup chatGroup = chatGroups.Where(x => x.GroupName.Equals(groupName)).FirstOrDefault();
            if (chatGroup != null)
            {
                ChatUser messageSender = connectedChatUsers.Where(x => x.Username.Equals(senderUsername)).FirstOrDefault();
                MessageDetails message = LMSChatHelper.GenerateMessageDetails(messageText, messageSender);
                chatGroup.GroupMessages.Add(message);
                List<string> connectionIds = LMSChatHelper.GenerateConnectionIds(chatGroup.GroupUsers);
                Clients.Clients(connectionIds).UpdateChat(messageSender, messageText, chatGroup.GroupName);
            }
            else
            {
                Clients.Caller.ShowMessage("Error while sending message.");
            }
        }

        public void CreateChatGroup(string creatorUsername, string groupName, string message, List<int> memberIds)
        {
            if (LMSChatHelper.IsGroupUnique(chatGroups, groupName))
            {
                ChatUser groupCreator = connectedChatUsers.Where(user => user.Username.Equals(creatorUsername)).FirstOrDefault();
                if (groupCreator != null)
                {
                    ChatGroup chatGroup = new ChatGroup(groupName, groupCreator);
                    MessageDetails messageDetails = LMSChatHelper.GenerateMessageDetails(message, groupCreator);
                    chatGroup.GroupMessages.Add(messageDetails);

                    List<ChatUser> members = LMSChatHelper.GenerateGroupMembers(connectedChatUsers, memberIds);
                    if (!members.Contains(groupCreator))
                        members.Add(groupCreator);

                    chatGroup.GroupUsers = members;

                    chatGroups.Add(chatGroup);

                    List<string> connectionIds = LMSChatHelper.GenerateConnectionIds(members);
                    connectionIds.ForEach(x => Groups.Add(x, groupName));

                    Clients.Clients(connectionIds).UpdateGroup(new { success = true, group = chatGroup, status = "created" });
                }
            }
            else
            {
                Clients.Caller.ShowMessage("Group name is not unique. Please try with another group name!");
            }
        }

        public void GetOnlineUsers()
        {
            Clients.All.UpdateUsersOnline(new { success = true, onlineUsers = connectedChatUsers.Where(user => user.IsOnline).ToArray() });
        }

        public void GetAllGroupsForUser(string username)
        {
            List<ChatGroup> userGroups = chatGroups.Where(group => group.GroupUsers.Any(user => user.Username.Equals(username))).ToList();
            Clients.All.UpdateGroups(new { success = true, groups = userGroups });
        }

        public bool IsOnline(string username, string userId)
        {
            bool isOnline = false;
            ChatUser chatUser = connectedChatUsers.Where(user => user.Username.Equals(username) && user.UserId.ToString().Equals(userId)).FirstOrDefault();
            if (chatUser != null)
            {
                isOnline = chatUser.IsOnline;
            }

            return isOnline;
        }

        public List<MessageDetails> GetMessages(string senderUsername, string receiverUsername)
        {
            List<MessageDetails> messages = new List<MessageDetails>();
            foreach (MessageDetails message in currentMessages)
            {
                if ((message.SenderUsername.Equals(senderUsername) && message.ReceiverUsername.Equals(receiverUsername)) ||
                    (message.SenderUsername.Equals(receiverUsername) && message.ReceiverUsername.Equals(senderUsername)))
                {
                    messages.Add(message);
                }
            }
            messages.OrderBy(message => message.Date);

            return messages;
        }

        public List<MessageDetails> GetGroupMessages(string groupName)
        {
            List<MessageDetails> messageDetails = new List<MessageDetails>();
            ChatGroup chatGroup = chatGroups.Where(x => x.GroupName.Equals(groupName)).FirstOrDefault();
            if (chatGroup != null)
            {
                messageDetails = chatGroup.GroupMessages;
            }
            return messageDetails;
        }

        public string GetMembers(string groupName)
        {
            string result = "";
            StringBuilder stringBuilder = new StringBuilder();
            ChatGroup group = chatGroups.Where(x => x.GroupName.Equals(groupName)).FirstOrDefault();
            if (group != null)
            {
                List<ChatUser> groupMembers = group.GroupUsers;
                groupMembers.ForEach(x => stringBuilder.Append(x.Username + ", "));
                result = stringBuilder.ToString();
                result = result.Substring(0, result.Length - 2);
            }
            return result;
        }

        public ChatGroup GetGroup(string groupName)
        {
            ChatGroup chatGroup = null;
            chatGroup = chatGroups.Where(x => x.GroupName.Equals(groupName)).FirstOrDefault();
            return chatGroup;
        }

        public void AddNewMembers(string groupName, List<int> newMembersIds)
        {
            ChatGroup chatGroup = chatGroups.Where(x => x.GroupName.Equals(groupName)).FirstOrDefault();
            List<string> newConnections = new List<string>();
            string newConnection = "";
            if (chatGroup != null)
            {
                List<ChatUser> members = LMSChatHelper.GenerateGroupMembers(connectedChatUsers, newMembersIds);
                foreach (ChatUser member in members)
                {
                    bool exists = chatGroup.GroupUsers.Any(x => x.UserId.Equals(member.UserId));
                    if (!exists)
                    {
                        chatGroup.GroupUsers.Add(member);
                        newConnection = member.ConnectionId;
                        newConnections.Add(newConnection);
                        Groups.Add(newConnection, groupName);
                        Clients.Client(newConnection).UpdateGroup(new { success = true, group = chatGroup, status = "created" });
                    }
                }
                List<string> connectionIds = LMSChatHelper.GenerateConnectionIds(chatGroup.GroupUsers);
                newConnections.ForEach(x => connectionIds.Remove(x));
                Clients.Clients(connectionIds).UpdateGroup(new { success = true, group = chatGroup, status = "updated" });
            }
            else
            {
                Clients.Caller.ShowMessage("Error while adding new members to group: " + groupName + ".");
            }
        }

        public void RemoveMember(string groupName, string memberUsername)
        {
            ChatGroup chatGroup = chatGroups.Where(x => x.GroupName.Equals(groupName)).FirstOrDefault();
            if (chatGroup != null)
            {
                ChatUser userToRemove = chatGroup.GroupUsers.Where(x => x.Username.Equals(memberUsername)).FirstOrDefault();
                if (userToRemove != null)
                {
                    string removedConnection = userToRemove.ConnectionId;
                    Groups.Remove(removedConnection, groupName);
                    chatGroup.GroupUsers.Remove(userToRemove);

                    List<string> connectionIds = LMSChatHelper.GenerateConnectionIds(chatGroup.GroupUsers);
                    Clients.Clients(connectionIds).UpdateGroup(new { success = true, group = chatGroup, status = "updated" });
                    Clients.Client(removedConnection).RemovedFromGroup(new { success = true, group = chatGroup });
                }
                else
                {
                    Clients.Caller.ShowMessage("User: " + memberUsername + " is not in group: " + groupName + ".");
                }
            }
            else
            {
                Clients.Caller.ShowMessage("Error while removing member from group: " + groupName + ".");
            }
        }
    }
}