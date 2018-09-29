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

    var $dialogOpener = $(".lms-open-dialog");
    var $container = $("#Dialog");
    var $loginForm = $("#LoginUserForm");
    var $profileInfoForm = $("#ProfileInfoForm");
    var $changeProfileDataLink = $("a#ChangeProfileData");
    var $cancelChangeProfileDataLink = $("a#CancelChangeProfileData");
    var $changePasswordLink = $("a#ChangePassword");
    var $saveProfileInfo = $(".SaveProfileInfoForm");

    var setRegularSidebar = function () {
        var $sidebar = $("ul.sidebar");
        var $sidebarItems = $sidebar.children();
        for (var i = 0; i < $sidebarItems.length; i++) {
            var $item = $($sidebarItems[i]);
            var $action = $item.children()[0];
            var data = $($action).data("sidebar-action");
            if (data == "cancel") {
                $item.addClass("lms-sidebar-item-disabled");
            } else {
                $item.removeClass("lms-sidebar-item-disabled");
            }
        }
    }

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

    $container.on("click", ".lms-save-button", function (e) {
        e.preventDefault();
        var $form = $("div.panel-body > form");
        new AjaxHttpSender().saveData($form);
    });

    $loginForm.on("click", ".lms-login-btn", function (e) {
        e.preventDefault();
        var $form = $("div.panel-body > form");
        new AjaxHttpSender().login($form);
    });

    $container.on("dialogclose", function (event) {
        var $sidebar = $("ul.sidebar");
        if ($sidebar.length != 0) {
            setRegularSidebar();
            var $lmsGrid = $(".lms-grid-container");
            var lmsGridData = $lmsGrid.data("LMSGrid");
            lmsGridData.changeLookByMode(null);
            lmsGrid.addCounter = 0;
            lmsGrid.editCounter = 0;
            lmsGrid.deleteCounter = 0;
            lmsGrid.loanCounter = 0;
            lmsGrid.restoreCounter = 0;
        }
    });

    $changeProfileDataLink.on("click", function () {
        var $submitButton = $(".SaveProfileInfoForm");
        $submitButton.show();

        $cancelChangeProfileDataLink.show();

        var $inputs = $profileInfoForm.find("input");
        for (var i = 0; i < $inputs.length; i++) {
            var $input = $($inputs[i]);
            var id = $input.attr("id");
            if (id != "role" && id != "category") {
                $input.attr("readonly", false);
            }
        }

        $(this).hide();
    });

    $cancelChangeProfileDataLink.on("click", function () {
        var $submitButton = $(".SaveProfileInfoForm");
        $submitButton.hide();

        $changeProfileDataLink.show();

        var $inputs = $profileInfoForm.find("input");
        for (var i = 0; i < $inputs.length; i++) {
            var $input = $($inputs[i]);
            var id = $input.attr("id");
            if (id != "role" && id != "category") {
                $input.attr("readonly", true);
            }
        }

        $(this).hide();
    });

    $saveProfileInfo.on("click", function (e) {
        e.preventDefault();
        var $form = $("#ProfileInfoForm");
        new AjaxHttpSender().saveProfileData($form);
    });
});