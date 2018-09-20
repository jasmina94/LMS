/// <reference path="~/Resources/Scripts/lms-ribbon/LMSSidebar.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>

function LMSUsersSidebar() {
   this.base = LMSSidebar;
   this.base();
}
LMSUsersSidebar.prototype = new LMSSidebar();
LMSUsersSidebar.prototype.refresh = function () {
   if (!this.lmsGrid) {
      var $ = jQuery;
      var $lmsGridUsers = $("#UserGrid");
      var lmsGridUsers = $lmsGridUsers.data("LMSGrid");

      this.lmsGrid = lmsGridUsers;
   }

   this.base.prototype.refresh.call(this);
};
LMSUsersSidebar.prototype.filter = function () {
   if (!this.lmsGrid) {
      var $ = jQuery;
      var $lmsGridUsers = $("#UserGrid");
      var lmsGridUsers = $lmsGridUsers.data("LMSGrid");

      this.lmsGrid = lmsGridUsers;
   }

   this.base.prototype.filter.call(this);
};
LMSUsersSidebar.prototype.add = function () {
   var path = "/User/User/Create";
   var data = "";
   var dialog = new DialogFactory().createDialog(path);
   dialog.open(data);
};