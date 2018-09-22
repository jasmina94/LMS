/// <reference path="~/Resources/Scripts/lms-ribbon/LMSSidebar.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>

function LMSBookCopiesSidebar() {
    this.base = LMSSidebar;
    this.base();
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
    var path = new DialogTypeEnum().BOOK_COPY;
    var data = "";
    var dialog = new DialogFactory().createDialog(path);
    dialog.open(data);
};