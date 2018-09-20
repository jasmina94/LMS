/// <reference path="~/Resources/Scripts/lms-grid/LMSGrid.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/CMSRibbonInitializer.js" />
/// <reference path="~/Resources/Scripts/ajax/AjaxHttpSender.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>
/// <reference path="~/Resources/Scripts/Validator.js"/>

(function ($) {
    $.fn.lmsGrid = lmsGrid($);
    $(window).on("load", function () {
        lmsRibbonInitializer($);
    });
}(jQuery));

$(function () {

    var $dialogOpener = $(".OpenDialog");
    var $container = $("#Dialog");
    var $loginForm = $("#LoginUserForm");

    $dialogOpener.on("click", function (e) {
        e.preventDefault();
        var dialogType = $(this).attr("href");
        var data = "";
        if (dialogType.indexOf("Value") >= 0) {
            data = $(this).attr("id");
            dialogType = dialogType.slice(0, dialogType.lastIndexOf("/"));
        }
        var dialog = new DialogFactory().createDialog(dialogType);
        dialog.open(data);
    });

    $container.on("click", ".saveBtn", function (e) {
        e.preventDefault();
        var $form = $("div.panel-body > form");
        new AjaxHttpSender().saveData($form);
    });

    $loginForm.on("click", ".lms-login-btn", function (e) {
        e.preventDefault();
        var $form = $("div.panel-body > form");
        new AjaxHttpSender().login($form);
    });
});