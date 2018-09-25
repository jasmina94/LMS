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
LMSSidebar.prototype.enableCancel = function () {

    var $refreshOption = $(".sidebar").find("[data-sidebar-action=refresh]").parent();
    $refreshOption.addClass("lms-sidebar-item-disabled");

    var $filterOption = $(".sidebar").find("[data-sidebar-action=filter]").parent();
    $filterOption.addClass("lms-sidebar-item-disabled");

    var $addOptions = $(".sidebar").find("[data-sidebar-action=add]").parent();
    $addOptions.addClass("lms-sidebar-item-disabled");

    var $editOption = $(".sidebar").find("[data-sidebar-action=edit]").parent();
    $editOption.addClass("lms-sidebar-item-disabled");

    var $deleteOption = $(".sidebar").find("[data-sidebar-action=delete]").parent();
    $deleteOption.addClass("lms-sidebar-item-disabled");   

    var $cancelOptions = $(".sidebar").find("[data-sidebar-action=cancel]").parent();
    $cancelOptions.removeClass("lms-sidebar-item-disabled");
};
LMSSidebar.prototype.disableCancel = function () {

    var $refreshOption = $(".sidebar").find("[data-sidebar-action=refresh]").parent();
    $refreshOption.removeClass("lms-sidebar-item-disabled");

    var $filterOption = $(".sidebar").find("[data-sidebar-action=filter]").parent();
    $filterOption.removeClass("lms-sidebar-item-disabled");

    var $addOptions = $(".sidebar").find("[data-sidebar-action=add]").parent();
    $addOptions.removeClass("lms-sidebar-item-disabled");

    var $editOption = $(".sidebar").find("[data-sidebar-action=edit]").parent();
    $editOption.removeClass("lms-sidebar-item-disabled");

    var $deleteOption = $(".sidebar").find("[data-sidebar-action=delete]").parent();
    $deleteOption.removeClass("lms-sidebar-item-disabled");

    var $cancelOptions = $(".sidebar").find("[data-sidebar-action=cancel]").parent();
    $cancelOptions.addClass("lms-sidebar-item-disabled");
};
