$(document).ready(function () {
    $('#fullaccess').change(function () {

        if ($('#fullaccess').is(":checked")) {
            $(".genericchk").attr('disabled', true);
        }
        else {
            $(".genericchk").attr('disabled', false);

        }
    });
});
function ConfirmChangePermission() {
    var CancelClick = false;
    if (CancelClick) {
        CancelClick = false;
        return;
    }
    swal({
        title: 'warning',
        text: 'Are you sure you want to change permission ?',
        type: "warning",
        confirmButtonText: "<i class='la la-headphones'></i>Confirm",
        confirmButtonClass: "btn btn-danger",

        showCancelButton: true,
        cancelButtonText: "<i class='la la-thumbs-down'></i>Cancel",
        cancelButtonClass: "btn btn-default"
    }).then((result) => {
        if (result) {
            
            ChangePermission();

        }
    })
}
function ChangePermission() {
    
    var formid = $('#forms').val();
    var userid = $('#users').val();
    var IsFullAccess;
    if ($('#fullaccess').is(":checked")) {
        IsFullAccess = true;

    }
    else {
        IsFullAccess = false;

    }
    var ChangePermissionDTO = {};
    var FunctionId = [];
    $(".genericchk:checked").each(function () {
        FunctionId.push(parseInt($(this).attr('id')));
    });
    ChangePermissionDTO.FormId = formid;
    ChangePermissionDTO.UserId = userid;
    ChangePermissionDTO.FunctionId = FunctionId;
    ChangePermissionDTO.FullAccess = IsFullAccess;
    $.ajax({
        type: 'POST',
        url: '/Permission/ChangePermission',
        data: ChangePermissionDTO,
        success: function (response) {

            if (response == "true") {
                swal({
                    title: 'Success',
                    text: 'Successfully changed',
                    type: "success",
                    confirmButtonText: "<i class='la la-headphones'></i>ok",
                    confirmButtonClass: "btn btn-default",

                    showCancelButton: false
                }).then((result) => {
                    if (result) {


                    }
                })
            }
            else {
                alert('error');
            }
        },

    });
}
function ShowFunctionalities() {

    var modelform = {};
    if ($('#users').val() == "") {
        swal("error", "Please select user", "error")
        return;

    }
    if ($('#forms').val() == "") {
        swal("error", "Please select Page", "error")
        return;

    }

    var formid = $('#forms').val();
    var userid = $('#users').val();
    modelform.FormId = formid;
    modelform.UserId = userid;
    $.ajax({
        type: 'POST',
        //url: "Permission/GetFunctionalities",      
        url: '/Permission/GetFunctionalities',
        data: modelform,
        success: function (response) {
            var IsFullAccess;
            $('#chkdiv').html('');


            for (let i = 0; i < response.length; ++i) {

                for (let j = 0; j < response[i].list.length; ++j) {
                    $('#chkdiv').append(`<div class="custom-checkbox custom-control-inline ml-3"> <input class="custom-control-input genericchk" id="` + response[i].list[j].FunctionalityId + `"  type="checkbox" value="true"> <label class="custom-control-label" for="` + response[i].list[j].FunctionalityId + `">
`+ response[i].list[j].FunctionalityName + `
</label></div>`);
                }
            }

            for (let i = 0; i < response.length; ++i) {
                IsFullAccess = response[i].IsFullAccess;
                for (let j = 0; j < response[i].list.length; ++j) {

                    $('#' + response[i].list[j].FunctionalityId).prop('checked', response[i].list[j].IsSelected)
                }
            }
            $('#fullaccess').prop('checked', IsFullAccess)
            if (IsFullAccess) {
                $(".genericchk").attr('disabled', true);
            }
            else {
                $(".genericchk").attr('disabled', false);
            }
            $('#functionalitiesDiv').css('visibility', 'visible');

        },

    });



}