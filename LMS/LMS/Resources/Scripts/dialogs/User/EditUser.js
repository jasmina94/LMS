/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>

var EditUserDialog = function () {
   this.name = "EditUser";
   this.url = new DialogTypeEnum().USER_EDIT;
}

EditUserDialog.prototype = Object.create(BaseDialog.prototype);

EditUserDialog.prototype.initSpecific = function () { }

EditUserDialog.prototype.initValidator = function () {

   var $form = $(this.container).find("form");

   $form.validate({
      rules: {
         Firstname: "required",
         Lastname: "required",
         Username: "required",
         UserPassword: "required",
         DateOfBirth: {
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
         DateOfBirth: {
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

