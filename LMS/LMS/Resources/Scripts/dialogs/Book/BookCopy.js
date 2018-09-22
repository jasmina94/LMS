/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>
/// <reference path="~/Resources/Scripts/dialogs/DialogTypeEnum.js"/>

var BookCopyDialog = function () {
    this.name = "Book";
    this.url = new DialogTypeEnum().BOOK_COPY;
}
BookCopyDialog.prototype = Object.create(BaseDialog.prototype);

BookCopyDialog.prototype.initSpecific = function () { }

BookCopyDialog.prototype.initValidator = function () {}