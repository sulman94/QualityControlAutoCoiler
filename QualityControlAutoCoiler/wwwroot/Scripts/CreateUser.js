$(document).ready(function () {
    $("#CNIC").on("keyup", function () {
        AddDashesToCnicField(this);
    });
});
$(document).on("change", ".uploadProfileInput", function () {
    
    var triggerInput = this;
    var currentImg = $(this).closest(".pic-holder").find(".pic").attr("src");
    var holder = $(this).closest(".pic-holder");
    var wrapper = $(this).closest(".profile-pic-wrapper");
    $(wrapper).find('[role="alert"]').remove();
    var files = !!this.files ? this.files : [];
    if (!files.length || !window.FileReader) {
        return;
    }
    if (/^image/.test(files[0].type)) {
        // only image file
        var reader = new FileReader(); // instance of the FileReader
        reader.readAsDataURL(files[0]); // read the local file

        reader.onloadend = function () {
            $(holder).addClass("uploadInProgress");
            $(holder).find(".pic").attr("src", this.result);
            $(holder).append(
                '<div class="upload-loader"><div class="spinner-border text-primary" role="status"><span class="sr-only">Loading...</span></div></div>'
            );

            // Dummy timeout; call API or AJAX below
            setTimeout(() => {
                $(holder).removeClass("uploadInProgress");
                $(holder).find(".upload-loader").remove();
                // If upload successful
                if (Math.random() < 0.9) {
                    $(wrapper).append(
                        '<div class="snackbar show" role="alert"><i class="fa fa-check-circle text-success"></i> Profile image updated successfully</div>'
                    );

                    // Clear input after upload
                    $(triggerInput).val("");

                    setTimeout(() => {
                        $(wrapper).find('[role="alert"]').remove();
                    }, 3000);
                } else {
                    $(holder).find(".pic").attr("src", currentImg);
                    $(wrapper).append(
                        '<div class="snackbar show" role="alert"><i class="fa fa-times-circle text-danger"></i> There is an error while uploading! Please try again later.</div>'
                    );

                    // Clear input after upload
                    $(triggerInput).val("");
                    setTimeout(() => {
                        $(wrapper).find('[role="alert"]').remove();
                    }, 3000);
                }
            }, 1500);
        };
    } else {
        $(wrapper).append(
            '<div class="alert alert-danger d-inline-block p-2 small" role="alert">Please choose the valid image.</div>'
        );
        setTimeout(() => {
            $(wrapper).find('role="alert"').remove();
        }, 3000);
    }
});

function CreateUser() {
    if ($('#FrmCreateUser').valid()) {
        var ajaxDataParams = $('#FrmCreateUser').serializeArray().reduce(function (obj, item) {
            obj[item.name] = item.value;
            return obj;
        }, {});
        ajaxDataParams.ProfileImage = document.getElementById("profilePic").src;        
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/Admin/SaveUser';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {              
                if (result.success) {
                    swal(result.message, "", "success");
                }
                else {
                    swal(result.message, "", "error");
                }
            });
        }
        catch (e) {
            console.log(e);
        }
    }
    else {

    }
}
function AddDashesToCnicField(input) {
    let Cnic = input.value.replace(/[^0-9]/g, ''); // Remove non-numeric characters

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