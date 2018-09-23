﻿/// <reference path="~/Resources/Scripts/lms-ribbon/LMSSidebar.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>

function LMSBookBorrowedSidebar() {
    this.base = LMSSidebar;
    this.base();
}
LMSBookBorrowedSidebar.prototype = new LMSSidebar();
LMSBookBorrowedSidebar.prototype.refresh = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookBorrowGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");

        this.lmsGrid = lmsGridBooks;
    }

    this.base.prototype.refresh.call(this);
};
LMSBookBorrowedSidebar.prototype.filter = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookBorrowGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");

        this.lmsGrid = lmsGridBooks;
    }

    this.base.prototype.filter.call(this);
};
LMSBookBorrowedSidebar.prototype.add = function () {
    //var path = new DialogTypeEnum().LOAN;
    //var data = "";
    //var dialog = new DialogFactory().createDialog(path);
    //dialog.open(data);
};