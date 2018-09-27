/// <reference path="~/Resources/Scripts/lms-ribbon/LMSSidebar.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>

function LMSCategoriesSidebar() {
    this.base = LMSSidebar;
    this.base();
}
LMSCategoriesSidebar.prototype = new LMSSidebar();
LMSCategoriesSidebar.prototype.refresh = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridCatrgories = $("#CategoryGrid");
        var lmsGridCatrgories = $lmsGridCatrgories.data("LMSGrid");

        this.lmsGrid = lmsGridCatrgories;
    }

    this.base.prototype.refresh.call(this);
};
LMSCategoriesSidebar.prototype.filter = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridCategories = $("#CategoryGrid");
        var lmsGridCategories = $lmsGridCategories.data("LMSGrid");

        this.lmsGrid = lmsGridCategories;
    }

    this.base.prototype.filter.call(this);
};
LMSCategoriesSidebar.prototype.add = function () {
    var path = new DialogTypeEnum().CATEGORY;
    var data = "";
    var dialog = new DialogFactory().createDialog(path);
    dialog.open(data);
};
LMSCategoriesSidebar.prototype.edit = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridCategories = $("#CategoryGrid");
        var lmsGridCategories = $lmsGridCategories.data("LMSGrid");

        this.lmsGrid = lmsGridCategories;
    }
    this.base.prototype.edit.call(this);
};
LMSCategoriesSidebar.prototype.delete = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridCategories = $("#CategoryGrid");
        var lmsGridCategories = $lmsGridCategories.data("LMSGrid");

        this.lmsGrid = lmsGridCategories;
    }
    this.base.prototype.delete.call(this);
};
LMSCategoriesSidebar.prototype.cancel = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridCategories = $("#CategoryGrid");
        var lmsGridCategories = $lmsGridCategories.data("LMSGrid");

        this.lmsGrid = lmsGridCategories;
    }
    this.base.prototype.cancel.call(this);
};