/// <reference path="~/Resources/Scripts/lms-ribbon/LMSSidebar.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>

function LMSBookCopiesSidebar() {
    this.base = LMSSidebar;
    this.base();

    this.changeLookForAddMode = function () {
        var $addOptions = $(".sidebar").find("[data-sidebar-action=add]").parent();
        $addOptions.addClass("lms-sidebar-item-disabled");       

        var $refreshOption = $(".sidebar").find("[data-sidebar-action=refresh]").parent();
        $refreshOption.addClass("lms-sidebar-item-disabled");

        var $filterOption = $(".sidebar").find("[data-sidebar-action=filter]").parent();
        $filterOption.addClass("lms-sidebar-item-disabled");

        var $deleteOption = $(".sidebar").find("[data-sidebar-action=delete]").parent();
        $deleteOption.addClass("lms-sidebar-item-disabled");

        var $cancelOptions = $(".sidebar").find("[data-sidebar-action=cancel]").parent();
        $cancelOptions.removeClass("lms-sidebar-item-disabled");
    }

    this.changeLookForDelete = function () {
        var $addOptions = $(".sidebar").find("[data-sidebar-action=add]").parent();
        $addOptions.addClass("lms-sidebar-item-disabled");        

        var $refreshOption = $(".sidebar").find("[data-sidebar-action=refresh]").parent();
        $refreshOption.addClass("lms-sidebar-item-disabled");

        var $filterOption = $(".sidebar").find("[data-sidebar-action=filter]").parent();
        $filterOption.addClass("lms-sidebar-item-disabled");

        var $deleteOption = $(".sidebar").find("[data-sidebar-action=delete]").parent();
        $deleteOption.addClass("lms-sidebar-item-disabled");

        var $cancelOptions = $(".sidebar").find("[data-sidebar-action=cancel]").parent();
        $cancelOptions.removeClass("lms-sidebar-item-disabled");
    }

    this.changeLookForRegularMode = function () {
        var $addOptions = $(".sidebar").find("[data-sidebar-action=add]").parent();
        $addOptions.removeClass("lms-sidebar-item-disabled");

        var $addOptions = $(".sidebar").find("[data-sidebar-action=cancel]").parent();
        $addOptions.addClass("lms-sidebar-item-disabled");

        var $refreshOption = $(".sidebar").find("[data-sidebar-action=refresh]").parent();
        $refreshOption.removeClass("lms-sidebar-item-disabled");

        var $filterOption = $(".sidebar").find("[data-sidebar-action=filter]").parent();
        $filterOption.removeClass("lms-sidebar-item-disabled");

        var $refreshOption = $(".sidebar").find("[data-sidebar-action=delete]").parent();
        $refreshOption.removeClass("lms-sidebar-item-disabled");
    }
}
LMSBookCopiesSidebar.prototype = new LMSSidebar();
LMSBookCopiesSidebar.prototype.refresh = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookCopyGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }
    this.base.prototype.refresh.call(this);
};
LMSBookCopiesSidebar.prototype.filter = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookCopyGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    this.base.prototype.filter.call(this);
};
LMSBookCopiesSidebar.prototype.add = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookCopyGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    this.lmsGrid.buildCheckboxes(false, "add");
    this.changeLookForAddMode();
};
LMSBookCopiesSidebar.prototype.delete = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookCopyGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    this.lmsGrid.buildCheckboxes(false, "delete");
    this.changeLookForDelete();
};
LMSBookCopiesSidebar.prototype.cancel = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookCopyGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    this.lmsGrid.mode = false;
    this.lmsGrid.filterTable();
    this.changeLookForRegularMode();
};