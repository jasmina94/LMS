// <reference path= "~/Resources/Scripts/jquery/jquery.validate.js"/>

var Validator = function () { };

Validator.prototype = {

    initialize: function () {
        $.validator.setDefaults({
            errorElement: "span",
            errorClass: "errorTooltipText",
            errorPlacement: function (error, element) {
                var elementParent = element.parent();
                $(elementParent).append('<div id="error' + element.attr("id") + '" class="errorTooltip"></div>');
                error.appendTo($("div#error" + element.attr("id")));
            }
        });
    }
}

new Validator().initialize();