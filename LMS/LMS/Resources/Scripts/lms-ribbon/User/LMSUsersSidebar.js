﻿/// <reference path="~/Resources/Scripts/lms-ribbon/LMSSidebar.js" />
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
    var path = new DialogTypeEnum().USER;
    var data = "";
    var dialog = new DialogFactory().createDialog(path);
    dialog.open(data);
};
LMSUsersSidebar.prototype.edit = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridUsers = $("#UserGrid");
        var lmsGridUsers = $lmsGridUsers.data("LMSGrid");

        this.lmsGrid = lmsGridUsers;
    }

    this.base.prototype.edit.call(this);
};
LMSUsersSidebar.prototype.delete = function () {
    if (!this.lmsGrid) {
        var $ = jQuery;
        var $lmsGridUsers = $("#UserGrid");
        var lmsGridUsers = $lmsGridUsers.data("LMSGrid");

        this.lmsGrid = lmsGridUsers;
    }

    this.base.prototype.delete.call(this);
};