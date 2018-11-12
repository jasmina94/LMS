var FormHandler = function () {

    this.init = function () {
        initView();
        initValidators();
        initSelectpickerHandler();
        initSubmitSaveHandler();
    }

    var initView = function () {
        var $inputYear = $("#publicationyear");
        var currentYear = new Date().getFullYear();

        $inputYear.attr("max", currentYear);
        $inputYear.attr("min", 0);
    }

    var initSelectpickerHandler = function () {
        var selectpickers = $(".lms-selectpicker");

        $(selectpickers).each(function () {
            var $selectDiv = $(this);
            var $select = $selectDiv.find("select");
            var id = $selectDiv.attr("id");
            var placeHolder =findPlaceHolder(id);
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
    }
    
    var findPlaceHolder = function (id) {

        if (id.indexOf("Category") !== -1) {
            placeholder = "Select category";
        }

        if (id.indexOf("Language") !== -1) {
            placeholder = "Select language";
        }
    }

    var findUrl = function (id) {
        var url = "";
        if (id.indexOf("&") !== -1) {
            url = id.split("&")[0];
        } else {
            url = id;
        }

        return url;
    }

    var initValidators = function () {

        var $formUpload = $("form#UploadEBookForm");

        $formUpload.validate({
            rules: {
                File: "required"
            },
            messages: {
                File: "Chosing file is required!"
            }
        });


        var $formSave = $("form#SaveEBookForm");

        $formUpload.validate({
            rules: {
                Title: "required",
                Author: "required",
                PublicationYear: "required",
                Keywords: "required",
                CategoryId: "required",
                LanguageId: "required"
            },
            messages: {
                Title: "Title is required!",
                Author: "Author is required!",
                PublicationYear: "Year is required!",
                Keywords: "At least one keyword is required!",
                CategoryId: "Category is required!",
                LanguageId: "Language is required!"
            }
        });
    }

    var initSubmitSaveHandler = function () {
        
    }
}