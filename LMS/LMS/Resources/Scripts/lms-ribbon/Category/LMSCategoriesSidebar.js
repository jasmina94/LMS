/// <reference path="~/Resources/Scripts/lms-ribbon/CMSSidebar.js" />
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
   var path = "/Category/Category/Create";
   var data = "";
   var dialog = new DialogFactory().createDialog(path);
   dialog.open(data);
};