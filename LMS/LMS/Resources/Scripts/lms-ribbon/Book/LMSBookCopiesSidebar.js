/// <reference path="~/Resources/Scripts/lms-ribbon/LMSSidebar.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>

function LMSBookCopiesSidebar() {
    this.base = LMSSidebar;
    this.base();

    this.enableBorrow = function () {
        var $borrowOption = $(".sidebar").find("[data-sidebar-action=loan]").parent();
        $borrowOption.removeClass("lms-sidebar-item-disabled");
    }

    this.disableBorrow = function () {
        var $borrowOption = $(".sidebar").find("[data-sidebar-action=loan]").parent();
        $borrowOption.addClass("lms-sidebar-item-disabled");
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
    var path = new DialogTypeEnum().BOOK_COPY_COMPLEX;
    var data = "";
    var dialog = new DialogFactory().createDialog(path);
    dialog.open(data);
};
LMSBookCopiesSidebar.prototype.loan = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookCopyGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    if (this.lmsGrid.changeLookByMode("loan")) {
        this.enableCancel();
    } else {
        this.disableCancel();
    }
};
LMSBookCopiesSidebar.prototype.delete = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookCopyGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    this.base.prototype.delete.call(this);
};
LMSBookCopiesSidebar.prototype.cancel = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookCopyGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    this.base.prototype.cancel.call(this);
};