﻿/// <reference path="~/Resources/Scripts/dialogs/DialogUtility.js"/>

var BaseDialog = function () { throw "Abstact class can't be instantiate." }

BaseDialog.prototype = {
    name: "Base dialog",
    url: null,
    title: null,
    dialogUtil: new DialogUtility(),
    container: null,

    open: function (data) {
        var self = this;
        self.container = $("#Dialog");  
        if (data != "") {
            self.url = self.url + "/" + data;
        }
        $.ajax({
            url: self.url,
            data: JSON.stringify(data),
            success: function (htmlContent) {
                self.container.html(htmlContent);
                self.initialize();
            }
        });
    },

    initialize: function () {
        this.initBase();
        this.initSpecific();
        this.dialogUtil.createDialog(this.container);
        this.dialogUtil.open(this.container);
    },

    initBase: function () {
        this.initUIParts();
        this.initValidator();
    },

    initSpecific: function () {
        console.log("Abstract::specificOpen()");
    },

    initUIParts: function () {
        var self = this;
        var wrapper = $(this.container)[0];
        var datepickers = $(wrapper).find(".DatePicker");
        var timepickers = $(wrapper).find(".TimePicker");
        var selectpickers = $(wrapper).find(".lms-selectpicker");

        $(datepickers).each(function () {
            $(this).datepicker({
                dateFormat: "mm/dd/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "-100:+0",
            });
        });

        $(timepickers).each(function () {
            $(this).datetimepicker({
                format: 'HH:mm'
            });
        });

        $(selectpickers).each(function () {
            var $selectDiv = $(this);
            var $select = $selectDiv.find("select");
            var id = $selectDiv.attr("id");
            var placeHolder = self.findPlaceHolder(id);
            var url = self.findUrl(id);
            var pageSize = 20;

            $select.select2({
                theme: "bootstrap",
                placeholder: placeHolder,
                dropdownParent: $("#Dialog"),
                width: "280px",
                allowClear: true,
                ajax: {
                    quietMillis: 150,
                    url: url,
                    dataType: 'json',
                    type: 'GET',
                    data: function (params) {
                        return {
                            searchTerm: params.term || "",
                            pageSize: pageSize,
                            pageNum: params.page || 1
                        };
                    },
                    processResults: function (data, page) {
                        var more = (page * pageSize) < data.Total;
                        return { results: data.Results, more: more };
                    }
                }
            });
        });
    },

    findPlaceHolder: function (id) {
        var placeholder = "";
        var parts = null;
        var url = id;
        if (url.indexOf("&") !== -1) {
            parts = url.split("&");
            placeholder = parts[1];
        } else {
            if (url.indexOf("Category") !== -1) {
                placeholder = "Select category";
            }
            if (url.indexOf("Language") !== -1) {
                placeholder = "Select language";
            }
            if (url.indexOf("Role") !== -1) {
                placeholder = "Select role";
            }
        }

        return placeholder;
    },

    findUrl: function (id) {
        var url = "";
        if (id.indexOf("&") !== -1) {
            url = id.split("&")[0];
        } else {
            url = id;
        }

        return url;
    },

    initValidator: function () { }
};