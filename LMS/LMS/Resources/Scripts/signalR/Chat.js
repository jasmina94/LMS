/// <reference path="~/Resources/Scripts/signalR/ChatEnum.js"/>

var chatHub = null;

var CMSChat = function () {

    var self = this;
    var lmsChatHub = null;
    var loggedInUser = null;

    var $sendBtn = $("#lms-chat-send");
    var $messageAreaBox = $(".lms-chat-msg-area-box");
    var $messageArea = $("#lms-chat-txt-area");
    var $receiverName = $(".lms-chat-receiver-name");
    var $receiverStatus = $(".lms-chat-receiver-status");
    var $createGroup = $(".lms-chat-group-create-btn");
    var $manageGroupMembers = $(".lms-chat-group-manage-members");

    this.initChat = function () {
        self.lmsChatHub = $.connection.lMSChatHub;
        chatHub = self.lmsChatHub;

        self.loggedInUser = JSON.parse(sessionStorage.getItem("current-user"));
        $messageAreaBox.hide();

        self.buildSearchOptions();
        self.setMessageHandler();
        self.setChatHandler();
        self.setGroupChatCreateHandler();
        self.setGroupChatHandler();
        self.setAddMembersHandler();
        self.setRemoveMembersHandler();

        $.connection.hub.start()
            .done(function () {
                self.lmsChatHub.server.connect(self.loggedInUser);
                self.lmsChatHub.server.getOnlineUsers();
                self.lmsChatHub.server.getAllGroupsForUser(self.loggedInUser.Username);
            });
    }

    this.getMessages = function (receiverUsername) {
        var senderUsername = self.loggedInUser.Username;
        $.connection.hub.start()
            .done(function () {
                var messages = self.lmsChatHub.server.getMessages(senderUsername, receiverUsername)
                    .done(function (data) {
                        $(".lms-chat-msg-list").empty();
                        if (data.length !== 0) {
                            for (var i = 0; i < data.length; i++) {
                                var message = (data[i]);
                                var text = message.Message;
                                var messageType = self.getMessageType(message);
                                var senderUsername = message.SenderUsername;
                                self.buildMessage(text, messageType, senderUsername);
                            }
                        }
                    });
            });
    }

    this.getMessageType = function (message) {
        var messageType = null;
        var currentUsername = self.loggedInUser.Username;
        if (currentUsername === message.SenderUsername) {
            messageType = lmsChatEnum.SENDER_MSG;
        } else {
            messageType = lmsChatEnum.RECEIVER_MSG;
        }
        return messageType;
    }

    this.showHideMessages = function (nextReceiver, previousReceiver) {
        var $messageList = $(".lms-chat-msg-list");
        var $messages = $messageList.children("");
        for (var i = 0; i < $messages.length; i++) {
            var $message = $($messages[i]);
            var conversationWith = $message.attr("data-receiver");
            if (conversationWith !== undefined && conversationWith === nextReceiver) {
                $message.removeClass("lms-chat-hide-msg");
                $message.removeAttr("data-receiver");
            } else {
                $message.attr("data-receiver", previousReceiver);
                $message.addClass("lms-chat-hide-msg");
            }
        }
    }

    this.updateReceiver = function (receiver) {
        var status = null;
        var username = receiver.Username;
        var currentReceiverUsername = $receiverName.attr("id");

        if (username !== currentReceiverUsername) {
            var name = receiver.Firstname + " " + receiver.Lastname;
            if (receiver.IsOnline) {
                status = "Online";
            } else {
                status = "Offline";
            }

            $receiverName.attr("id", username);
            $receiverName.text(name);
            $receiverStatus.text(status);
        }
    }

    this.buildMessage = function (msg, type, senderUsername) {
        var $msgBubble = $("<div>");
        var $msgSenderUsername = $("<p>");
        var $newMessage = $("<div>");
        var $msgList = $(".lms-chat-msg-list");

        $msgSenderUsername.text(senderUsername);
        $msgSenderUsername.addClass("lms-chat-msg-sender");

        $newMessage.addClass("lms-chat-msg");
        $newMessage.text(msg);

        if (type === lmsChatEnum.SENDER_MSG) {
            $msgBubble.addClass("lms-chat-msg-bubble-sender");
        } else {
            $msgBubble.addClass("lms-chat-msg-bubble-receiver");
        }

        $msgBubble.append($msgSenderUsername);
        $msgBubble.append($newMessage);

        $msgList.append($msgBubble);
        $msgList.scrollTop($msgList[0].scrollHeight);

        $("#lms-chat-txt-area").val("");
    }

    this.buildUserCard = function (id, username, name) {
        var $cardList = $(".lms-chat-box-list");

        if (!self.checkIfCardExists(id)) {
            var $card = $("<div>");
            var $name = $("<p>");

            $card.attr("class", "lms-chat-user-card");
            $card.attr("id", id);
            $card.attr("data-username", username);

            $name.text(name);
            $card.append($name);
            $cardList.prepend($card);
        }
    }

    this.buildSearchOptions = function () {
        var chat = self;
        var $searchInput = $(".lms-chat-search-user");
        var id = $searchInput.attr("id");
        var url = id.split("&")[0];
        var placeholder = id.split("&")[1];
        var pageSize = 20;

        $searchInput.select2({
            theme: "bootstrap",
            placeholder: placeholder,
            allowClear: true,
            ajax: {
                quietMillis: 150,
                url: url,
                dataType: 'json',
                type: 'GET',
                data: function (params) {
                    return {
                        searchTerm: params.term || "",
                        pageSize: pageSize,
                        pageNum: params.page || 1
                    };
                },
                processResults: function (data, page) {
                    var more = (page * pageSize) < data.Total;
                    return { results: data.Results, more: more };
                }
            }
        });

        $searchInput.on("select2:select", function (e) {
            var selected = $(this).val();
            var name = $(this).text();
            var userId = selected.split(" ")[0];
            var username = selected.split(" ")[1];

            $(".lms-chat-receiver-name").text(name);
            $(".lms-chat-receiver-name").attr("id", username);
            $(".lms-chat-msg-area-box").show();
            $(this).text("");
            $(this).val("");
            chat.buildUserCard(userId, username, name);
            chat.getMessages(username);
            self.updateSelectedChatByUsername(username);

            var status = self.lmsChatHub.server.isOnline(username, userId).done(function (data) {
                if (data) {
                    status = lmsChatEnum.ONLINE;
                } else {
                    status = lmsChatEnum.OFFLINE;
                }
                $(".lms-chat-receiver-status").text("");
                $(".lms-chat-receiver-status").text(status);
            });
        });
    };

    this.setMessageHandler = function () {
        $sendBtn.on("click", function () {
            var message = $messageArea.val();
            var receiverUsername = $(".lms-chat-receiver-name").attr("id");
            if (receiverUsername.indexOf("group|") !== -1) {
                var groupName = receiverUsername.split("|")[1];
                $.connection.hub.start()
                    .done(function () {
                        self.lmsChatHub.server.sendGroupMessage(message, groupName, self.loggedInUser.Username);
                    });
            } else {
                $.connection.hub.start()
                    .done(function () {
                        self.lmsChatHub.server.sendMessage(message, receiverUsername, self.loggedInUser.Username);
                    });
            }
        });

        $messageArea.on("keypress", function (e) {
            var key = e.keyCode;
            if (key === 13) {
                e.preventDefault();
                var message = $messageArea.val();
                var receiverUsername = $(".lms-chat-receiver-name").attr("id");

                if (receiverUsername.indexOf("group|") !== -1) {
                    var groupName = receiverUsername.split("|")[1];
                    $.connection.hub.start()
                        .done(function () {
                            self.lmsChatHub.server.sendGroupMessage(message, groupName, self.loggedInUser.Username);
                        });
                } else {
                    $.connection.hub.start()
                        .done(function () {
                            self.lmsChatHub.server.sendMessage(message, receiverUsername, self.loggedInUser.Username);
                        });
                }
            }
        });

    }

    this.setChatHandler = function () {
        $(document).on("click", "div.lms-chat-user-card", function () {
            var $card = $(this);
            var currentReceiverUsername = $receiverName.attr("id");
            var nextReceiverUsername = $card.attr("data-username");

            self.getMessages(nextReceiverUsername);

            if (currentReceiverUsername !== nextReceiverUsername) {
                var nextReceiverFullName = $card.find(">:first-child").text();
                var nextReceiverId = $card.attr("id");
                var status = self.lmsChatHub.server.isOnline(nextReceiverUsername, nextReceiverId).done(function (data) {
                    if (data) {
                        status = lmsChatEnum.ONLINE;
                    } else {
                        status = lmsChatEnum.OFFLINE;
                    }
                    $receiverStatus.text("");
                    $receiverStatus.text(status);
                });

                $receiverName.text(nextReceiverFullName);
                $receiverName.attr("id", nextReceiverUsername);

                $messageAreaBox.show();
            }
            $manageGroupMembers.empty();
            self.updateSelectedChat($card);
            self.removeNotification($card);
        });
    }

    this.setGroupChatCreateHandler = function () {
        $createGroup.on("click", function () {
            var dialogType = new DialogTypeEnum().CHAT_GROUP;
            var dialog = new DialogFactory().createDialog(dialogType);
            dialog.open("");
        });
    }

    this.setGroupChatHandler = function () {
        $(document).on("click", "div.lms-chat-group-card", function () {
            var $card = $(this);
            var groupId = $card.attr("id");
            var groupName = groupId.split("|")[1];

            $messageAreaBox.show();
            $messageArea.val("");
            $receiverName.text(groupName);
            $receiverName.attr("id", groupId);

            var group = self.lmsChatHub.server.getGroup(groupName)
                .done(function (data) {
                    if (data != null) {
                        var messages = data.GroupMessages;
                        var members = self.createGroupMembersNames(data);
                        $(".lms-chat-msg-list").empty();
                        self.showGroupMessages(messages);
                        $receiverStatus.text("Members: " + members);

                        self.showOptionForAddingMembers(groupName);
                        self.updateSelectedChat($card);
                        self.removeNotification($card);
                        if (self.loggedInUser.Username === data.GroupCreator.Username) {
                            self.showOptionForRemovingMembers(groupName);
                        }
                    }
                });
        });
    }

    this.removeNotification = function (selectCard) {
        var $selectCard = $(selectCard);
        var $children = $selectCard.children();
        if ($children.length > 1) {
            $($children[1]).remove();
        }
    }

    this.updateSelectedChat = function (selectCard) {
        var $selectCard = $(selectCard);
        var $cards = $(".lms-chat-box-list").children();
        for (var i = 0; i < $cards.length; i++) {
            var $card = $($cards[i]);
            if ($card.hasClass("lms-chat-card-selected")) {
                $card.removeClass("lms-chat-card-selected");
                break;
            }
        }
        $selectCard.addClass("lms-chat-card-selected");
    }

    this.updateSelectedChatByUsername = function (username) {
        var $cards = $(".lms-chat-box-list").children();
        var $cardToSelect = null;
        for (var i = 0; i < $cards.length; i++) {
            var $card = $($cards[i]);
            if ($card.attr("data-username") === username) {
                $cardToSelect = $card;
            } else if ($card.hasClass("lms-chat-card-selected")) {
                $card.removeClass("lms-chat-card-selected");
            }
        }
        $cardToSelect.addClass("lms-chat-card-selected");
    }

    this.makeNotificationOnCard = function (sender) {
        var receiverUsername = sender.Username;
        var $cards = $(".lms-chat-box-list").children();
        for (var i = 0; i < $cards.length; i++) {
            var $card = $($cards[i]);
            var usernameOnCard = $card.attr("data-username");
            if (usernameOnCard === receiverUsername) {
                var $cardChildren = $card.children();
                if ($cardChildren.length > 1) {
                    var $notificationParagraph = $card.children(".lms-chat-msg-counter-notification");
                    var $span = $($notificationParagraph.children()[0]);
                    var currentNumber = $span.text();
                    currentNumberInt = parseInt(currentNumber);
                    currentNumberInt = currentNumberInt + 1;
                    $span.text(currentNumberInt);
                } else {
                    var $notification = self.makeNotification();
                    $card.append($notification);
                }
                break;
            }
        }
    }

    this.makeNotificationOnGroupCard = function (groupName) {
        var comparer = "group|" + groupName;
        var $cards = $(".lms-chat-box-list").children();
        for (var i = 0; i < $cards.length; i++) {
            var $card = $($cards[i]);
            var id = $card.attr("id");
            if (comparer === id) {
                var $cardChildren = $card.children();
                if ($cardChildren.length > 1) {
                    var $notificationParagraph = $card.children(".lms-chat-msg-counter-notification");
                    var $span = $($notificationParagraph.children()[0]);
                    var currentNumber = $span.text();
                    currentNumberInt = parseInt(currentNumber);
                    currentNumberInt = currentNumberInt + 1;
                    $span.text(currentNumberInt);
                } else {
                    var $notification = self.makeNotification();
                    $card.append($notification);
                }
                break;
            }
        }
    }

    this.makeNotification = function () {
        var $paragraph = $("<p>");
        var $span = $("<span>");
        $paragraph.addClass("lms-chat-msg-counter-notification");
        $span.addClass("lms-chat-msg-counter-notification-span");
        $span.text("1");
        $paragraph.append($span);
        return $paragraph;
    }

    this.buildGroupCard = function (group, focus) {
        var $cardList = $(".lms-chat-box-list");
        var groupName = group.GroupName;

        var id = "group|" + groupName;

        if (!self.checkIfCardExists(id)) {
            var $card = $("<div>");
            var $text = $("<p>");
            var $name = $("<b>");

            $card.attr("class", "lms-chat-group-card");
            $card.attr("id", id);

            $name.text(groupName);
            $text.text("Group: ");
            $text.append($name);
            $card.append($text);
            $cardList.prepend($card);

            if (focus) {
                self.updateSelectedChat($card);
            }
        }
    }

    this.removeAllGroupCards = function () {
        var $cardList = $(".lms-chat-box-list");
        var $cardListElements = $cardList.children();
        for (var i = 0; i < $cardListElements.length; i++) {
            var $card = $($cardListElements[i]);
            var cardId = $card.attr("id");
            if (cardId.indexOf("group|") !== -1) {
                $("div[id='" + cardId + "']").remove();
            }
        }
    }

    this.removeGroupCard = function (groupName) {
        var compare = "group|" + groupName;
        var $cardList = $(".lms-chat-box-list");
        var $cardListElements = $cardList.children();
        for (var i = 0; i < $cardListElements.length; i++) {
            var $card = $($cardListElements[i]);
            var cardId = $card.attr("id");
            if (cardId === compare) {
                $card.remove();
            }
        }
    }

    this.updateActiveChat = function (groupName) {
        var activeChatId = $receiverName.attr("id");
        var compare = "group|" + groupName;
        if (activeChatId === compare) {
            $messageArea.val("");
            $messageAreaBox.hide();
            $receiverName.text("");
            $receiverName.attr("id", "");
            $receiverStatus.text("");
            $manageGroupMembers.empty();
            $(".lms-chat-msg-list").empty();
        }
    }

    this.createGroupMembersNames = function (group) {
        var groupMembers = "";
        var users = group.GroupUsers;
        for (var i = 0; i < users.length; i++) {
            var user = users[i];
            var username = user.Username;
            groupMembers = groupMembers + username + ", ";
        }
        groupMembers = groupMembers.substring(0, groupMembers.length - 2);

        return groupMembers;
    }

    this.createGroupMembersFromSelection = function (selected) {
        var members = [];
        for (var i = 0; i < selected.length; i++) {
            var selectedParts = selected[i].split(" ");
            var memberId = selectedParts[0];
            members.push(memberId);
        }
        return members;
    }

    this.checkIfCardExists = function (id) {
        var $cardList = $(".lms-chat-box-list");
        var $cardListElements = $cardList.children();
        var exists = false;
        for (var i = 0; i < $cardListElements.length; i++) {
            var cardId = $($cardListElements[i]).attr("id");
            if (cardId === id) {
                exists = true;
                break;
            }
        }
        return exists;
    }

    this.showGroupMessages = function (groupMessages) {
        for (var i = 0; i < groupMessages.length; i++) {
            var message = (groupMessages[i]);
            var text = message.Message;
            var messageType = self.getMessageType(message);
            var senderUsername = message.SenderUsername;
            self.buildMessage(text, messageType, senderUsername);
        }
    }

    this.showOptionForAddingMembers = function (groupId) {
        if ($manageGroupMembers.children().length == 0) {
            var $addMembersParagraph = $("<p>");
            var $addMembersLink = $("<a>");

            $addMembersParagraph.css("margin-bottom", 0);
            $addMembersLink.attr("href", "#");
            $addMembersLink.attr("id", groupId);
            $addMembersLink.attr("class", "lms-chat-group-add-members-link");
            $addMembersLink.text("Add members to group");

            $addMembersParagraph.append($addMembersLink);
            $manageGroupMembers.append($addMembersParagraph);
        }
    }

    this.showOptionForRemovingMembers = function (groupId) {
        if ($manageGroupMembers.children().length <= 1) {
            var $removeMembersParagraph = $("<p>");
            var $removeMembersLink = $("<a>");

            $removeMembersParagraph.css("margin-bottom", 0);
            $removeMembersLink.attr("href", "#");
            $removeMembersLink.attr("id", groupId);
            $removeMembersLink.attr("class", "lms-chat-group-remove-members-link");
            $removeMembersLink.text("Remove members from group");

            $removeMembersParagraph.append($removeMembersLink);
            $manageGroupMembers.append($removeMembersParagraph);
        }
    }

    this.showAllForGroupChat = function (group) {
        var groupCreator = group.GroupCreator;
        var id = "group|" + group.GroupName;
        var messages = group.GroupMessages;

        $(".lms-chat-msg-list").empty();
        self.showGroupMessages(messages)

        $messageAreaBox.show();
        $messageArea.val("");
        $receiverName.text(group.GroupName);
        $receiverName.attr("id", id);
        $receiverStatus.text("Members: " + self.createGroupMembersNames(group));

        self.showOptionForAddingMembers(group.GroupName);
        if (groupCreator.Username === self.loggedInUser.Username) {
            self.showOptionForRemovingMembers(group.GroupName);
        }
    }

    this.setAddMembersHandler = function () {
        $(document).on("click", "a.lms-chat-group-add-members-link", function () {
            var dialogType = new DialogTypeEnum().CHAT_ADD_MEMBERS;
            var dialog = new DialogFactory().createDialog(dialogType);
            dialog.open("");
        });
    }

    this.setRemoveMembersHandler = function () {
        $(document).on("click", "a.lms-chat-group-remove-members-link", function () {
            var dialogType = new DialogTypeEnum().CHAT_REMOVE_MEMBER;
            var dialog = new DialogFactory().createDialog(dialogType);
            dialog.open("");
        });
    }
}


var chat = new CMSChat();
chat.initChat();


chatHub.client.updateUsersOnline = function (data) {
    $(".lms-chat-box-list").empty();
    var loggedUserUsername = chat.loggedInUser.Username;
    if (data.success) {
        var onlineUsers = data.onlineUsers;
        for (var i = 0; i < onlineUsers.length; i++) {
            var onlineUser = onlineUsers[i];
            if (onlineUser.Username !== loggedUserUsername) {
                var name = onlineUser.Firstname + " " + onlineUser.Lastname;
                chat.buildUserCard(onlineUser.UserId, onlineUser.Username, name);
            }
        }
    }
}


chatHub.client.updateGroups = function (data) {
    if (data.success) {
        var userGroups = data.groups;        
        for (var i = 0; i < userGroups.length; i++) {
            var group = userGroups[i];
            chat.buildGroupCard(group, false);
        }
    }
}


chatHub.client.updateGroup = function (data) {
    var $dialog = $("#Dialog");
    var $form = $("form.lms-chat-dialog-form");
    var $selection = $("#lms-chat-group-members-select");
    var focus = false;

    if ($dialog.hasClass("ui-dialog-content")) {
        if ($dialog.dialog("isOpen")) {
            $form.find("input[type=text]").val("");
            $selection.val("");
            $dialog.dialog("close");
            focus = true;
        }
    }

    if (data.success) {
        var group = data.group;
        var status = data.status;
        if (status == "created") {
            chat.buildGroupCard(group, focus);
            if (focus) {
                chat.showAllForGroupChat(group);
            } else {
                chat.makeNotificationOnGroupCard(group.GroupName);
            }
        } else {
            chat.showAllForGroupChat(group);
        }
    }
}


chatHub.client.updateChat = function (sender, message, groupName) {
    if (chat.loggedInUser.Username === sender.Username) {
        chat.buildMessage(message, lmsChatEnum.SENDER_MSG, sender.Username);
    } else {
        var currentReceiver = $(".lms-chat-receiver-name").text();
        var messageSender = sender.Firstname + " " + sender.Lastname;
        if (groupName !== "" && groupName != null) {
            if (currentReceiver === groupName) {
                chat.buildMessage(message, lmsChatEnum.RECEIVER_MSG, sender.Username);
            } else {
                chat.makeNotificationOnGroupCard(groupName);
            }
        } else {
            if (currentReceiver === messageSender) {
                chat.buildMessage(message, lmsChatEnum.RECEIVER_MSG, sender.Username);
            } else {
                chat.makeNotificationOnCard(sender);
            }
        }
    }
}


chatHub.client.showMessage = function (data) {
    var $dialog = $("#Dialog");
    if ($dialog.hasClass("ui-dialog-content")) {
        if ($dialog.dialog("isOpen")) {
            $dialog.dialog("close");
            focus = true;
        }
    }
    alert(data);
}


chatHub.client.removedFromGroup = function (data) {
    if (data.success) {
        var group = data.group;
        chat.removeGroupCard(group.GroupName);
        chat.updateActiveChat(group.GroupName);
    }
    alert("You are removed from group: " + data.group.GroupName);
}

$(document).on("click", ".lms-chat-submit-group", function () {
    if ($("#lms-chat-create-group-form").valid()) {
        var groupCreator = chat.loggedInUser.Username;
        var groupName = $("#lms-chat-group-name").val();
        var message = $("#lms-chat-group-message").val();
        var selectedMembers = $("#lms-chat-group-members-select").val();
        var groupMembers = null;
        if (selectedMembers != null)
            groupMembers = chat.createGroupMembersFromSelection(selectedMembers);

        $.connection.hub.start()
            .done(function () {
                chat.lmsChatHub.server.createChatGroup(groupCreator, groupName, message, groupMembers);
            });
    }
});

$(document).on("click", ".lms-chat-add-members-submit", function () {
    if ($("#lms-chat-add-members-form").valid()) {
        var groupName = $(".lms-chat-receiver-name").text();
        var newMembers = $("#lms-chat-group-members-select").val();
        newMembers = chat.createGroupMembersFromSelection(newMembers);
        $.connection.hub.start()
            .done(function () {
                chat.lmsChatHub.server.addNewMembers(groupName, newMembers);
            });
    }
});

$(document).on("click", ".lms-chat-remove-member-submit", function () {
    if ($("#lms-chat-remove-member-form").valid()) {
        var groupName = $(".lms-chat-receiver-name").text();
        var username = $("#lms-chat-group-member-username").val();
        $.connection.hub.start()
            .done(function () {
                chat.lmsChatHub.server.removeMember(groupName, username);
            });
    }
});