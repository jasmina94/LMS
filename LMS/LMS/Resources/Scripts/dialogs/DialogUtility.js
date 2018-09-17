var DialogUtility = function () { };

DialogUtility.prototype.createDialog = function (container) {
    var customTitle = container.find(".TitleDialog").val();
    container.dialog({
        autoOpen: false,
        width: 550,
        resizable: false,
        modal: true,
        title: customTitle,
        close: function (e) {
            enableNavbar();
            //refreshPage(); not necessary right now
        }
    });
};

DialogUtility.prototype.open = function (container) {
    container.dialog("open");
    disableNavbar();
}

function enableNavbar() {
    var links = $(".lms-disable-on-open-dialog");
    $(links).each(function () {
        $(this).css({
            "pointer-events": ""
        });
        $(this).prop("disabled", false);
    });
}

function disableNavbar() {
    var links = $(".lms-disable-on-open-dialog");
    $(links).each(function () {
        $(this).css({
            "pointer-events": "none"
        });
        $(this).prop("disabled", true);
    });
}

function refreshPage() {
    var path = window.location.pathname;
    if (path.indexOf("ShowAll") !== -1) {
        location.reload();
    } else {
        return;
    }
}