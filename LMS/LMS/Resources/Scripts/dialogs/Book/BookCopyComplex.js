/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>
/// <reference path="~/Resources/Scripts/dialogs/DialogTypeEnum.js"/>

var BookCopyComplexDialog = function () {
    this.name = "Book copy";
    this.url = new DialogTypeEnum().BOOK_COPY_COMPLEX;
}
BookCopyComplexDialog.prototype = Object.create(BaseDialog.prototype);

BookCopyComplexDialog.prototype.initSpecific = function () { }

BookCopyComplexDialog.prototype.initValidator = function () {

    var $form = $(this.container).find("form");

    $form.validate({
        rules: {
            BookId: "required"
        },
        messages: {
            BookId: "Select book!"
        }
    });
}