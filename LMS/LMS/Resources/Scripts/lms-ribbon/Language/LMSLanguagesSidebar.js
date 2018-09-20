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
    var path = "/Language/Language/Create";
    var data = "";
    var dialog = new DialogFactory().createDialog(path);
    dialog.open(data);
};