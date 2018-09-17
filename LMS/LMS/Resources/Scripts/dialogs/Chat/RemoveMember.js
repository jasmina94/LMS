/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>
/// <reference path="~/Resources/Scripts/dialogs/DialogTypeEnum.js"/>

var RemoveMemberDialog = function () {
    this.name = "Remove member from group";
    this.url = new DialogTypeEnum().CHAT_REMOVE_MEMBER;
}

RemoveMemberDialog.prototype = Object.create(BaseDialog.prototype);

RemoveMemberDialog.prototype.initSpecific = function () {

    $(this.container).find("#cms-chat-group-member-username").attr("name", "MemberUsername");
}

RemoveMemberDialog.prototype.initValidator = function () {

    var $form = $(this.container).find("form");

    $form.validate({
        rules: {
            MemberUsername: "required"
        },
        messages: {
            MemberUsername: "Member username is required!"
        }
    });
}