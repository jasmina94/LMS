var ChangePasswordDialog = function () {
    this.name = "Change password";
    this.url = new DialogTypeEnum().CHANGE_PASS;
}
ChangePasswordDialog.prototype = Object.create(BaseDialog.prototype);

ChangePasswordDialog.prototype.initSpecific = function () {
    var $changePasswordButton = $(".ChangePasswordBtn");

    $changePasswordButton.on("click", function (e) {
        e.preventDefault();
        var $form = $(this).closest("form");
        var url = $form.attr("action");
        var data = $form.serialize();
        var callback = {
            success: function (data) {
                if (data.Success) {
                    $("#Dialog").dialog("close");
                    var newLocation = location.origin + "/Account";
                    location.href = newLocation;
                } else {
                    toastr.error(data.Message, "Error");
                }
            },
            failure: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error("Error making AJAX call: " +
                    XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
            }
        }

        new AjaxHttpSender().sendPost(url, data, callback);
    });
}

ChangePasswordDialog.prototype.initValidator = function () {

    var $form = $(this.container).find("form");
    $form.validate({
        rules: {
            OldPassword: {
                required: true,
                newPassword: true
            },
            NewPassword: {
                required: true,
                newPassword: true,
                equalPassword: false
            },
            RepeatPassword: {
                required: true,
                equalPassword: true
            }
        },
        messages: {
            OldPassword: {
                required: "Required field!",
                newPassword: "New password can't be the same as old!"
            },
            NewPassword: {
                required: "Required field!",
                newPassword: "New password can't be the same as old!",
                equalPassword: "Passwords don't match!",
            },
            RepeatPassword: {
                required: "Required field!",
                equalPassword: "Passwords don't match!",
            }
        }
    });
}