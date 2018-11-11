/// <reference path="~/Resources/Scripts/lms-grid/LMSGrid.js" />
/// <reference path="~/Resources/Scripts/lms-ribbon/CMSRibbonInitializer.js" />
/// <reference path="~/Resources/Scripts/ajax/AjaxHttpSender.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>
/// <reference path="~/Resources/Scripts/dialogs/DialogTypeEnum.js"/>
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
    var $changeProfileDataLink = $("a#ChangeProfileData");
    var $cancelChangeProfileDataLink = $("a#CancelChangeProfileData");
    var $changePasswordLink = $("a#ChangePassword");
    var $saveProfileInfo = $(".SaveProfileInfoForm");
    var $changePasswordLink = $("a#ChangePassword");
    var $profileInfoForm = $("#ProfileInfoForm");
    var $logoutLink = $(".lms-logout-link");
    var $profileSidebarItems = $(".ProfileSidebarItem");

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
    };

    var changeSidebarItemsStyle = function (idToActivate) {
        var $sidebarItems = $(".ProfileSidebarItem");
        for (var i = 0; i < $sidebarItems.length; i++) {
            var $item = $($sidebarItems[i]);
            var id = $item.attr("id");
            if (id === idToActivate) {
                $item.addClass("ActiveSidebarItem");
            } else {
                $item.removeClass("ActiveSidebarItem");
            }
        }
    };

    $dialogOpener.on("click", function (e) {
        e.preventDefault();
        var dialogType = $(this).attr("href");
        var data = "";
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
            if (id != "roleProfile" && id != "categoryProfile") {
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
        if ($form.valid())
            new AjaxHttpSender().saveProfileData($form);
    });

    $changePasswordLink.on("click", function () {
        var path = new DialogTypeEnum().CHANGE_PASS;
        var data = "";
        var dialog = new DialogFactory().createDialog(path);
        dialog.open(data);
    })

    $logoutLink.on("click", function (e) {
        var lmsChatHub = $.connection.lMSChatHub;
        var currentUsername = localStorage.getItem("current-user-username");
        lmsChatHub.server.disconnect(currentUsername);

        localStorage.removeItem("current-user-username");
        localStorage.removeItem("current-user-email");
        sessionStorage.removeItem("current-user");
        
    });

    $profileSidebarItems.on("click", function (e) {
        e.preventDefault();
        var id = $(this).attr("id");
        changeSidebarItemsStyle(id);
        switch (id) {
            case "profileInfo":                
                $(".ProfileMainContainer").show();
                $(".ProfileCurrentlyLoanContainer").hide();
                $(".ProfileHistoryLoanContainer").hide();
                break;
            case "currentlyLoan":
                $(".ProfileMainContainer").hide();
                $(".ProfileCurrentlyLoanContainer").show();
                $(".ProfileHistoryLoanContainer").hide();
                break;
            case "historyLoan":
                $(".ProfileMainContainer").hide();
                $(".ProfileCurrentlyLoanContainer").hide();
                $(".ProfileHistoryLoanContainer").show();
                break;
            case "chat":
                var newLocation = location.origin + "/Chat/Chat";
                location.href = newLocation;
        }
    });
});

