function ConfirmSavePermissionTemplate() {
    swal({
        title: 'warning',
        text: 'Are you sure you want to create Role Template?',
        type: "warning",
        buttonsStyling: true,
        confirmButtonText: "<i class='la la-check-circle'></i>Confirm",
        confirmButtonClass: "btn btn-danger",
        showCancelButton: true,
        cancelButtonText: "<i class='la la-times-circle-o'></i>Cancel",
        cancelButtonClass: "btn btn-default"
    }).then((result) => {
        if (result) {
            SavePermissionTemplate();
        }
    })
}
function SavePermissionTemplate() {
    var TemplateName = $('#TemplateName').val();
    var IsActive = document.getElementById('IsActive').checked;
    var PermissionTemplateDTO = {};
    var checkedList = [];
    $('#tbodyid tr').each(function () {
        $(this).find('td').each(function () {

            if ($(this).find('input[type="checkbox"]:checked').val() != undefined) {
                var functionalityList = {}
                functionalityList.FunctionalityId = $(this).find('input[type="checkbox"]:checked').val();
                functionalityList.IsAllow = true;
                checkedList.push(functionalityList);
            }

        });
    });
    if (TemplateName == "") {
        swal("error", "Please Enter Role Name", "error");
    }
    else if (checkedList.length == 0) {
        swal("error", "Please Select Atleast 1 Permission Checkbox", "error");
    }
    else {
        PermissionTemplateDTO.TemplateName = TemplateName;
        PermissionTemplateDTO.IsActive = IsActive;
        PermissionTemplateDTO.permissionTemplates = checkedList;
        var ajaxDataParams = PermissionTemplateDTO;
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/Permission/SavePermissionTemplate';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                if (result.success) {
                    swal({
                        title: 'Role Created Successfully.',
                        text: result.message,
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#0CC27E'
                    }).then(function (isConfirm) {
                        if (isConfirm) {
                        }
                    }).catch(swal.noop);
                }
                else {
                    swal("error", result.message, "error");
                }
            });
        }
        catch (e) {
            console.log(e);
        }
    }
}
function ShowPermissionTemplate() {
    if ($('#Templates').val() == "") {
        swal("error", "Please select Role Name", "error");
        return;
    }
    else {
        var templateId = $('#Templates').val();
        window.location.href = '/Permission/ManageRoles?TempId=' + templateId;
    }
}
function ConfirmUpdatePermissionTemplate() {
    swal({
        title: 'warning',
        text: 'Are you sure you want to update Role Template?',
        type: "warning",
        buttonsStyling: true,
        confirmButtonText: "<i class='la la-check-circle'></i>Confirm",
        confirmButtonClass: "btn btn-danger",
        showCancelButton: true,
        cancelButtonText: "<i class='la la-times-circle-o'></i>Cancel",
        cancelButtonClass: "btn btn-default"
    }).then((result) => {
        if (result) {
            UpdatePermissionTemplate();
        }
    })
}

function UpdatePermissionTemplate() {
    var TemplateName = $('#TemplateName').val();
    var TemplateId = $('#Id').val();
    var IsActive = document.getElementById('IsActive').checked == true ? true : false;
    var PermissionTemplateDTO = {};
    var checkedList = [];
    $('#tbodyid tr').each(function () {
        $(this).find('td').each(function () {

            if ($(this).find('input[type="checkbox"]:checked').val() != undefined) {
                var functionalityList = {}
                functionalityList.FunctionalityId = $(this).find('input[type="checkbox"]:checked').val();
                functionalityList.IsAllow = true;
                checkedList.push(functionalityList);
            }

        });
    });

    if (TemplateName == "") {
        swal("error", "Please enter Role Name", "error");
    }
    else if (checkedList.length == 0) {
        swal("error", "Please Select Atleast 1 Permission Checkbox", "error");
    }
    else {
        PermissionTemplateDTO.Id = TemplateId;
        PermissionTemplateDTO.TemplateName = TemplateName;
        PermissionTemplateDTO.IsActive = IsActive;
        PermissionTemplateDTO.permissionTemplates = checkedList;
        var ajaxDataParams = PermissionTemplateDTO;
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/Permission/UpdatePermissionTemplate';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                if (result.success) {
                    swal({
                        title: 'Role Updated Successfully.',
                        text: result.message,
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#0CC27E'
                    }).then(function (isConfirm) {
                        if (isConfirm) {
                            var url = "/Permission/ManageRoles";
                            window.location.replace(url);
                        }
                    }).catch(swal.noop);
                }
                else {
                    swal("error", result.message, "error");
                }
            });
        }
        catch (e) {
            console.log(e);
        }
    }
}