function CMSSidebar() {

}

CMSSidebar.prototype.refresh = function () {
   this.cmsGrid.clearAllFilterFields();
   this.cmsGrid.filterTable();
};

CMSSidebar.prototype.filter = function () {
   this.cmsGrid.filterTable();
};

CMSSidebar.prototype.add = function () {
   console.log("add");
};
