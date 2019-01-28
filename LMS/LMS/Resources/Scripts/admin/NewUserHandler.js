var NewUserHandler = function () {

    var $form = $("#AdminNewUserForm");

    this.init = function () {
        initValidator();
        initSelectpickerHandler();
        initDatepickerHandler();
        initSubmitSaveHandler();
    }

    var initValidator = function () {
        $form.removeData("validator");
        $form.removeData("unobtrusiveValidation");

        $form.validate({
            rules: {
                Firstname: "required",
                Lastname: "required",
                Username: {
                    required: true,
                    uniqueUsernameProfile: true
                },
                BirthDate: {
                    required: true,
                    date: true,
                },
                Email: {
                    required: true,
                    email: true,
                    uniqueEmailProfile: true
                },
                RoleId: {
                    required: true,
                    cateogoryIsChosen: true
                }
            },
            messages: {
                Firstname: "Firstname is required!",
                Lastname: "Lastname is required!",
                Username: {
                    required: "Username is required!",
                    uniqueUsername: "Username is not unique!"
                },
                BirthDate: {
                    required: "Date of birth is required!",
                    date: "Date of birth must be in date format."
                },
                Email: {
                    required: "Email is required!",
                    email: "Email is not valid!",
                    uniqueEmail: "Email is not unique!"
                },
                RoleId: {
                    required: "Role is required!",
                    cateogoryIsChosen: "Category is required for subscriber!"
                }
            }
        });
    }

    var findPlaceHolder = function (id) {
        var placeholder = "";
        var parts = null;
        var url = id;
        if (url.indexOf("&") !== -1) {
            parts = url.split("&");
            placeholder = parts[1];
        } else {
            placeholder = "Select";
        }

        return placeholder;
    };

    var findUrl = function (id) {
        var url = "";
        if (id.indexOf("&") !== -1) {
            url = id.split("&")[0];
        } else {
            url = id;
        }

        return url;
    };

    var initSelectpickerHandler = function () {

        var selectpickers = $form.find(".lms-selectpicker");
        
        $(selectpickers).each(function () {
            var $selectDiv = $(this);
            var $select = $selectDiv.find("select");
            var id = $selectDiv.attr("id");
            var placeHolder = findPlaceHolder(id);
            var url = findUrl(id);
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

        $("#AdminSelectRole").on("select2:select", function (e) {
            var $selected = $(this);
            var $categoryWrapper = $(".AdminCategoryWrapper");
            var val = $selected.val();

            if (val == "3") {
                $categoryWrapper.show();
            } else {
                $categoryWrapper.hide();
            }
        });

        $("#AdminSelectRole").on("select2:unselect", function (e) {
            var $categoryWrapper = $(".AdminCategoryWrapper");
        });
    }

    var initDatepickerHandler = function () {
        var datepickers = $form.find(".DatePicker");

        $(datepickers).each(function () {
            $(this).datepicker({
                dateFormat: "mm/dd/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "-100:+0",
            });
        });
    }

    var checkCategory = function () {
        var valid = false;
        var $roleSelect = $("#AdminSelectRole");
        var $categorySelect = $("#AdminSelectCategory");
        var roleVal = $roleSelect.val();
        var catVal = $categorySelect.val();

        if (roleVal == "3" && catVal != null && catVal != undefined) {
            valid = true;

        } else if (roleVal != null && roleVal != "3" && catVal == null) {
            valid = true;
        }

        return valid;
    }

    var resetForm = function () {       
        var $roleSelect = $form.find("#AdminSelectRole");
        var $categorySelect = $form.find("#AdminSelectCategory");

        $form[0].reset();
        $roleSelect.empty().trigger("change");
        $categorySelect.empty().trigger("change");
        $(".AdminCategoryWrapper").hide();
    };

    var getFormData = function () {        
        var user = new Object();

        user["Firstname"] = $form.find("#NewUserFirstname").val();
        user["Lastname"] = $form.find("#NewUserLastname").val();
        user["Username"] = $form.find("#NewUserUsername").val();
        user["Email"] = $form.find("#NewUserEmail").val();
        user["BirthDate"] = $form.find("#NewUserBirthDate").val();
        user["RoleId"] = $form.find("select#AdminSelectRole").val();
        user["CategoryId"] = $form.find("select#AdminSelectCategory").val();
        user["UserPassword"] = $form.find("#NewUserPassword").val();
        return user;
    };

    var initSubmitSaveHandler = function () {

        $(".SaveNewUserForm").on("click", function (e) {
            e.preventDefault();
            if ($form.valid()) {
                //if (checkCategory()) {
                    var formData = getFormData();
                    var url = $form.attr("action");
                    $.ajax({
                        url: url,
                        method: "POST",
                        data: JSON.stringify(formData),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.Success) {
                                resetForm();
                                toastr.success(data.Message);
                            } else {
                                toastr.error(data.Message);
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            toastr.error('Error authenitication user!  Status = ' + xhr.status);
                        }
                    });
                //} else {
                //    toastr.error("Category is required for Subscriber role!");
                //}
            }
        });
    }
};