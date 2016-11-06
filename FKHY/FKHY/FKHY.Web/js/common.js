function alertSuccess(text, callback) {
    $().toastmessage('showToast', {
        inEffectDuration: 500,   // in effect duration in miliseconds
        stayTime: 1800,   // time in miliseconds before the item has to disappear
        text: text,
        sticky: false,
        position: 'middle-center',
        type: 'success',
        closeText: '',
        close: callback
    });
}
function alertWarning(text, callback) {
    //var options = { text: message, type: 'warning' };
    //return $().toastmessage('showToast', options);

    $().toastmessage('showToast', {
        inEffectDuration: 500,   // in effect duration in miliseconds
        stayTime: 1800,   // time in miliseconds before the item has to disappear
        text: text,
        sticky: false,
        position: 'middle-center',
        type: 'warning',
        closeText: '',
        close: callback
    });
}
function alertFailed(text, callback) {
    $().toastmessage('showToast', {
        inEffectDuration: 500,   // in effect duration in miliseconds
        stayTime: 1800,   // time in miliseconds before the item has to disappear
        text: text,
        sticky: false,
        position: 'middle-center',
        type: 'error',
        closeText: '',
        close: callback
    });
}

$(function () {
    jQuery.ajaxPost = function (url, data, successfn)
    {
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            type: 'POST',
            data: JSON.stringify(data),
            success: function (d) {
                //called when successful
                successfn(d);
            },
            error: function (e) {
                //called when there is an error
                //console.log(e.message);
            },
            beforeSend: function (jqXHR, settings) {
                $('#loading').show();
                $('#loading_gif').show();
            },
            complete: function (jqXHR, status) {
                $('#loading').hide();
                $('#loading_gif').hide();
            },
            error: function (jqXHR, status, error) {
                alertFailed(error);
            }
        });
    };
});