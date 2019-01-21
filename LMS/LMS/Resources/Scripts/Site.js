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
    var $adminSidebarItems = $(".AdminSidebarItem");
    var $deleteEBookBtn = $(".lms-ebook-delete-btn");
    var $downloadEBookBtn = $(".lms-ebook-download-btn");
    
    var canDownload = function (ebookId, user) {
        var success = false
        var currentUser = JSON.parse(user);
        var roles = currentUser.Roles;
        var userId = currentUser.UserId;

        if (roles.includes("RoleAdmin") || roles.includes("RoleLibrarian")) {
            success = true;
        } else {
            var postData = new Object();
            postData["EBookId"] = parseInt(ebookId);
            postData["UserId"] = userId;     
            $.ajax({
                url: "/EBook/EBook/AllowDownload",
                type: "POST",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    if (data) {
                        success = true;
                    } else {
                        success = false;
                    }
                }
            });
        }
        return success;
    }

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

    var changeSidebarItemsStyle = function (idToActivate, classToFind) {
        var $sidebarItems = $("." + classToFind);
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
        changeSidebarItemsStyle(id, "ProfileSidebarItem");
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

    $adminSidebarItems.on("click", function (e) {
        e.preventDefault();
        var id = $(this).attr("id");
        changeSidebarItemsStyle(id, "AdminSidebarItem");
        switch (id) {
            case "adminNewUser":
                $(".AdminNewUserContainer").show();
                $(".AdminPermissionContainer").hide();
                break;
            case "admnPermissions":
                $(".AdminNewUserContainer").hide();
                $(".AdminPermissionContainer").show();
                break;
        }
    });

    $deleteEBookBtn.on("click", function (e) {
        e.stopPropagation();
        var $nearestRow = $(this).closest("tr");
        if (!$nearestRow.hasClass("selected")) {
            $nearestRow.addClass("selected");
        }
        var ebookId = $(this).attr("id");
        if (confirm("Do you want to delete selected e-book? [Id: " + ebookId + "]")) {
            $.ajax({
                url: "/EBook/EBook/Delete/" + ebookId,
                type: "GET",
                success: function (data) {
                    if (data.Success) {
                        toastr.success(data.Message);
                        window.setTimeout(function () {
                            location.reload();
                        }, 2800);
                        location.reload();
                    } else {
                        toastr.error(data.Message);
                    }                        
                },
                error: function (XMLHtppRequest, textStatus, errorThrown) {
                    toastr.error("Error making AJAX call: " +
                        XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
                }
            });
        } else {
            $nearestRow.removeClass("selected");
        }
    });   

    $downloadEBookBtn.on("click", function (e) {
        e.stopPropagation();
        var id = $(this).attr("id");
        var ebookId = id.split("|")[0];
        var ebookFilename = id.split("|")[1];
        var currentUser = sessionStorage.getItem("current-user");

        if (!currentUser) {
            alert("You must be signed in to be able to download e-books!");
        } else if (canDownload(ebookId, currentUser)) {
            var aElement = document.createElement("a");
            var fileUrl = "/EBook/EBook/Download/" + ebookId;

            aElement.href = fileUrl;
            aElement.download = ebookFilename;
            aElement.click();
        } else {
            alert("You aren't able to download this e-book. It is in category you're not subscribed to.");
        }
    });
});

