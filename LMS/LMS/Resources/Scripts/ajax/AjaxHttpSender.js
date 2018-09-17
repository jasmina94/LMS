var AjaxHttpSender = function () {

    var self = this;

    this.sendGet = function (url, callback) {
        $.ajax({
            url: url,
            type: 'GET',
            success: function (data, textStatus) {
                callback.success(data, textStatus);
            },
            error: function (XMLHtppRequest, textStatus, errorThrown) {
                callback.failure(XMLHtppRequest, textStatus, errorThrown)
            }
        });
    }

    this.sendPost = function (url, data, callback) {
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (data) {
                callback.success(data);
            },
            error: function (data, textStatus, errorThrown) {
                callback.failure(data, textStatus, errorThrown);
            }

        });
    }

    this.saveDataAjaxCall = function ($form) {
        var url = $form.attr("action");
        var method = $form.attr("method");
        var data = $form.serialize();
        var id = ("#" + $form.attr("id"));
        var callback = {
            success: function (data) {
                if (data.Success) {
                    toastr.success(data.Message, "Success");
                    $(id)[0].reset();
                    $form.closest("div#Dialog").dialog("close");
                }
                else {
                    toastr.error(data.Message, "Error");
                }
            },
            failure: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error("Error making AJAX call: " + XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
            }
        }

        if (method == "post") {
            self.sendPost(url, data, callback);
        }
        else {
            self.sendGet(url, callback);
        }
    }

    this.loginAjaxCall = function ($form) {
        var url = $form.attr("action");
        var method = $form.attr("method");
        var data = $form.serialize();
        var callback = {
            success: function (data) {
                if (data.Success) {
                    var location = data.RedirectionUrl;
                    window.location = location;
                } else {
                    toastr.error(data.Message, "Error");
                }
            },
            failure: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error("Error making AJAX call: " + XMLHttpRequest.statusText + " (" + XMLHttpRequest.status + ")");
            }
        }

        if (method == "post") {
            self.sendPost(url, data, callback);
        } else {
            self.sendGet(url, callback);
        }
    }

    this.saveData = function ($form) {
        if ($form.valid()) {
            self.saveDataAjaxCall($form);
        }
    }

    this.login = function ($form) {
        self.loginAjaxCall($form);
    }
};

