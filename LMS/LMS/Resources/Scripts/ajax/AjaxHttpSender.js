﻿/// <reference path="~/Resources/Scripts/lms-ribbon/LMSRibbonEnum.js"/>

var AjaxHttpSender = function () {

    var self = this;

    this.ribbonEnum = new LMSRibbonEnum();

    this.sendGet = function (url, callback) {
        $.ajax({
            url: url,
            type: 'GET',
            success: function (data, textStatus) {
                callback.success(data, textStatus);
            },
            error: function (XMLHtppRequest, textStatus, errorThrown) {
                callback.failure(XMLHtppRequest, textStatus, errorThrown)
            }
        });
    }

    this.sendPost = function (url, data, callback) {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {
                callback.success(data);
            },
            error: function (data, textStatus, errorThrown) {
                callback.failure(data, textStatus, errorThrown);
            }

        });
    }

    this.saveDataAjaxCall = function ($form) {
        var url = $form.attr("action");
        var method = $form.attr("method");
        var data = $form.serialize();
        var id = ("#" + $form.attr("id"));
        var callback = {
            success: function (data) {
                if (data.Success) {
                    toastr.success(data.Message, "Success");
                    if (id != undefined) {
                        $(id)[0].reset();
                    }                    
                    $form.closest("div#Dialog").dialog("close");

                    self.resetRibbon(true, false, true, true, false);
                }
                else {
                    toastr.error(data.Message, "Error");
                    self.resetRibbon(true, false, true, true, false);
                }
            },
            failure: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error("Error making AJAX call: " + XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
            }
        }

        if (method == "post") {
            self.sendPost(url, data, callback);
        }
        else {
            self.sendGet(url, callback);
        }
    }

    this.resetRibbon = function (changeAddCounter, changeDeleteCounter,
        changeEditCounter, changeLoanCounter, changeResetCounter) {
        var self = this;    
        var $sidebar = $("ul.sidebar");

        var id = $sidebar.attr("id");

        switch (id) {
            case self.ribbonEnum.BookSidebar:
                var sidebar = new LMSBooksSidebar();
                sidebar.disableCancel();
                var $lmsGrid = $("#BookGrid");
                if ($lmsGrid != undefined) {
                    self.resetGrid(changeAddCounter, changeDeleteCounter, changeEditCounter,
                        changeLoanCounter, changeResetCounter, "BookGrid");
                }
                break;
            case self.ribbonEnum.BookCopySidebar:
                var sidebar = new LMSBookCopiesSidebar();
                sidebar.disableCancel();
                var $lmsGrid = $("#BookCopyGrid");
                if ($lmsGrid != undefined) {
                    self.resetGrid(changeAddCounter, changeDeleteCounter, changeEditCounter,
                        changeLoanCounter, changeResetCounter, "BookCopyGrid");
                }
                break;
            case self.ribbonEnum.BookBorrowedSidebar:
                var sidebar = new LMSBookBorrowedSidebar();
                sidebar.disableCancel();
                var $lmsGrid = $("#BookBorrowGrid");
                if ($lmsGrid != undefined) {
                    self.resetGrid(changeAddCounter, changeDeleteCounter, changeEditCounter,
                        changeLoanCounter, changeResetCounter, "BookBorrowGrid");
                }
                break;
            case self.ribbonEnum.CategorySidebar:
                var sidebar = new LMSCategoriesSidebar();
                sidebar.disableCancel();
                var $lmsGrid = $("#CategoryGrid");
                if ($lmsGrid != undefined) {
                    self.resetGrid(changeAddCounter, changeDeleteCounter, changeEditCounter,
                        changeLoanCounter, changeResetCounter, "CategoryGrid");
                }
                break;
            case self.ribbonEnum.LanguageSidebar:
                var sidebar = new LMSLanguagesSidebar();
                sidebar.disableCancel();
                var $lmsGrid = $("#LanguageGrid");
                if ($lmsGrid != undefined) {
                    self.resetGrid(changeAddCounter, changeDeleteCounter, changeEditCounter,
                        changeLoanCounter, changeResetCounter, "LanguageGrid");
                }
                break;
            case self.ribbonEnum.UserSidebar:
                var sidebar = new LMSUsersSidebar();
                sidebar.disableCancel();
                var $lmsGrid = $("#UserGrid");
                if ($lmsGrid != undefined) {
                    self.resetGrid(changeAddCounter, changeDeleteCounter, changeEditCounter,
                        changeLoanCounter, changeResetCounter, "UserGrid");
                }
                break;
        }            
    }

    this.resetGrid = function (add, del, edit, loan, restore, gridId) {
        var $lmsGrid = $('#' + gridId);
        var lmsGridData = $lmsGrid.data("LMSGrid");
        lmsGridData.changeLookByMode(null);
        if (del) {
            lmsGridData.deleteCounter = 0;
        }
        if (add) {
            lmsGridData.addCounter = 0;
        }
        if (edit) {
            lmsGridData.editCounter = 0;
        }
        if (loan) {
            lmsGridData.loanCounter = 0;
        }
        if (restore) {
            lmsGridData.restoreCounter = 0;
        }
    }

    this.getCurrentUser = function () {
        $.ajax({
            url: "/Account/GetCurrentUser",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (data) {
                localStorage.setItem("current-user-email", data.Email);
                localStorage.setItem("current-user-username", data.Username);
                sessionStorage.setItem("current-user", JSON.stringify(data));
            }
        });
    }

    this.loginAjaxCall = function ($form) {
        var self = this;
        var url = $form.attr("action");
        var method = $form.attr("method");
        var data = $form.serialize();
        var callback = {
            success: function (data) {
                if (data.Success) {
                    self.getCurrentUser();
                    var location = data.RedirectionUrl;
                    window.location = location;                    
                } else {
                    toastr.error(data.Message, "Error");
                }
            },
            failure: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error("Error making AJAX call: " +
                    XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
            }
        }

        if (method == "post") {
            self.sendPost(url, data, callback);
        } else {
            self.sendGet(url, callback);
        }
    }

    this.saveData = function ($form) {
        if ($form.valid()) {
            self.saveDataAjaxCall($form);
        }
    }

    this.login = function ($form) {
        self.loginAjaxCall($form);
    }

    this.delete = function (path) {
        var callback = {
            success: function (data) {
                if (data.Success) {
                    toastr.success(data.Message, "Success");                   
                } else {
                    if (data.Message == undefined) {
                        toastr.error("Action is not allowed!", "Error");
                    } else {
                        toastr.error(data.Message, "Error");
                    }
                    
                }
                self.resetRibbon(false, true, false, false, false);
            },
            failure: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error("Error making AJAX call: " +
                    XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
            }
        };
        self.sendGet(path, callback);
    }

    this.restore = function (path) {
        var callback = {
            success: function (data) {
                if (data.Success) {
                    toastr.success(data.Message, "Success");                    
                } else {
                    if (data.Message == undefined) {
                        toastr.error("Action is not allowed!", "Error");
                    } else {
                        toastr.error(data.Message, "Error");
                    }
                }
                self.resetRibbon(false, false, false, false, true);
            },
            failure: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error("Error making AJAX call: " +
                    XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
            }
        };
        self.sendGet(path, callback);
    }

    this.saveProfileData = function ($form) {
        var url = $form.attr("action");
        var data = $form.serialize();
        var callback = {
            success: function (data) {
                if (data.Success) {
                    toastr.success(data.Message, "Success");
                    setTimeout(location.reload.bind(location), 2800);
                }
                else {
                    toastr.error(data.Message, "Error");                    
                }
            },
            failure: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error("Error making AJAX call: " + XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
            }
        }
        self.sendPost(url, data, callback);
    }
};

