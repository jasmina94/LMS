/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>

var LanguageDialog = function () {
    this.name = "Language";
    this.url = new DialogTypeEnum().LANGUAGE;
}
LanguageDialog.prototype = Object.create(BaseDialog.prototype);

LanguageDialog.prototype.initSpecific = function () {}

LanguageDialog.prototype.initValidator = function () {

    var $form = $(this.container).find("form");

    $form.validate({
        rules: {
            Name: "required"
        },
        messages: {
            Name: "Name is required!"
        }
    });
}