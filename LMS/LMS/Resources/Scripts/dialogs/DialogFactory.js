/// <reference path="~/Resources/Scripts/dialogs/DialogTypeEnum.js"/>
/// <reference path="~/Resources/Scripts/dialogs/User/User.js"/>
/// <reference path="~/Resources/Scripts/dialogs/User/EditUser.js"/>

/// <reference path="~/Resources/Scripts/dialogs/Category/Category.js"/>

/// <reference path="~/Resources/Scripts/dialogs/Language/Language.js"/>

/// <reference path="~/Resources/Scripts/dialogs/Book/Book.js"/>

/// <reference path="~/Resources/Scripts/dialogs/User/About.js"/>
/// <reference path="~/Resources/Scripts/dialogs/Chat/GroupChat.js"/>
/// <reference path="~/Resources/Scripts/dialogs/Chat/AddMember.js"/>
/// <reference path="~/Resources/Scripts/dialogs/Chat/RemoveMember.js"/>

var DialogFactory = function () {
    var dialogDictionary = {};
    var dialogTypeEnum = new DialogTypeEnum();

    dialogDictionary[dialogTypeEnum.USER] = new UserDialog();
    dialogDictionary[dialogTypeEnum.USER_EDIT] = new EditUserDialog();
    dialogDictionary[dialogTypeEnum.USER_ABOUT] = new UserAboutDialog();

    dialogDictionary[dialogTypeEnum.CATEGORY] = new CategoryDialog();

    dialogDictionary[dialogTypeEnum.LANGUAGE] = new LanguageDialog();

    dialogDictionary[dialogTypeEnum.BOOK] = new BookDialog();

    dialogDictionary[dialogTypeEnum.CHAT_GROUP] = new GroupChatDialog();
    dialogDictionary[dialogTypeEnum.CHAT_ADD_MEMBERS] = new AddMemberDialog();
    dialogDictionary[dialogTypeEnum.CHAT_REMOVE_MEMBER] = new RemoveMemberDialog();


    this.createDialog = function (type) {
        var dialog;
        dialog = dialogDictionary[type];
        return dialog;
    }
}
