function showMessage(container, type, message) {
    var alert = $(container).find('div[name="message"]:first');
    if (type === 'error') {
        $(alert).removeClass('alert-success').addClass('alert-danger');
    }
    else {
        $(alert).removeClass('alert-danger').addClass('alert-success');
    }

    var span = $(alert).find('span:first');
    $(span).html(message);
    $(alert).parent().show();
}

function getAjaxJQ(type, url, data, successCallback, errorCallback) {
    $.ajax({
        type: type,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.columnDefinitions) {
                let coldef = JSON.parse(response.columnDefinitions);
                response.columnDefinitions = coldef;
            }
            else if (response.value && response.value.columnDefinitions) {
                let coldef = JSON.parse(response.value.columnDefinitions);
                response.columnDefinitions = coldef;
            }
            
            successCallback(response.value ? response.value : response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            var r = response.value ? response.value : response;
            if(errorCallback != null) {
                errorCallback(r);
            }
            else {
                alert(response.responseText);
            }
        }
    });
}

function getAjax(method, url, data, successCallback, errorCallback) {
    const options = {
        method: method,
        body: data,
        headers: {
            'Content-Type': 'application/json'
        }
    };

    fetch(url, options)
        .then(response => response.json())
        .then(response => {
            console.log(response);
            if (response.code == 200) {
                if (response.columnDefinitions) {
                    let coldef = JSON.parse(response.columnDefinitions);
                    response.columnDefinitions = coldef;
                }
                else if (response.value && response.value.columnDefinitions) {
                    let coldef = JSON.parse(response.value.columnDefinitions);
                    response.columnDefinitions = coldef;
                }
                if (successCallback !== null) {
                    successCallback(response);
                }
            }
            else if (errorCallback !== null) {
                if (response.message) {
                    errorCallback(response.message);
                }
                else if (response.title) {
                    errorCallback(response.title);
                }
                else {
                    errorCallback('An error occured');
                }
            }
        })
        .catch(err => {
            console.log(err);
            if (errorCallback != null) {
                errorCallback(err);
            }
        });
}




