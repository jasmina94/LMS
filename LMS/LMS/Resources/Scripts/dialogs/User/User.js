/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>

var UserDialog = function () {
    this.name = "User";
    this.url = new DialogTypeEnum().USER;
}
UserDialog.prototype = Object.create(BaseDialog.prototype);

UserDialog.prototype.initSpecific = function () {

    $("#UserFormRole").on("select2:select", function (e) {
        var $selected = $(this);
        var $categoryWrapper = $(".UserFormCategoryWrapper");
        var val = $selected.val();

        if (val == "3") {
            $categoryWrapper.show();
        } else {
            $categoryWrapper.hide();
        }
    });

    $("#UserFormRole").on("select2:unselect", function (e) {
        var $categoryWrapper = $(".UserFormCategoryWrapper");
        $categoryWrapper.hide();
    });
}

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
            },
            RoleId: {
                required: true,
                basicCateogoryIsChosen: true
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
            },
            RoleId: {
                required: "Role is required!",
                basicCateogoryIsChosen: "Category is required for subscriber!"
            }
        }
    });
}


