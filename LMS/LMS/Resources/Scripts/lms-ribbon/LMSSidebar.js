function LMSSidebar() {

}

LMSSidebar.prototype.refresh = function () {
   this.lmsGrid.clearAllFilterFields();
   this.lmsGrid.filterTable();
};

LMSSidebar.prototype.filter = function () {
   this.lmsGrid.filterTable();
};

LMSSidebar.prototype.add = function () {
   console.log("add prototype");
};
