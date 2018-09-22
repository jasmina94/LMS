/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>

var CategoryDialog = function () {
    this.name = "Category";
    this.url = new DialogTypeEnum().CATEGORY;
}
CategoryDialog.prototype = Object.create(BaseDialog.prototype);

CategoryDialog.prototype.initSpecific = function () {

    $(this.container).on("keyup", "#name", function () {

        var name = $("#name").val();
        var code = $("#code");

        if (name.indexOf(" ") >= 0) {
            name = name.split(' ').join("_");
        }

        if (name === "") {
            $(code).val("");
        } else {
            $(code).val("cat_" + name);
        }
    });
}

CategoryDialog.prototype.initValidator = function () {

    var $form = $(this.container).find("form");

    $form.validate({
        rules: {
            Name: "required",
            Code: "required"
        },
        messages: {
            Name: "Name is required!",
            Code: "Code is required!"
        }
    });
}