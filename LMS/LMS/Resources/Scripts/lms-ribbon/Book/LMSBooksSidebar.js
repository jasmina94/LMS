/// <reference path="~/Resources/Scripts/lms-ribbon/LMSSidebar.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>

function LMSBooksSidebar() {
    this.base = LMSSidebar;
    this.base();
}
LMSBooksSidebar.prototype = new LMSSidebar();
LMSBooksSidebar.prototype.refresh = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");

        this.lmsGrid = lmsGridBooks;
    }

    this.base.prototype.refresh.call(this);
};
LMSBooksSidebar.prototype.filter = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#BookGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");

        this.lmsGrid = lmsGridBooks;
    }

    this.base.prototype.filter.call(this);
};
LMSBooksSidebar.prototype.add = function () {
    var path = "/Book/Book/Create";
    var data = "";
    var dialog = new DialogFactory().createDialog(path);
    dialog.open(data);
};