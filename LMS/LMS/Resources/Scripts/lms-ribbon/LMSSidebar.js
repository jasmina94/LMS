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
LMSSidebar.prototype.select = function (all) {
    this.lmsGrid.buildCheckboxes(all);
};
LMSSidebar.prototype.cancel = function () {
    console.log("implement");
}
