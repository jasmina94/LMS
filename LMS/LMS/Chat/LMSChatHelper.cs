using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Chat.Model
{
    public class LMSChatHelper
    {
        public static MessageDetails GenerateMessageDetails(string messageText, ChatUser sender, ChatUser receiver)
        {
            MessageDetails messageDetails = null;

            messageDetails = new MessageDetails
            {
                Message = messageText,
                SenderId = sender.UserId,
                SenderUsername = sender.Username,
                ReceiverId = receiver.UserId,
                ReceiverUsername = receiver.Username,
                Date = DateTime.Now
            };

            return messageDetails;
        }

        public static MessageDetails GenerateMessageDetails(string messageText, ChatUser sender)
        {
            MessageDetails messageDetails = null;

            messageDetails = new MessageDetails
            {
                Message = messageText,
                SenderId = sender.UserId,
                SenderUsername = sender.Username,
                Date = DateTime.Now
            };

            return messageDetails;
        }

        public static bool IsGroupUnique(List<ChatGroup> chatGroups, string groupName)
        {
            bool isUnique = true;
            ChatGroup chatGroup = chatGroups.Where(group => group.GroupName.Equals(groupName)).FirstOrDefault();
            if (chatGroup != null)
                isUnique = false;
            return isUnique;
        }

        public static bool IsUserConnected(List<ChatUser> connectedChatUsers, string username)
        {
            bool connected = false;
            foreach (ChatUser chatUser in connectedChatUsers)
            {
                if (chatUser.Username.Equals(username))
                {
                    connected = true;
                    break;
                }
            }

            return connected;
        }

        public static bool IsUserConnected(List<ChatUser> connectedChatUsers, int userId)
        {
            bool connected = false;
            foreach (ChatUser chatUser in connectedChatUsers)
            {
                if (chatUser.UserId.Equals(userId))
                {
                    connected = true;
                    break;
                }
            }

            return connected;
        }

        public static ChatUser GetConnectedUser(List<ChatUser> connectedChatUsers, string username)
        {
            return connectedChatUsers.Where(x => x.Username.Equals(username)).FirstOrDefault();
        }

        public static ChatUser GetConnectedUser(List<ChatUser> connectedChatUsers, int userId)
        {
            return connectedChatUsers.Where(x => x.UserId.Equals(userId)).FirstOrDefault();
        }

        public static ChatUser GenerateOfflineChatUser(string username)
        {
            ChatUser chatUser = null;
            chatUser = new ChatUser
            {
                ConnectionId = null,
                UserId = 0,
                Firstname = null,
                Lastname = null,
                Username = username,
                IsOnline = false
            };
            return chatUser;
        }

        public static ChatUser GenerateOfflineChatUser(int userId)
        {
            ChatUser chatUser = null;
            chatUser = new ChatUser
            {
                ConnectionId = null,
                UserId = userId,
                Firstname = null,
                Lastname = null,
                Username = null,
                IsOnline = false
            };
            return chatUser;
        }

        public static List<ChatUser> GenerateGroupMembers(List<ChatUser> connectedChatUsers, List<int> userIds)
        {
            List<ChatUser> groupMembers = new List<ChatUser>();
            foreach (int userId in userIds)
            {
                if (IsUserConnected(connectedChatUsers, userId))
                {
                    ChatUser groupMember = GetConnectedUser(connectedChatUsers, userId);
                    groupMembers.Add(groupMember);
                }
                else
                {
                    ChatUser offlineGroupMember = GenerateOfflineChatUser(userId);
                    groupMembers.Add(offlineGroupMember);
                }
            }
            return groupMembers;
        }

        public static List<string> GenerateConnectionIds(List<ChatUser> chatUsers)
        {
            List<string> connectionIds = new List<string>();
            foreach (ChatUser chatUser in chatUsers)
            {
                if (chatUser.ConnectionId != null)
                {
                    connectionIds.Add(chatUser.ConnectionId);
                }
            }
            return connectionIds;
        }
    }
}