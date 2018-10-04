/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>

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
            Username: {
                required: true,
                uniqueUsername: true
            },
            UserPassword: "required",
            BirthDate: {
                required: true,
                date: true,
            },
            Email: {
                required: true,
                email: true,
                uniqueEmail: true
            }
        },
        messages: {
            Firstname: "Firstname is required!",
            Lastname: "Lastname is required!",
            Username: {
                required: "Username is required!",
                uniqueUsername: "Username is not unique!"
            },
            UserPassword: "Password is required!",
            BirthDate: {
                required: "Date of birth is required!",
                date: "Date of birth must be in date format."
            },
            Email: {
                required: "Email is required!",
                email: "Email is not valid!",
                uniqueEmail: "Email is not unique!"
            }
        }
    });
}


