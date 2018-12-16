var SearchHandler = function () {
    var $resultsDiv = $("div#searchResultsDiv");
    var $resultsMessage = $("div#searchNoResults");

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

        $resultsDiv.hide();
        $resultsMessage.hide();
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

    var canDownload = function (ebookId, user) {
        var success = false
        var currentUser = JSON.parse(user);
        var roles = currentUser.Roles;
        var userId = currentUser.UserId;

        if (roles.includes("RoleAdmin") || roles.includes("RoleLibrarian")) {
            success = true;
        } else {
            var postData = new Object();
            postData["EBookId"] = parseInt(ebookId);
            postData["UserId"] = userId;
            $.ajax({
                url: "/EBook/EBook/AllowDownload",
                type: "POST",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    if (data) {
                        success = true;
                    } else {
                        success = false;
                    }
                }
            });
        }
        return success;
    }

    var initResultsHandlers = function () {

        $(".lms-ebook-download-btn").on("click", function (e) {
            e.stopPropagation();
            var id = $(this).attr("id");
            var ebookId = id.split("|")[0];
            var ebookFilename = id.split("|")[1];
            var currentUser = sessionStorage.getItem("current-user");

            if (!currentUser) {
                alert("You must be signed in to be able to download e-books!");
            } else if (canDownload(ebookId, currentUser)) {
                var aElement = document.createElement("a");
                var fileUrl = "/EBook/EBook/Download/" + ebookId;

                aElement.href = fileUrl;
                aElement.download = ebookFilename;
                aElement.click();
            } else {
                alert("You aren't able to download this e-book. It is in category you're not subscribed to.");
            }
        });

        $(".lms-ebook-delete-btn").on("click", function (e) {
            e.stopPropagation();
            var $nearestRow = $(this).closest("tr");
            if (!$nearestRow.hasClass("selected")) {
                $nearestRow.addClass("selected");
            }
            var ebookId = $(this).attr("id");
            if (confirm("Do you want to delete selected e-book? [Id: " + ebookId + "]")) {
                $.ajax({
                    url: "/EBook/EBook/Delete/" + ebookId,
                    type: "GET",
                    success: function (data) {
                        if (data.Success) {
                            toastr.success(data.Message);
                            window.setTimeout(function () {
                                location.reload();
                            }, 2800);
                            location.reload();
                        } else {
                            toastr.error(data.Message);
                        }
                    },
                    error: function (XMLHtppRequest, textStatus, errorThrown) {
                        toastr.error("Error making AJAX call: " +
                            XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
                    }
                });
            } else {
                $nearestRow.removeClass("selected");
            }
        });
    }

    var getSFSData = function ($form) {
        var data = new Object();
        data["FieldName"] = $form.find("select#sfsFieldName").val();
        data["FieldValue"] = $form.find("#sfsFieldValue").val();
        data["Language"] = $form.find("select#sfsLanguage").val();
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

    var showNoResultsMessage = function () {
        $resultsDiv.hide();
        $resultsMessage.show();
    }
    
    var generateResultRow = function (result) {       
        var id = result.Id;
        var title = result.Title;
        var author = result.Author;
        var keywords = result.Keywords;
        var language = result.Language;
        var category = result.Category;
        var filename = result.Filename;
        var highlights = result.Highlights;
        var year = result.Year;
        
        var downloadId = id + "|" + filename;

        var $row = $("<tr></tr>").attr("id", result.Id);        
        var titleField = $("<td></td>").text(title);
        var authorField = $("<td></td>").text(author);
        var highlightsField = $("<td></td>").html(highlights);
        var yearField = $("<td></td>").text(year);
        var keywordsField = $("<td></td>").text(keywords);
        var languageField = $("<td></td>").text(language);
        var categoryField = $("<td></td>").text(category);
        var filenameField = $("<td></td>").text(filename);

        var deleteField = $("<td></td>").attr("id", id).attr("class", "lms-ebook-delete-btn");
        var deleteImg = $("<img/>").attr("src", "/Resources/Images/delete_trash.png")
        var downloadField = $("<td></td>").attr("id", downloadId).attr("class", "lms-ebook-download-btn");
        var downloadImg = $("<img/>").attr("src", "/Resources/Images/download.png")

        deleteField.append(deleteImg);
        downloadField.append(downloadImg);

        $row.append(titleField);
        $row.append(authorField);
        $row.append(highlightsField);
        $row.append(yearField);
        $row.append(languageField);
        $row.append(categoryField);
        $row.append(keywordsField);
        $row.append(deleteField);
        $row.append(downloadField);

        return $row;
    }

    var showResultsInTable = function (data) {
        var results = data.Result;
        var $table = $resultsDiv.find("table#searchResultTable");
        var $thead = $table.find("thead");
        var $tbody = $("<tbody>");        
        
        for (var i = 0; i < results.length; i++) {
            var singleResult = results[i];
            var $row = generateResultRow(singleResult);
            $tbody.append($row);
        }

        $thead.after($tbody);
        $resultsDiv.show();
        $resultsMessage.hide();
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
                            showResultsInTable(data);
                            initResultsHandlers();
                        } else {
                            showNoResultsMessage();
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
                            showResultsInTable(data);
                            initResultsHandlers();
                        } else {
                            showNoResultsMessage();
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