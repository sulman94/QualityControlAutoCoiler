var ajaxCallParams = {};
var ajaxDataParams = {};

// Generic function for all ajax calls
function ajaxCall(callParams, dataParams, callback) {
    if (!callParams || !callParams.Type || !callParams.Url) {
        console.error("Invalid callParams:", callParams);
        return;
    }

    $("#divLoader").show();

    $.ajax({
        type: callParams.Type,
        url: callParams.Url,
        beforeSend: function (xhr) {
            var token = $('input:hidden[name="__RequestVerificationToken"]').val();
            if (token) {
                xhr.setRequestHeader("XSRF-TOKEN", token);
            }
        },
        dataType: 'json',
        data: dataParams,
        cache: true,
        success: function (response) {
            $("#divLoader").hide();
            if (typeof callback === "function") {
                callback(response);
            }
        },
        error: function (response) {
            $("#divLoader").hide();
            if (typeof callback === "function") {
                callback(response);
            }
        }
    });
}

function AddDashes(fieldName) {
    let input = document.getElementById(fieldName);
    let Cnic = input.value.replace(/[^0-9]/g, ''); // Remove non-numeric characters

    // Limit input to 13 numeric characters (excluding dashes)
    if (Cnic.length > 13) {
        Cnic = Cnic.substring(0, 13);
    }

    let formattedCnic = '';
    if (Cnic.length > 5) {
        formattedCnic += Cnic.substring(0, 5) + '-';
    } else {
        formattedCnic += Cnic;
    }
    if (Cnic.length > 12) {
        formattedCnic += Cnic.substring(5, 12) + '-';
        formattedCnic += Cnic.substring(12, 13);
    } else if (Cnic.length > 5) {
        formattedCnic += Cnic.substring(5);
    }

    input.value = formattedCnic;
}

function ajaxCallFileUpload(callParams, dataParams, callback) {

    $.ajax({
        type: callParams.Type,
        url: callParams.Url,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
       dataType: 'json',
        //contentType: 'multipart/form-data',
        data: dataParams,
        cache: true,
        success: function (response) {

            callback(response);
        },
        error: function (response) {
            callback(response);
        }
    });
}
//Generic Function for Post-Redirect
function PostRedirect(formID,Id)
{
        
    var form = document.getElementById(formID);
    $('#'+formID).append("<input type='hidden' name='Id' value='" +
        Id + "' />");
    form.submit();
}


$(document).ajaxStart(function (event, jqxhr, settings) {

    $.blockUI({
        message: "<img style='height: 40px' src='/css/loader.gif' />",
        css: { width: "150px", left: "45%" }
    });


}).ajaxStop(function () {

    $.unblockUI();
});

$(document).ajaxError(function () {
    $.unblockUI();
});

$(function () {
    //setup ajax error handling
    $.ajaxSetup({
        error: function (x, status, error) {
             ;
            if (x.status === 401) {
                UnauthorizeAlert();
            }

        }
    });
});

function UnauthorizeAlert() {

    swal({
        title: 'Unauthorized',
        text: 'You do not have permission to perform this operation',
        type: "error",

        buttonsStyling: false,

        confirmButtonText: "<i class='la la-headphones'></i>ok",
        confirmButtonClass: "btn btn-default",

        showCancelButton: false
    });
}


function LogOut() {
    $.ajax({
        type: "POST",
        url: "Login/LogOut",
        data: {},
        success: function (response) {

            if (response === "LogOut") {
                var url = "/Login/Index";
                window.location.replace(url);
            }

        },

    });
}
