﻿// <reference path= "~/Resources/Scripts/jquery/jquery.validate.js"/>

var Validator = function () { };

Validator.prototype = {

    initialize: function () {
        $.validator.setDefaults({
            errorElement: "span",
            errorClass: "errorTooltipText",
            errorPlacement: function (error, element) {
                var elementParent = element.parent();
                $(elementParent).append('<div id="error' + element.attr("id") + '" class="errorTooltip"></div>');
                error.appendTo($("div#error" + element.attr("id")));
            }
        });

        $.validator.addMethod("equalPassword", function (value, element) {
            return $("#newpassword").val() == $("#repeatpassword").val()
        }, "* New password and repeated should match!");

        $.validator.addMethod("newPassword", function (value, element) {
            return $("#newpassword").val() != $("#oldpassword").val()
        }, "* New password and old password should nor match!");

        $.validator.addMethod("uniqueUsername", function (value, element) {
            var response;
            var data = {};
            data["username"] = value;
            data = JSON.stringify(data);
            var url = "/User/User/CheckUsername";
            $.ajax({
                type: "POST",
                url: url,
                data: data,
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    response = data;
                }
            });
            return response;
        }, "* Username is not unique!");

        $.validator.addMethod("uniqueEmail", function (value, element) {
            var response;
            var data = {};
            data["email"] = value;
            data = JSON.stringify(data);
            var url = "/User/User/CheckEmail"
            $.ajax({
                type: "POST",
                url: url,
                data: data,
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    response = data;
                }
            });
            return response;
        }, "* Email is not unique!");

        $.validator.addMethod("uniqueUsernameProfile", function (value, element) {
            var response;
            var currentUsername = localStorage.getItem("current-user-username");
            if (currentUsername == null) {
                new AjaxHttpSender().getCurrentUser();
                currentUsername = localStorage.getItem("current-user-username");
            }
            if (currentUsername === value) {
                response = true;
            }

            if (!response) {
                var data = {};
                data["username"] = value;
                data = JSON.stringify(data);
                var url = "/User/User/CheckUsername";
                $.ajax({
                    type: "POST",
                    url: url,
                    data: data,
                    dataType: "JSON",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        response = data;
                    }
                });
            }
            
            return response;
        }, "* Username is not unique!");

        $.validator.addMethod("uniqueEmailProfile", function (value, element) {
            var response;
            var currentUserEmail = localStorage.getItem("current-user-email");
            if (currentUserEmail == null) {
                new AjaxHttpSender().getCurrentUser();
                currentUserEmail = localStorage.getItem("current-user-email");
            }
            if (currentUserEmail == value) {
                response = true;
            }

            if (!response) {
                var data = {};
                data["email"] = value;
                data = JSON.stringify(data);
                var url = "/User/User/CheckEmail"
                $.ajax({
                    type: "POST",
                    url: url,
                    data: data,
                    dataType: "JSON",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    success: function (data) {
                        response = data;
                    }
                });
            }
            
            return response;
        }, "* Email is not unique!");

        $.validator.addMethod("yearIsValid", function (value, element) {
            var response;
            var currentYear = new Date().getFullYear();
            var minYear = 1500;
            var value = $("#publicationyear").val();
            var isNumber = /^\d{4}$/.test(value)
            if (isNumber) {
                if (value >= 1500 && value <= currentYear) {
                    response = true;
                } else {
                    response = false;
                }
            } else {
                response = false;
            }
            
            return response;
        }, "* Publication year is not valid!");

        $.validator.addMethod("serbianIsChosen", function (value, element) {
            return $("select#languageid").find("option:selected").val() === "3";
        }, "* Currently is only available adding e-books on Serbian lanugage!");
    }
}

new Validator().initialize();