/// <reference path="~/Resources/Scripts/cms-grid/CMSGridEnum.js" />
/// <reference path="~/Resources/Scripts/cms-grid/CMSGridUtillity.js" />
/// <reference path="~/Resources/Scripts/css-element-queries/ResizeSensor.js" />
/// <reference path="~/Resources/Scripts/css-element-queries/ElementQueries.js" />

function cmsGrid($) {
   var cmsGridEnum = new CMSGridEnum();
   var cmsGridUtillity = new CMSGridUtillity();

   var CMSGrid = function (element, configuration) {
      var self = this;
      var $element = $(element);

      $element.data("CMSGrid", this);

      this.container = $element;
      this.initialize(configuration);
      this.render();
      
      new ResizeSensor(this.head, function () {
         self.formatBodyRowsCells();
      });  
   };

   CMSGrid.prototype = {
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
      paging: false,
      showPrevNext: false,
      showFirstLast: false,

      initialize: function (configuration) {
         this.source = configuration[cmsGridEnum.SOURCE];
         this.fields = configuration[cmsGridEnum.FIELDS];

         if (cmsGridEnum.VISIBLE_HEAD in configuration) {
            this.visibleHead = configuration[cmsGridEnum.VISIBLE_HEAD];
         }

         if (cmsGridEnum.SORTING in configuration) {
            this.sorting = configuration[cmsGridEnum.SORTING];
         }

         if (cmsGridEnum.FILTERING in configuration) {
            this.filtering = configuration[cmsGridEnum.FILTERING];
         }

         if (cmsGridEnum.MULTIPLE_FIELD_FILTER in configuration) {
            this.multipleFieldFilter = configuration[cmsGridEnum.MULTIPLE_FIELD_FILTER];
         }

         if (cmsGridEnum.ROW_CLICK in configuration) {
            this.rowClickable = configuration[cmsGridEnum.ROW_CLICK];
         }

         if (cmsGridEnum.ROW_ACTION in configuration) {
            this.rowAction = configuration[cmsGridEnum.ROW_ACTION];
         }

         if (cmsGridEnum.CELL_ACTION in configuration) {
            this.cellAction = configuration[cmsGridEnum.CELL_ACTION];
         }

         if (cmsGridEnum.PAGING in configuration) {
            this.paging = configuration[cmsGridEnum.PAGING];
            this.pager = new CMSGridPager();
         }

         if (cmsGridEnum.PREV_NEXT in configuration) {
            this.showPrevNext = configuration[cmsGridEnum.PREV_NEXT];
         }

         if (cmsGridEnum.FIRST_LAST in configuration) {
            this.showFirstLast = configuration[cmsGridEnum.FIRST_LAST];
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

            if (field[cmsGridEnum.STYLE] != "hidden") {
               $headLabels.append(this.createOneHeadLabel(field));
            }
         }         

         return $headLabels;
      },

      createOneHeadLabel: function (field) {
         var self = this;
         var $headLabel = $("<th>");

         $headLabel.text(field[cmsGridEnum.LABEL]);
         $headLabel.addClass("cms-grid-head-label");

         if (this.sorting === true) {
            $headLabel.addClass("cms-grid-head-label-sorting");
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

         $headLabelsChildren.removeClass("cms-grid-head-label-sorted");
         $headLabelsChildren.removeClass("cms-grid-head-label-sorted-asc");
         $headLabelsChildren.removeClass("cms-grid-head-label-sorted-desc");
      },

      setSorterTag: function (headLabel, field) {
         var $headLabel = $(headLabel);

         $headLabel.addClass("cms-grid-head-label-sorted");

         if (this.sorter == undefined || this.sorter == null || this.sorter.name != field[cmsGridEnum.NAME]) {
            this.sorter = {};
            this.sorter.name = field[cmsGridEnum.NAME];
            this.sorter.order = cmsGridEnum.SORT_ASC;
         }
         else {
            if (this.sorter.order == cmsGridEnum.SORT_ASC) {
               this.sorter.order = cmsGridEnum.SORT_DESC;
            }
            else {
               this.sorter.order = cmsGridEnum.SORT_ASC;
            }
         }

         if (this.sorter.order == cmsGridEnum.SORT_ASC) {
            $headLabel.addClass("cms-grid-head-label-sorted-asc");
         }
         else if (this.sorter.order == cmsGridEnum.SORT_DESC) {
            $headLabel.addClass("cms-grid-head-label-sorted-desc");
         }
      },

      createHeadFilters: function () {
         var $headFilters = this.headFilters = $("<tr>");

         for (var i = 0; i < this.fields.length; i++) {
            var field = this.fields[i];

            if (field[cmsGridEnum.STYLE] != "hidden") {
               $headFilters.append(this.createOneHeadFilter(field));
            }
         }

         return $headFilters;
      },

      createOneHeadFilter: function (field) {
         var self = this;
         var $headFilter = $("<td>");

         $headFilter.addClass("cms-grid-td");

         var $headFilterField;

         switch (field[cmsGridEnum.TYPE]) {
            case cmsGridEnum.INT_TYPE:
               $headFilterField = $("<input type='number' />");
               break;
            case cmsGridEnum.STRING_TYPE:
               $headFilterField = $("<input type='text' />");
               break;
            case cmsGridEnum.DATE_TYPE:
               $headFilterField = $("<input type='date' />");
               break;
            default:
               $headFilterField = $("<input type='text' />");
               break;
         }

         $headFilterField.addClass("cms-grid-head-filter-field");
         $headFilterField.data("name", field[cmsGridEnum.NAME]);
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
         var $headFilterFields = $(this.headFilters).find(".cms-grid-head-filter-field");

         $headFilterFields.each(function () {
            if (this != exceptField) {
               $(this).val("");
            }
         });
      },

      clearAllFilterFields: function () {
         var $headFilterFields = $(this.headFilters).find(".cms-grid-head-filter-field");

         $headFilterFields.each(function () {
            $(this).val("");
         });
      },

      filterTable: function () {
         var self = this;
         var $headFilterFields = $(self.headFilters).find(".cms-grid-head-filter-field");
         var fieldDictionary = {};

         self.filter = [];

         for (var i = 0, j = self.fields.length; i < j; i++) {
            var field = self.fields[i]
            var fieldName = field[cmsGridEnum.NAME];

            fieldDictionary[fieldName] = field;
         }

         for (var i = 0, j = $headFilterFields.length; i < j; i++) {
            var $headFilterField = $($headFilterFields[i]);
            var headFilterFieldName = $headFilterField.data("name");

            if (headFilterFieldName in fieldDictionary) {
               var field = fieldDictionary[headFilterFieldName];
               var filterElement = {
                  name: field[cmsGridEnum.NAME],
                  value: $headFilterField.val(),
                  type: field[cmsGridEnum.TYPE]
               };

               if (field[cmsGridEnum.TYPE] == cmsGridEnum.DATE_TYPE) {
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
               var fieldName = field[cmsGridEnum.NAME];
               var bodyRowCellData = bodyRowData[fieldName];

               $bodyRowCell.addClass("cms-grid-td");
               bodyRowCellData = this.formatBodyRowCellData(bodyRowCellData, field);

               if (field[cmsGridEnum.LINK] != undefined && field[cmsGridEnum.LINK] != null) {
                  $bodyRowCell.append("<a href='#'>" + bodyRowCellData + "</a>");
               }
               else {
                  $bodyRowCell.text(bodyRowCellData);
               }

               if (field[cmsGridEnum.STYLE] == "hidden") {
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
               case cmsGridEnum.DATE_TYPE:
                  newBodyRowCellData = cmsGridUtillity.formatRowDate(bodyRowCellData);
                  break;
               case cmsGridEnum.TIME_TYPE:
                  newBodyRowCellData = cmsGridUtillity.formatRowTime(bodyRowCellData);
                  break;
            }
         }

         return newBodyRowCellData;
      },

      formatBodyRowsCells: function () {
         var $headLabels = this.headLabels;
         var $headLabelsCells = $headLabels.children("th.cms-grid-head-label");
         var $body = this.body;
         var $bodyRows = $body.children("tr");

         for (var i = 0; i < $bodyRows.length; i++) {
            var $bodyRow = $($bodyRows[i]);
            var $bodyRowCells = $bodyRow.children("td[style!='display: none;']");

            for (var j = 0; j < $bodyRowCells.length - 1; j++) {
               var $bodyRowCell = $($bodyRowCells[j]);
               var width = $headLabelsCells[j].getBoundingClientRect().width;
               $bodyRowCell.outerWidth(width);
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

         if (fieldDetails[cmsGridEnum.LINK] != undefined && fieldDetails[cmsGridEnum.LINK] != null) {
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
      }
   };

   var createGrid = function (configuration) {
      this.each(function () {
         var $element = $(this);

         new CMSGrid($element, configuration);
      });

      return this;
   };

   return createGrid;
}
