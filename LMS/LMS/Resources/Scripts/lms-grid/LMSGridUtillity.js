/// <reference path="~/Resources/Scripts/lms-grid/LMSGridEnum.js" />

function LMSGridUtillity() {
   
};

LMSGridUtillity.prototype.parseDate = function (rowDate) {
   var parsedDate = new Date(parseInt(rowDate.substr(6)));
   return parsedDate;
};

LMSGridUtillity.prototype.formatDate = function (date) {
   var formatedDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();
   return formatedDate;
};

LMSGridUtillity.prototype.formatRowDate = function (rowDate) {
   var parsedDate = this.parseDate(rowDate);
   var formatedDate = this.formatDate(parsedDate);
   return formatedDate;
}

LMSGridUtillity.prototype.formatRowTime = function (rowTime) {
   var hours = rowTime.Hours;
   var minutes = rowTime.Minutes;
   return hours + ":" + minutes;
}