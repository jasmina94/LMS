/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>
/// <reference path="~/Resources/Scripts/dialogs/DilogTypeEnum.js"/>

var UserAboutDialog = function () {
   this.name = "About user";
   this.url = new DialogTypeEnum().USER_ABOUT;
}

UserAboutDialog.prototype = Object.create(BaseDialog.prototype);

UserAboutDialog.prototype.initSpecific = function () { }

UserAboutDialog.prototype.initValidator = function () { }