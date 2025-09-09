$(document).ready(function () {
    getAllUser();

});
function getAllUser() {

    $.ajax({
        'url': "/Admin/GetAllUsers",
        'method': "GET",
        'contentType': 'application/json'
    }).done(function (result) {
        $('#AllUsers').DataTable({
            destroy: true,
            "aaData": result.Data,
            "columns": [
                { "data": "UserId", "name": "UserId", "title": "User ID", visible: false },
                { "data": "FirstName", "name": "FirstName", "title": "First Name" },
                { "data": "LastName", "name": "LastName", "title": "Last Name" },
                {
                    data: 'Status',
                    title: 'Status',
                    mRender: function (data, type, row) {
                        if (data) {
                            return '<span class= "badge badge-success" >Active</span>'
                        }
                        else {
                            return '<span class= "badge badge-warning">InActive</span>'
                        }
                    }
                },
                { "data": "Email", "name": "Email", "title": "Email" },
                {
                    data: 'UserId',
                    title: "Actions",
                    bSortable: false,
                    mRender: function (data, type, row) {
                        return '<a class="success p-0" data-original-title="" title="" onclick=ViewUser(' + data + ')> <i class="ft-eye font-medium-3 mr-2" ></i ></a><a class="warning p-0" data-original-title="" title="" onclick=EditUser(' + data + ')> <i class="ft-edit-2 font-medium-3 mr-2" ></i ></a><a class="primary p-0" data-original-title="" title="" onclick=ResetPassword(' + data + ')> <i class="ft-user-x font-medium-3 mr-2" ></i ></a><a class="danger p-0" data-original-title="" title="" onclick=UpdateUserStatus(' + data + ')> <i class="ft-trash-2 font-medium-3 mr-2" ></i ></a>'
                    }
                }

            ]
        })
    })
}

function ViewUser(userid) {
    
    PostRedirect("ViewUser", userid);
}
function EditUser(userid) {
    PostRedirect("EditUser", userid);
}
function UpdateUserStatus(userid) {
    var CancelClick = false;
    if (CancelClick) {
        CancelClick = false;
        return;
    }
    swal({
        title: 'warning',
        text: 'Are you sure you want to Active/InActive User?',
        type: "warning",
        confirmButtonText: "<i class='la la-headphones'></i>Confirm",
        confirmButtonClass: "btn btn-danger",

        showCancelButton: true,
        cancelButtonText: "<i class='la la-thumbs-down'></i>Cancel",
        cancelButtonClass: "btn btn-default"
    }).then((result) => {
        if (result) {
            ajaxDataParams.userid = userid;
            ajaxCallParams.Type = 'POST';
            ajaxCallParams.Url = '/Admin/UpdateUserStatus';
            try {
                ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                    if (result.success) {
                        swal("success", result.message, "success");
                        getAllUser();
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
    })
}
function ResetPassword(userid) {
    var CancelClick = false;
    if (CancelClick) {
        CancelClick = false;
        return;
    }
    swal({
        title: 'warning',
        text: 'Are you sure you want to reset password?',
        type: "warning",
        confirmButtonText: "<i class='la la-headphones'></i>Confirm",
        confirmButtonClass: "btn btn-danger",

        showCancelButton: true,
        cancelButtonText: "<i class='la la-thumbs-down'></i>Cancel",
        cancelButtonClass: "btn btn-default"
    }).then((result) => {
        if (result) {
            ajaxDataParams.userid = userid;
            ajaxCallParams.Type = 'POST';
            ajaxCallParams.Url = '/Admin/ResetPassword';
            try {
                ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                    if (result.success) {
                        swal("success", result.message, "success");
                        getAllUser();
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
    })
}