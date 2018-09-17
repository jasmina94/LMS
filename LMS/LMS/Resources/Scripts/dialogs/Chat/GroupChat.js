/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>
/// <reference path="~/Resources/Scripts/dialogs/DialogTypeEnum.js"/>

var GroupChatDialog = function () {
    this.name = "Group chat";
    this.url = new DialogTypeEnum().CHAT_GROUP;
}

GroupChatDialog.prototype = Object.create(BaseDialog.prototype);

GroupChatDialog.prototype.initSpecific = function () {

    var $selectMembers = $(this.container).find("#cms-chat-group-members-select");    
    var placeHolder = "Add members...";
    var url = "/Chat/Home/GetUsers";
    var pageSize = 20;

    $selectMembers.select2({
        theme: "bootstrap",
        placeholder: placeHolder,
        dropdownParent: $("#Dialog"),
        width: "280px",
        allowClear: true,
        multiple: true,
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

    $selectMembers.attr("name", "SelectMembers");
    $(this.container).find("#cms-chat-group-name").attr("name", "GroupName");
    $(this.container).find("#cms-chat-group-message").attr("name", "FirstMessage");
}

GroupChatDialog.prototype.initValidator = function () {
    
    var $form = $(this.container).find("form");

    $form.validate({
       rules: {
           GroupName: "required",
           FirstMessage: "required",
           SelectMembers: "required"
       },
       messages: {
           GroupName: "Group name is required!",
           FirstMessage: "Message is required!",
           SelectMembers: "Members are required!"
       }
    });
}