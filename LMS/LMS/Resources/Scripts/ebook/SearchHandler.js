var SearchHandler = function () {
    this.init = function (singleSelectVal) {
        initView(singleSelectVal);
        initValidators();
        initResultTable();        
        initSFSSearchButtonHandler();
        initMFSSearchButtonHandler();
    }

    var initView = function (selectSingle) {
        if (selectSingle != "") {
            var $singleSelectpicker = $("select#sfsFieldName");
            $singleSelectpicker.val(selectSingle);
        }
    }

    var initResultTable = function () {       
        var table = $(".lms-ebook-table").DataTable({
            "pagingType": "full_numbers",
            "scrollY": "50vh",
            "scrollCollapse": true,
        });

        $(".lms-ebook-table tbody").on("click", "tr", function () {
            if ($(this).hasClass("selected")) {
                $(this).removeClass("selected");
            }
            else {
                table.$("tr.selected").removeClass("selected");
                $(this).addClass("selected");
            }
        });
    }

    var initSFSFormValidator = function () {
        var $searchSFSForm = $(".lms-sfs-form");
        if ($searchSFSForm.length != 0) {
            $searchSFSForm.validate({
                rules: {
                    FieldValue: "required"
                },
                messages: {
                    FieldValue: "Value of field is required!"
                }
            });
        }
    }

    var initMFSFormValidator = function () {
        var $searchMFSForm = $(".lms-mfs-form");
        if ($searchMFSForm.length != 0) {
            $searchMFSForm.validate({
                rules: {
                    Author: {
                        require_from_group: [2, ".mfsGroup"]
                    },
                    Title: {
                        require_from_group: [2, ".mfsGroup"]
                    },
                    Keywords: {
                        require_from_group: [2, ".mfsGroup"]
                    },
                    Content: {
                        require_from_group: [2, ".mfsGroup"]
                    },
                },
                messages: {
                    Author: {
                        require_from_group: "At least one field is required!"
                    },
                    Title: {
                        require_from_group: "At least one field is required!"
                    },
                    Keywords: {
                        require_from_group: "At least one field is required!"
                    },
                    Content: {
                        require_from_group: "At least one field is required!"
                    },
                }
            });
        }
    }

    var initValidators = function () {
        initSFSFormValidator();
        initMFSFormValidator();
    }

    var getSFSData = function ($form) {
        var data = new Object();
        data["FieldName"] = $form.find("select#sfsFieldName").val();
        data["FieldValue"] = $form.find("#sfsFieldValue").val();
        data["QueryType"] = $form.find("select#sfsQueryType").val();

        return data;
    }

    var getMFSData = function ($form) {
        var data = new Object();
        data["Title"] = $form.find("#mfsTitle").val();
        data["Author"] = $form.find("#mfsAuthor").val();
        data["Keywords"] = $form.find("#mfsKeywords").val();
        data["Content"] = $form.find("#mfsContent").val();
        data["Language"] = $form.find("#mfsContent").val();
        data["QueryOperator"] = $form.find("select#mfsQueryOperator").val();
        data["QueryType"] = $form.find("select#mfsQueryType").val();

        return data;
    }

    var initSFSSearchButtonHandler = function () {
        var $sfsSearchBtn = $(".lms-sfs-search-btn");
        var $form = $(".lms-sfs-form");

        $sfsSearchBtn.on("click", function (e) {
            e.preventDefault();
            if ($form.valid()) {
                var formData = getSFSData($form);
                var url = $form.attr("action");
                $.ajax({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(formData),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        console.log(data);
                        if (data.IsSuccess) {
                            console.log("aaa");
                        } else {
                            toastr.error("error");
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        toastr.error('Error authenitication user!  Status = ' + xhr.status);
                    }
                });
            }
        });
    }

    var initMFSSearchButtonHandler = function () {
        var $mfsSearchBtn = $(".lms-mfs-search-btn");
        var $form = $(".lms-mfs-form");

        $mfsSearchBtn.on("click", function (e) {
            e.preventDefault();
            console.log($form);
            if ($form.valid()) {
                var formData = getMFSData($form);
                var url = $form.attr("action");
                $.ajax({
                    url: url,
                    method: "POST",
                    data: JSON.stringify(formData),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        console.log(data);
                        if (data.Success) {
                            console.log("aaa");
                        } else {
                            toastr.error("error");
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