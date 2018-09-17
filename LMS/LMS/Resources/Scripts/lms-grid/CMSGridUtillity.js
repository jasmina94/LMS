/// <reference path="~/Resources/Scripts/cms-grid/CMSGridEnum.js" />

function CMSGridUtillity() {
   
};

CMSGridUtillity.prototype.parseDate = function (rowDate) {
   var parsedDate = new Date(parseInt(rowDate.substr(6)));

   return parsedDate;
};

CMSGridUtillity.prototype.formatDate = function (date) {
   var formatedDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();

   return formatedDate;
};

CMSGridUtillity.prototype.formatRowDate = function (rowDate) {
   var parsedDate = this.parseDate(rowDate);
   var formatedDate = this.formatDate(parsedDate);

   return formatedDate;
}

CMSGridUtillity.prototype.formatRowTime = function (rowTime) {
   var hours = rowTime.Hours;
   var minutes = rowTime.Minutes;
   
   return hours + ":" + minutes;
}