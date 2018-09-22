/// <reference path="~/Resources/Scripts/dialogs/BaseDialog.js"/>

var LoanDialog = function () {
    this.name = "Loan";
    this.url = new DialogTypeEnum().LOAN;
}
LoanDialog.prototype = Object.create(BaseDialog.prototype);

LoanDialog.prototype.initSpecific = function () { }

LoanDialog.prototype.initValidator = function () {}