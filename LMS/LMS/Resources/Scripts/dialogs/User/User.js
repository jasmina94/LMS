﻿/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>

var UserDialog = function () {
   this.name = "User";
   this.url = new DialogTypeEnum().USER;
}
UserDialog.prototype = Object.create(BaseDialog.prototype);

UserDialog.prototype.initSpecific = function () { }

UserDialog.prototype.initValidator = function () {

   var $form = $(this.container).find("form");

   $form.validate({
      rules: {
         Firstname: "required",
         Lastname: "required",
         Username: "required",
         UserPassword: "required",
         BirthDate: {
            required: true,
            date: true,
         },
         Email: {
            required: true,
            email: true,
         }
      },
      messages: {
         Firstname: "Firstname is required!",
         Lastname: "Lastname is required!",
         Username: "Username is required!",
         UserPassword: "Password is required!",
         BirthDate: {
            required: "Date of birth is required!",
            date: "Date of birth must be in date format."
         },
         Email: {
            required: "Email is required!",
            email: "Email is not valid."
         }
      }
   });
}

