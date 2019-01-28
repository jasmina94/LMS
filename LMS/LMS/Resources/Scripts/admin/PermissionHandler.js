var PermissionHandler = function () {

    this.init = function () {
        initSelectPicker();
        initAssignedButtonHandler();
        initRemoveButtonHandler();
    }

    var prepareData = function (ids, userId) {
        var data = {};
        data["Permissions"] = ids;
        data["User"] = userId;

        return data;
    }

    var sendToRemove = function (ids, userId) {
        var data = prepareData(ids, userId);
        $.ajax({
            url: "/Administration/Administration/PermissionRemove",
            type: "POST",
            data: JSON.stringify(data),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    toastr.success("Successfully removed permissions from user.");
                    window.setTimeout(function () {
                        location.reload();
                    }, 2000);
                } else {
                    toastr.error("Server side error while saving permission changes.");
                }
            }
        });
    }

    var sendToAssign = function (ids, userId) {
        var data = prepareData(ids, userId);
        $.ajax({
            url: "/Administration/Administration/PermissionAssign",
            type: "POST",
            data: JSON.stringify(data),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    toastr.success("Successfully assigned permissions to user.");
                    window.setTimeout(function () {
                        location.reload();
                    }, 2000);
                } else {
                    toastr.error("Server side error while saving permission changes.");
                }
            }
        });
    }

    var buildTable = function (tableId, data) {
        var $table = $("<table>");
        var $thead = $("<thead>");
        var $tbody = $("<tbody>");

        $thead.append("<tr><th></th><th>Id</th><th>Code</th><th>Name</th></tr>");

        for (var i = 0; i < data.length; i++) {
            var permission = data[i];
            var $tr = $("<tr>");
            $tr.attr("id", permission.Id);

            var $tdCheckbox = $("<td></td>");

            var $tdId = $("<td>")
            $tdId.text(permission.Id);

            var $tdName = $("<td>")
            $tdName.text(permission.Name);

            var $tdCode = $("<td>");
            $tdCode.text(permission.Code);

            $tr.append($tdCheckbox);
            $tr.append($tdId);
            $tr.append($tdCode)
            $tr.append($tdName);

            $tbody.append($tr);
        }

        $table.append($thead);
        $table.append($tbody);

        $table.attr("id", tableId);
        $table.addClass("lms-permission-table");
        $table.addClass("display");
        $table.addClass("table");

        return $table;
    };

    var clearCurrentTables = function () {
        var $gridOne = $("#AvailablePermissionsGrid");
        var $gridTwo = $("#AssignedPermissionsGrid");
        var $tableAvailable = $gridOne.find("table");
        var $tableAssigned = $gridTwo.find("table");

        if ($tableAssigned.length > 0) {
            $gridTwo.empty();
        }

        if ($tableAvailable.length > 0) {
            $gridOne.empty();
        }

        $(".lms-permission-grid-label").hide();
    }

    var initSelectBoxHandler = function () {
        $("td.select-checkbox").on("click", function (e) {
            var $td = $(this);
            var id = $td.closest("tr").attr("id");
            console.log(id);
        });
    };

    var initTable = function ($table) {
        $table.DataTable({
            info: false,
            searching: true,
            paging: false,
            columnDefs: [{
                orderable: false,
                className: 'select-checkbox',
                targets: 0
            }],
            select: {                
                style: 'multi',
                selector: 'td:first-child'
            },
            order: [[1, 'asc']]
        });

        initSelectBoxHandler();
    }

    var initSelectPicker = function () {
        var $selectpicker = $(".lms-select-permission-user");
        
        $selectpicker.select2({
            theme: "bootstrap",
            placeholder: "Search...",
            width: "280px",
            allowClear: true,
            ajax: {
                quietMillis: 150,
                url: "/Chat/Chat/GetUsers",
                dataType: 'json',
                type: 'GET',
                data: function (params) {
                    return {
                        searchTerm: params.term || "",
                        pageSize: 20,
                        pageNum: params.page || 1
                    };
                },
                processResults: function (data, page) {
                    var more = (page * 20) < data.Total;
                    return { results: data.Results, more: more };
                }
            }
        });

        $selectpicker.on("select2:select", function (e) {           

            var $selected = $(this);
            var val = $selected.val();
            var userId = val.split(" ")[0];
            var userName = val.split(" ")[1];

            clearCurrentTables();

            $.ajax({
                url: "/Administration/Administration/Permissions/" + userId,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var $assignedTable = buildTable("Assigned", data.Assigned);
                    $("#AssignedPermissionsGrid").append($assignedTable);
                    initTable($assignedTable);

                    var $availableTable = buildTable("Available", data.Available);                    
                    $("#AvailablePermissionsGrid").append($availableTable);
                    initTable($availableTable);
                    
                    $(".lms-permission-grid-label").show();
                }
            });
        });
    };

    var initAssignedButtonHandler = function () {
        var $button = $(".lms-assign-permission-btn");

        $button.on("click", function () {
            var $table = $("table#Available");
            var idsToAssign = [];

            var $tbody = $table.children("tbody");
            var $rows = $tbody.children("tr.selected");
            
            if ($rows.length != 0) {
                for (var i = 0; i < $rows.length; i++) {
                    var $row = $rows[i];
                    var id = $($row).attr("id");
                    idsToAssign.push(id);
                }

                var selectedUserId = $("select.lms-select-permission-user").val().split(" ")[0];
                
                sendToAssign(idsToAssign, selectedUserId);

            } else {
                toastr.error("Select at least one permission to assign to user!");
            }
        });
    }

    var initRemoveButtonHandler = function () {
        var $button = $(".lms-remove-permission-btn");

        $button.on("click", function () {
            var $table = $("table#Assigned");
            var idsToRemove = [];

            var $tbody = $table.children("tbody");
            var $rows = $tbody.children("tr.selected");

            if ($rows.length != 0) {
                for (var i = 0; i < $rows.length; i++) {
                    var $row = $rows[i];
                    var id = $($row).attr("id");
                    idsToRemove.push(id);
                }

                var selectedUserId = $("select.lms-select-permission-user").val().split(" ")[0];

                sendToRemove(idsToRemove, selectedUserId);

            } else {
                toastr.error("Select at least one permission to assign to user!");
            }
        });
    };
}