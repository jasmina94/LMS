/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>

var BookDialog = function () {
    this.name = "Book";
    this.url = new DialogTypeEnum().BOOK;
}
BookDialog.prototype = Object.create(BaseDialog.prototype);

BookDialog.prototype.initSpecific = function () {}

BookDialog.prototype.initValidator = function () {

    var $form = $(this.container).find("form");

    $form.validate({
        rules: {
            Title: "required",
            Author: "required"
        },
        messages: {
            Title: "Title is required!",
            Author: "Author is required!"
        }
    });
}