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
LMSSidebar.prototype.edit = function () {
    if (this.lmsGrid.changeLookByMode("edit")) {
        this.enableCancel();
    } else {
        this.disableCancel();
    }
};
LMSSidebar.prototype.delete = function () {
    if (this.lmsGrid.changeLookByMode("delete")) {
        this.enableCancel();
    } else {
        this.disableCancel();
    }
};
LMSSidebar.prototype.cancel = function () {
    if (this.lmsGrid.changeLookByMode(null)) {
        this.disableCancel();
    } else {
        this.enableCancel();
    }
};
LMSSidebar.prototype.enableCancel = function () {
    var $sidebar = $("ul.sidebar");
        var $sidebarItems = $sidebar.children();
        for (var i = 0; i < $sidebarItems.length; i++) {
            var $item = $($sidebarItems[i]);
            var $action = $item.children()[0];
            var data = $($action).data("sidebar-action");
            if (data == "cancel") {
                $item.removeClass("lms-sidebar-item-disabled");
            } else {
                $item.addClass("lms-sidebar-item-disabled");
            }
        }
};
LMSSidebar.prototype.disableCancel = function () {
    var $sidebar = $("ul.sidebar");
    var $sidebarItems = $sidebar.children();
    for (var i = 0; i < $sidebarItems.length; i++) {
        var $item = $($sidebarItems[i]);
        var $action = $item.children()[0];
        var data = $($action).data("sidebar-action");
        if (data == "cancel") {
            $item.addClass("lms-sidebar-item-disabled");
        } else {
            $item.removeClass("lms-sidebar-item-disabled");
        }
    }
};
