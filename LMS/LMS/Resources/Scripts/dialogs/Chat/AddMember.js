/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>
/// <reference path="~/Resources/Scripts/dialogs/DialogTypeEnum.js"/>

var AddMemberDialog = function () {
    this.name = "Add members to group";
    this.url = new DialogTypeEnum().CHAT_ADD_MEMBERS;
}

AddMemberDialog.prototype = Object.create(BaseDialog.prototype);

AddMemberDialog.prototype.initSpecific = function () {

    var $selectMembers = $(this.container).find("#lms-chat-group-members-select");
    var placeHolder = "Add members...";
    var url = "/Chat/Chat/GetUsers";
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
}

AddMemberDialog.prototype.initValidator = function () {  

    var $form = $(this.container).find("form");

    $form.validate({
        rules: {
            SelectMembers: "required"
        },
        messages: {
            SelectMembers: "Chose at least one new member!"
        }
    });
}