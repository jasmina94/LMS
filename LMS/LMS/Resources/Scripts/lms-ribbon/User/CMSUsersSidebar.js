/// <reference path="~/Resources/Scripts/cms-ribbon/CMSSidebar.js" />
/// <reference path="~/Resources/Scripts/dialogs/DialogFactory.js"/>

function CMSUsersSidebar1() {
   this.base = CMSSidebar;
   this.base();
}
CMSUsersSidebar1.prototype = new CMSSidebar();
CMSUsersSidebar1.prototype.refresh = function () {
   if (!this.cmsGrid) {
      var $ = jQuery;
      var $cmsGridUsers = $("#cms-grid-users");
      var cmsGridUsers = $cmsGridUsers.data("CMSGrid");

      this.cmsGrid = cmsGridUsers;
   }

   this.base.prototype.refresh.call(this);
};
CMSUsersSidebar1.prototype.filter = function () {
   if (!this.cmsGrid) {
      var $ = jQuery;
      var $cmsGridUsers = $("#cms-grid-users");
      var cmsGridUsers = $cmsGridUsers.data("CMSGrid");

      this.cmsGrid = cmsGridUsers;
   }

   this.base.prototype.filter.call(this);
};
CMSUsersSidebar1.prototype.add = function () {
   var path = "/Overview/User/Create";
   var data = "";
   var dialog = new DialogFactory().createDialog(path);
   dialog.open(data);
};


function CMSUsersSidebar2() {
   this.base = CMSSidebar;
   this.base();
}
CMSUsersSidebar2.prototype = new CMSSidebar();


function CMSUsersSidebar3() {
   this.base = CMSSidebar;
   this.base();
}
CMSUsersSidebar3.prototype = new CMSSidebar();

function CMSUsersSidebar4() {
   this.base = CMSSidebar;
   this.base();
}
CMSUsersSidebar4.prototype = new CMSSidebar();