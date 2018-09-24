/// <reference path="~/Resources/Scripts/lms-grid/LMSGridEnum.js" />
/// <reference path="~/Resources/Scripts/lms-grid/LMSGridUtillity.js" />
/// <reference path="~/Resources/Scripts/css-element-queries/ResizeSensor.js" />
/// <reference path="~/Resources/Scripts/css-element-queries/ElementQueries.js" />

function lmsGrid($) {
    var lmsGridEnum = new LMSGridEnum();
    var lmsGridUtillity = new LMSGridUtillity();

    var LMSGrid = function (element, configuration) {
        var self = this;
        var $element = $(element);

        $element.data("LMSGrid", this);

        this.container = $element;
        this.initialize(configuration);
        this.render();

        new ResizeSensor(this.head, function () {
            self.formatBodyRowsCells();
        });
    };

    LMSGrid.prototype = {
        source: null,
        visibleHead: true,
        sorting: false,
        filtering: false,
        multipleFieldFilter: false,
        responsive: false,
        fields: [],
        data: [],
        sorter: null,
        filter: null,
        sendDateFormat: "yyyy-MM-dd",
        rowClickable: null,
        rowAction: null,
        cellAction: null,
        addAction: null,
        deleteAction: null,
        paging: false,
        showPrevNext: false,
        showFirstLast: false,
        mode: null,
        selectAllBox: false,
        deleteCounter: 0,
        addCounter: 0,

        initialize: function (configuration) {
            this.source = configuration[lmsGridEnum.SOURCE];
            this.fields = configuration[lmsGridEnum.FIELDS];

            if (lmsGridEnum.VISIBLE_HEAD in configuration) {
                this.visibleHead = configuration[lmsGridEnum.VISIBLE_HEAD];
            }

            if (lmsGridEnum.SORTING in configuration) {
                this.sorting = configuration[lmsGridEnum.SORTING];
            }

            if (lmsGridEnum.FILTERING in configuration) {
                this.filtering = configuration[lmsGridEnum.FILTERING];
            }

            if (lmsGridEnum.MULTIPLE_FIELD_FILTER in configuration) {
                this.multipleFieldFilter = configuration[lmsGridEnum.MULTIPLE_FIELD_FILTER];
            }
            if (lmsGridEnum.MODE in configuration) {
                this.mode = configuration[lmsGridEnum.MODE];
            }

            if (lmsGridEnum.SELECTION_ALL in configuration) {
                this.selectAllBox = configuration[lmsGridEnum.SELECTION_ALL];
            }

            if (lmsGridEnum.ROW_CLICK in configuration) {
                this.rowClickable = configuration[lmsGridEnum.ROW_CLICK];
            }

            if (lmsGridEnum.ROW_ACTION in configuration) {
                this.rowAction = configuration[lmsGridEnum.ROW_ACTION];
            }

            if (lmsGridEnum.CELL_ACTION in configuration) {
                this.cellAction = configuration[lmsGridEnum.CELL_ACTION];
            }

            if (lmsGridEnum.ADD_ACTION in configuration) {
                this.addAction = configuration[lmsGridEnum.ADD_ACTION];
            }

            if (lmsGridEnum.DELETE_ACTION in configuration) {
                this.deleteAction = configuration[lmsGridEnum.DELETE_ACTION];
            }

            if (lmsGridEnum.PAGING in configuration) {
                this.paging = configuration[lmsGridEnum.PAGING];
                this.pager = new LMSGridPager();
            }

            if (lmsGridEnum.PREV_NEXT in configuration) {
                this.showPrevNext = configuration[lmsGridEnum.PREV_NEXT];
            }

            if (lmsGridEnum.FIRST_LAST in configuration) {
                this.showFirstLast = configuration[lmsGridEnum.FIRST_LAST];
            }
        },

        loadData: function () {
            var self = this;
            var data = {
                Sorter: self.sorter,
                Filter: self.filter
            };

            $.ajax({
                url: self.source,
                method: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(data),
                success: self.successLoadData.bind(self),
                error: self.errorLoadData.bind(self)
            });
        },

        successLoadData: function (result) {
            this.data = result;
            this.renderBodyData();
            this.renderFooter();
            this.formatBodyRowsCells();
        },

        errorLoadData: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus);
        },

        render: function () {
            var $container = this.container;
            var $table = this.table = $("<table>");

            $container.empty();
            $container.append($table);
            $table.addClass("table table-hover");

            if (this.visibleHead === true) {
                $table.append(this.createHead());
            }

            $table.append(this.createBody());
        },

        createHead: function () {
            var $head = this.head = $("<thead>");

            $head.append(this.createHeadLabels());

            if (this.filtering === true) {
                $head.append(this.createHeadFilters());
            }

            return $head;
        },

        createHeadLabels: function () {
            var $headLabels = this.headLabels = $("<tr>");

            for (var i = 0; i < this.fields.length; i++) {
                var field = this.fields[i];

                if (field[lmsGridEnum.STYLE] != "hidden") {
                    $headLabels.append(this.createOneHeadLabel(field));
                }
            }

            return $headLabels;
        },

        createOneHeadLabel: function (field) {
            var self = this;
            var $headLabel = $("<th>");

            $headLabel.text(field[lmsGridEnum.LABEL]);
            $headLabel.addClass("lms-grid-head-label");

            if (this.sorting === true) {
                $headLabel.addClass("lms-grid-head-label-sorting");
                $headLabel.on("click", function () {
                    self.clearSorterTag();
                    self.setSorterTag(this, field);
                    self.loadData();
                });
            }

            return $headLabel;
        },

        clearSorterTag: function () {
            var $headLabels = this.headLabels;
            var $headLabelsChildren = $headLabels.children();

            $headLabelsChildren.removeClass("lms-grid-head-label-sorted");
            $headLabelsChildren.removeClass("lms-grid-head-label-sorted-asc");
            $headLabelsChildren.removeClass("lms-grid-head-label-sorted-desc");
        },

        setSorterTag: function (headLabel, field) {
            var $headLabel = $(headLabel);

            $headLabel.addClass("lms-grid-head-label-sorted");

            if (this.sorter == undefined || this.sorter == null || this.sorter.name != field[lmsGridEnum.NAME]) {
                this.sorter = {};
                this.sorter.name = field[lmsGridEnum.NAME];
                this.sorter.order = lmsGridEnum.SORT_ASC;
            }
            else {
                if (this.sorter.order == lmsGridEnum.SORT_ASC) {
                    this.sorter.order = lmsGridEnum.SORT_DESC;
                }
                else {
                    this.sorter.order = lmsGridEnum.SORT_ASC;
                }
            }

            if (this.sorter.order == lmsGridEnum.SORT_ASC) {
                $headLabel.addClass("lms-grid-head-label-sorted-asc");
            }
            else if (this.sorter.order == lmsGridEnum.SORT_DESC) {
                $headLabel.addClass("lms-grid-head-label-sorted-desc");
            }
        },

        createHeadFilters: function () {
            var $headFilters = this.headFilters = $("<tr>");

            for (var i = 0; i < this.fields.length; i++) {
                var field = this.fields[i];

                if (field[lmsGridEnum.STYLE] != "hidden") {
                    $headFilters.append(this.createOneHeadFilter(field));
                }
            }

            return $headFilters;
        },

        createOneHeadFilter: function (field) {
            var self = this;
            var $headFilter = $("<td>");

            $headFilter.addClass("lms-grid-td");

            var $headFilterField;

            switch (field[lmsGridEnum.TYPE]) {
                case lmsGridEnum.INT_TYPE:
                    $headFilterField = $("<input type='number' />");
                    break;
                case lmsGridEnum.STRING_TYPE:
                    $headFilterField = $("<input type='text' />");
                    break;
                case lmsGridEnum.DATE_TYPE:
                    $headFilterField = $("<input type='date' />");
                    break;
                default:
                    $headFilterField = $("<input type='text' />");
                    break;
            }

            $headFilterField.addClass("lms-grid-head-filter-field");
            $headFilterField.data("name", field[lmsGridEnum.NAME]);
            $headFilterField.on("keypress", function (e) {
                if (e.which == 13) {
                    if (!self.multipleFieldFilter) {
                        self.clearOtherFilterFields(this);
                    }
                    self.filterTable();
                }
            });

            $headFilter.append($headFilterField);

            return $headFilter;
        },

        clearOtherFilterFields: function (exceptField) {
            var $headFilterFields = $(this.headFilters).find(".lms-grid-head-filter-field");

            $headFilterFields.each(function () {
                if (this != exceptField) {
                    $(this).val("");
                }
            });
        },

        clearAllFilterFields: function () {
            var $headFilterFields = $(this.headFilters).find(".lms-grid-head-filter-field");

            $headFilterFields.each(function () {
                $(this).val("");
            });
        },

        filterTable: function () {
            var self = this;
            var $headFilterFields = $(self.headFilters).find(".lms-grid-head-filter-field");
            var fieldDictionary = {};

            self.filter = [];

            for (var i = 0, j = self.fields.length; i < j; i++) {
                var field = self.fields[i]
                var fieldName = field[lmsGridEnum.NAME];

                fieldDictionary[fieldName] = field;
            }

            for (var i = 0, j = $headFilterFields.length; i < j; i++) {
                var $headFilterField = $($headFilterFields[i]);
                var headFilterFieldName = $headFilterField.data("name");

                if (headFilterFieldName in fieldDictionary) {
                    var field = fieldDictionary[headFilterFieldName];
                    var filterElement = {
                        name: field[lmsGridEnum.NAME],
                        value: $headFilterField.val(),
                        type: field[lmsGridEnum.TYPE]
                    };

                    if (field[lmsGridEnum.TYPE] == lmsGridEnum.DATE_TYPE) {
                        filterElement.format = self.sendDateFormat;
                    }

                    self.filter.push(filterElement);
                }
            }

            self.loadData();
        },

        createBody: function () {
            var $body = this.body = $("<tbody>");

            this.loadData();

            return $body;
        },

        renderBodyData: function () {
            var $body = this.body;
            var self = this;
            var hiddenId = null;

            $body.empty();

            for (var i = 0; i < this.data.length; i++) {
                var $bodyRow = $("<tr>");
                var bodyRowData = this.data[i];

                if (this.rowClickable) {
                    self.onRowSelected($bodyRow, this.data[i].Id);
                }

                for (var j = 0; j < this.fields.length; j++) {
                    var $bodyRowCell = $("<td>");
                    var field = this.fields[j];
                    var fieldName = field[lmsGridEnum.NAME];
                    var bodyRowCellData = bodyRowData[fieldName];

                    $bodyRowCell.addClass("lms-grid-td");
                    bodyRowCellData = this.formatBodyRowCellData(bodyRowCellData, field);

                    if (field[lmsGridEnum.LINK] != undefined && field[lmsGridEnum.LINK] != null) {
                        $bodyRowCell.append("<a href='#'>" + bodyRowCellData + "</a>");
                    }
                    else {
                        $bodyRowCell.text(bodyRowCellData);
                    }

                    if (field[lmsGridEnum.STYLE] == "hidden") {
                        hiddenId = bodyRowData[fieldName];
                        $bodyRowCell.css("display", "none");
                    }

                    self.onCellSelected($bodyRowCell, hiddenId, field);

                    $bodyRow.append($bodyRowCell);
                }

                $body.append($bodyRow);
            }

            
        },

        formatBodyRowCellData: function (bodyRowCellData, fieldDetails) {
            var newBodyRowCellData = bodyRowCellData;

            if (bodyRowCellData == undefined || bodyRowCellData == null) {
                newBodyRowCellData = "";
            }
            else {
                switch (fieldDetails.type) {
                    case lmsGridEnum.DATE_TYPE:
                        newBodyRowCellData = lmsGridUtillity.formatRowDate(bodyRowCellData);
                        break;
                    case lmsGridEnum.TIME_TYPE:
                        newBodyRowCellData = lmsGridUtillity.formatRowTime(bodyRowCellData);
                        break;
                }
            }

            return newBodyRowCellData;
        },

        formatBodyRowsCells: function () {
            var $headLabels = this.headLabels;
            var $headLabelsCells = $headLabels.children("th.lms-grid-head-label");
            var $body = this.body;
            var $bodyRows = $body.children("tr");

            for (var i = 0; i < $bodyRows.length; i++) {
                var $bodyRow = $($bodyRows[i]);
                var $bodyRowCells = $bodyRow.children("td[style!='display: none;']");

                for (var j = 0; j < $bodyRowCells.length - 1; j++) {
                    var $bodyRowCell = $($bodyRowCells[j]);
                    if (!$bodyRowCell.hasClass("lms-grid-td-checkbox")) {
                        var width = $headLabelsCells[j].getBoundingClientRect().width;
                        $bodyRowCell.outerWidth(width);
                    }
                }
            }
        },

        onRowSelected: function (row, id) {
            var self = this;

            $(row).on("click", function (e) {
                e.preventDefault();
                self.rowAction(id);
            });
        },

        onCellSelected: function (cell, id, fieldDetails) {
            var self = this;

            if (fieldDetails[lmsGridEnum.LINK] != undefined && fieldDetails[lmsGridEnum.LINK] != null) {
                $(cell).on("click", function (e) {
                    e.stopPropagation();
                    var data = {
                        "id": id,
                        "field": fieldDetails
                    }
                    self.cellAction(data);
                });
            }
        },

        renderFooter: function () {
            var $body = this.body;

            if (this.paging) {
                $body.after(this.pager.createPager(this));
            }
        },

        buildCheckboxes: function (selectAll, mode) {
            var self = this;            
            var $body = this.body;
            var $head = this.head;

            var $headChildren = $head.children();
            for (var i = 0; i < $headChildren.length; i++) {
                var $headRow = $($headChildren[i]);
                var $firstChild = $($headRow.children()[0]);
                var $headerCheck = null;
                if ($firstChild.hasClass("lms-grid-head-label")) {
                    $headerCheck = self.buildHeaderForCheckBox(true, false);
                } else {
                    $headerCheck = self.buildHeaderForCheckBox(false, selectAll);
                }
                $headRow.prepend($headerCheck);
            }

            var $bodyChildren = $body.children();
            for (var i = 0; i < $bodyChildren.length; i++) {
                var $bodyRow = $($bodyChildren[i]);
                var data = self.data[i];
                var id = data.Id;
                var $bodyCheck = self.buildBodyCheckBox(id);
                self.onCheckboxSelected($bodyCheck);
                $bodyRow.prepend($bodyCheck);
            }

            self.mode = mode;
        },

        removeCheckboxes: function(){
            var self = this;
            self.mode = null;

            $("th.lms-grid-th-checkbox").remove();
            $("td.lms-grid-td-checkbox").remove();
        },

        buildHeaderForCheckBox: function (withColor, withInput) {
            var $headCheckbox = $("<th>");
            $headCheckbox.html("&nbsp;")
            $headCheckbox.css("width", "30px");
            $headCheckbox.addClass("lms-grid-th-checkbox");

            if (withColor)
                $headCheckbox.css("background-color", "#f7ebc2");

            if (withInput) {
                var $checkbox = $("<input>");
                $checkBox.attr("type", "checkbox");
                $checkBox.attr("class", "lms-grid-checkbox");
                $checkBox.attr("id", "CheckboxAll");
            }

            return $headCheckbox;
        },

        buildBodyCheckBox: function (dataId) {
            var $bodyCheckbox = $("<td>");
            var $checkBox = $("<input>");

            $checkBox.attr("type", "checkbox");
            $checkBox.attr("class", "lms-grid-input-checkbox");
            $checkBox.attr("id", dataId);

            $bodyCheckbox.append($checkBox);           
            $bodyCheckbox.addClass("lms-grid-td")
            $bodyCheckbox.addClass("lms-grid-td-checkbox");
            $bodyCheckbox.css("width", "30px");

            return $bodyCheckbox;
        },

        onCheckboxSelected: function(copyId){
            var self = this;
            
            $(".lms-grid-input-checkbox").change(function () {
                if (this.checked) {
                    var id = $(this).attr("id");

                    if (self.mode == "add") {
                        if (self.addCounter == 0) {
                            self.addCounter = self.addCounter + 1;
                            self.addAction(id);                            
                        }
                        
                    } else if (self.mode == "delete") {
                        if (self.deleteCounter == 0) {
                            self.deleteCounter = self.deleteCounter + 1;
                            self.deleteAction(id);                            
                        }
                        
                    }                   
                }
            });
        },

        changeLookByMode: function () {
            var self = this;
            if (self.mode == "add") {
                self.buildCheckboxes(self.selectAllBox, "add");
            } else if (self.mode == "delete") {
                self.buildCheckboxes(self.selectAllBox, "delete");
            } else {
                self.removeCheckboxes();
                self.loadData();
            }
        },
    };

    var createGrid = function (configuration) {
        this.each(function () {
            var $element = $(this);
            new LMSGrid($element, configuration);
        });

        return this;
    };

    return createGrid;
}
