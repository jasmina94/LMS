var FormHandler = function () {

    this.init = function () {
        initValidators();
        initSelectpickerHandler();
        initSubmitSaveHandler();
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

    var initBaseFormValidator = function () {
        var $formUpload = $("#UploadEBookForm");
        if ($formUpload.length != 0) {
            $formUpload.validate({
                rules: {
                    File: {
                        required: true,
                        extension: "pdf"
                    }
                },
                messages: {
                    File: {
                        required: "Chosing file is required!",
                        extension: "Wrong file type chosen!"
                    }
                }
            });
        }
    }

    var initMainFormValidator = function () {
        var $formSave = $("#SaveEBookForm1");
        if ($formSave.length != 0) {            
            $formSave.validate({
                rules: {
                    Title: "required",
                    Author: "required",
                    PublicationYear: {
                        required: true,
                        yearIsValid: true
                    },
                    Keywords: {
                        required: true,
                        regex: "^[0-9a-zA-Z]+(,[0-9a-zA-Z]+)*$"
                    },
                    CategoryId: "required",
                    LanguageId: {
                        required: true,
                        serbianIsChosen: true
                    }
                },
                messages: {
                    Title: "Title is required!",
                    Author: "Author is required!",
                    PublicationYear: {
                        required: "Year is required!",
                        yearIsValid: "Publication year is not valid!"
                    },
                    Keywords: {
                        required: "At least one keyword is required!",
                        regex: "Wrong format for keywords"
                    },
                    CategoryId: "Category is required!",
                    LanguageId: {
                        required: "Language is required!",
                        serbianIsChosen: "Currently is only available adding e-books on Serbian lanugage!"
                    }
                }
            });
        }
    }

    var initValidators = function () {
        initBaseFormValidator();
        initMainFormValidator();
    }

    var getFormData = function () {
        var $form = $("#SaveEBookForm1");
        var ebook = new Object();

        ebook["Filename"] = $form.find("input#filename").val();
        ebook["Title"] = $form.find("input#title").val();
        ebook["Author"] = $form.find("input#author").val();
        ebook["PublicationYear"] = $form.find("input#publicationyear").val();
        ebook["Keywords"] = $form.find("input#keywords").val();
        ebook["CategoryId"] = $form.find("select#categoryid").val();
        ebook["LanguageId"] = $form.find("select#languageid").val();

        return ebook;
    }

    var initSubmitSaveHandler = function () {        
        var $saveBtn = $(".SubmitEBookSave");
        var $form = $("form#SaveEBookForm1");

        $saveBtn.on("click", function (e) {
            e.preventDefault();

            if ($form.valid()) {
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
                            var newLocation = location.origin + "/EBook/EBook/ViewOverview";                            
                            toastr.success(data.Message);
                            window.setTimeout(function () {
                               window.location = newLocation
                            }, 2800);
                        } else {
                            toastr.error(data.Message);
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        toastr.error('Error authenitication user!  Status = ' + xhr.status);
                    }
                });
            }
        });
    }
}