var LMSGridPager = function () {

   var self = this;
   var grid = null;
   var tableFooter = null;
   var rowsPerPage = null;
   var numberOfPages = null;
   var statusFrom = null;
   var statusTo = null;

   this.createPager = function (lmsGrid) {
      $(self.tableFooter).empty();
      self.grid = lmsGrid;
      self.rowsPerPage = 20;
      self.statusFrom = 1;

      if (self.rowsPerPage > self.grid.data.length) {
         self.statusTo = self.grid.data.length;
      } else {
         self.statusTo = self.rowsPerPage;
      }      

      self.numberOfPages = Math.ceil(self.grid.data.length / self.rowsPerPage);

      var colspan = lmsGrid.fields.length;
      var $tableRows = lmsGrid.body.children();

      var $tableFooter = self.tableFooter = $("<tfoot>");
      $tableFooter.addClass("lms-grid-footer");

      var $tableFooterRow = $("<tr>");
      var $pagerWrapper = $("<td colspan='" + colspan + "'>");

      $pagerWrapper.addClass("lms-grid-pager-wrapper");

      $pagerWrapper.append(self.createPagerContent());
     
      $tableFooterRow.append($pagerWrapper);
      $tableFooter.append($tableFooterRow);

      $tableRows.hide();
      $tableRows.slice(0, self.rowsPerPage).show();

      return $tableFooter;    
   }

   this.createPagerContent = function () {
      var curr = 0;
      var $pager = self.gridPager = $("<ul>");

      $pager.addClass("lms-grid-pager");
      $pager.data("current", 0);        

      if (self.grid.showFirstLast) {
         $pager.append("<li><a href='#' class='first-link'><span class='glyphicon glyphicon-step-backward'></span></a></li>");
      }

      if (self.grid.showPrevNext) {
         $pager.append("<li><a href='#' class='prev-link'><span class='glyphicon glyphicon-backward'></span></a></li>");
      }

      $pager.append("<li class='lms-grid-pager-label'>Page:</li>");
      $pager.append("<li><input type='text' class='lms-grid-pager-input' value='1'/></li>");
      $pager.append("<li class='lms-grid-pager-label' id='changePageNum'>of " + self.numberOfPages + "</li>");

      if (self.grid.showPrevNext) {
         $pager.append("<li><a href='#' class='next-link'><span class='glyphicon glyphicon-forward'></span></a></li>");
      }

      if (self.grid.showFirstLast) {
         $pager.append("<li><a href='#' class='last-link'><span class='glyphicon glyphicon-step-forward'></a></li>");
      }

      $pager.append(self.createPagesPicker());

      $pager.append(self.createStatusLabel());

      self.setViewForPager();        
      self.setEventHandlersForPager();

      return $pager;
   }

   this.setViewForPager = function () {       
      var currentPage = $(self.gridPager).data("current");
      var lastPage = self.numberOfPages - 1;

      if (currentPage == lastPage) {
         self.disableFuture();
      }

      if (currentPage == 0) {
         self.disablePast();
      }

      if (self.numberOfPages == 1) {
         self.disableAll();
      }
   }

   this.disableAll = function () {
      var $pager = self.gridPager;

      $pager.find("li .next-link").addClass("disabled");
      $pager.find("li .prev-link").addClass("disabled");
      $pager.find("li .first-link").addClass("disabled");
      $pager.find("li .last-link").addClass("disabled");
   }

   this.disablePast = function () {
      var $pager = self.gridPager;

      $pager.find("li .prev-link").addClass("disabled");
      $pager.find("li .first-link").addClass("disabled");
      $pager.find("li .next-link").removeClass("disabled");
      $pager.find("li .last-link").removeClass("disabled");
   }

   this.disableFuture = function () {
      var $pager = self.gridPager;

      $pager.find("li .next-link").addClass("disabled");
      $pager.find("li .last-link").addClass("disabled");
      $pager.find("li .prev-link").removeClass("disabled");
      $pager.find("li .first-link").removeClass("disabled");
   }

   this.setEventHandlersForPager = function () {

      var $pager = self.gridPager;

      $pager.find("li .prev-link").click(function () {        
         self.previousPage();
      });

      $pager.find("li .next-link").click(function () {
         self.nextPage();
      });

      $pager.find("li .first-link").click(function () {
         self.firstPage();
      });

      $pager.find("li .last-link").click(function () {
         self.lastPage();
      });

      $pager.find("li .lms-grid-pager-input").keypress(function (e) {
         if (e.keyCode === 13) {
            var page = this.value;
            if (page <= self.numberOfPages && page > 0) {
               self.jumpToPage(--page);
            }
         }
      });

      $pager.find(".lms-grid-pager-picker").change(function () {
         var selected = parseInt($(".lms-grid-pager-picker option:selected").text());
         var newPagesNumber = Math.ceil(self.grid.data.length / selected);
         self.rowsPerPage = selected;
         self.numberOfPages = newPagesNumber;

         self.jumpToPage(0);
         self.setViewForPager();

         $(self.gridPager).find("#changePageNum").text("of " + newPagesNumber);
         $(self.gridPager).data("current", 0);
      });

   }

   this.firstPage = function () {
      var page = 0;
      this.jumpToPage(page);
   },

   this.previousPage = function () {
      var page = $(self.gridPager).data("current") - 1;         
      this.jumpToPage(page);
   },

   this.nextPage = function() {
      var page = $(self.gridPager).data("current") + 1;
      this.jumpToPage(page);
   },

   this.lastPage = function () {
      var page = self.numberOfPages;
      this.jumpToPage(page-1);
   },

   this.jumpToPage = function (page) {      
      var start = page * self.rowsPerPage;
      var end = start + self.rowsPerPage;
      var $tableRows = self.grid.body.children();

      $tableRows.css("display", "none").slice(start, end).show();

      $(self.gridPager).data("current", page);

      $("li .lms-grid-pager-input").val(++page);

      self.statusFrom = start + 1;
      self.statusTo = start + self.rowsPerPage;
      if (self.statusTo > self.grid.body.children().size()) {
         self.statusTo = self.grid.body.children().size();
      }

      $(self.gridPager).find(".lms-grid-pager-status").text("View " + self.statusFrom + " - " + self.statusTo + " of " + self.grid.body.children().size());

      self.setViewForPager();
   }

   this.createPagesPicker = function () {
      var $picker = self.pagesPicker = $("<select>");

      $picker.addClass("lms-grid-pager-picker");

      $picker.append("<option>10</option>");
      $picker.append("<option>20</option>");
      $picker.append("<option>50</option>");
      $picker.append("<option>100</option>");

      return $picker;
   }

   this.createStatusLabel = function () {
      var rowsNumber = self.grid.body.children().size();
      var $statusLabel = self.statusLabel = $("<span>");

      $statusLabel.addClass("lms-grid-pager-status");
      $statusLabel.text("View " + self.statusFrom + " - " + self.statusTo + " of " + rowsNumber);

      return $statusLabel;
   }   
}