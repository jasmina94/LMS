/// <reference path="~/Resources/Scripts/lms-ribbon/LMSSidebar.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>

function LMSLanguagesSidebar() {
    this.base = LMSSidebar;
    this.base();
}
LMSLanguagesSidebar.prototype = new LMSSidebar();
LMSLanguagesSidebar.prototype.refresh = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridLanguages = $("#LanguageGrid");
        var lmsGridLanguages = $lmsGridLanguages.data("LMSGrid");

        this.lmsGrid = lmsGridLanguages;
    }

    this.base.prototype.refresh.call(this);
};
LMSLanguagesSidebar.prototype.filter = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridLanguages = $("#LanguageGrid");
        var lmsGridLanguages = $lmsGridLanguages.data("LMSGrid");

        this.lmsGrid = lmsGridLanguages;
    }

    this.base.prototype.filter.call(this);
};
LMSLanguagesSidebar.prototype.add = function () {
    var path = new DialogTypeEnum().LANGUAGE;
    var data = null;
    var dialog = new DialogFactory().createDialog(path);
    dialog.open(data);
};
LMSLanguagesSidebar.prototype.edit = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#LanguageGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    this.lmsGrid.buildCheckboxes(false, "edit");
    this.enableCancel();
};
LMSLanguagesSidebar.prototype.delete = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#LanguageGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    this.lmsGrid.buildCheckboxes(false, "delete");
    this.enableCancel();
};
LMSLanguagesSidebar.prototype.cancel = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridBooks = $("#LanguageGrid");
        var lmsGridBooks = $lmsGridBooks.data("LMSGrid");
        this.lmsGrid = lmsGridBooks;
    }

    this.lmsGrid.changeLookByMode(null);
    this.disableCancel();
};