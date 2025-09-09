
$(document).ready(function () {
    GetAllSizeCategoryDetails();

});

$('.check input:checkbox').click(function () {
    $('.check input:checkbox').not(this).prop('checked', false);
});

function GetAllSizeCategoryDetails() {
    
    $.ajax({
        'url': "/SizeCategory/GetAllSizeCategoryDetails",
        'method': "GET",
        'contentType': 'application/json'
    }).done(function (result) {
        $('#tblSizeCategory').DataTable({
            destroy: true,
            pageLength: 50,
            lengthMenu: [[50, 100, 150, 200, -1], [50, 100, 150, 200, "All"]], 
            "aaData": result.Data,
            "columns": [
                { "data": "Id", "name": "Id", "title": "Id", visible: false },
                { "data": "Size", "name": "Size", "title": "Size" },
                {
                    data: 'IsActive',
                    title: 'IsActive',
                    mRender: function (data, type, row) {
                        if (data) {
                            return '<span class= "badge badge-success" onclick="ChangeStatus(' + row.Id + ',' + row.IsActive + ')" >Active</span>'
                        }
                        else {
                            return '<span class= "badge badge-warning" onclick="ChangeStatus(' + row.Id + ',' + row.IsActive + ')">InActive</span>'
                        }
                    }
                },
                { "data": "CreatedBy", "name": "CreatedBy", "title": "CreatedBy" },
                {
                    title: "Create Date",// name
                    render: function (data, type, row) {//data
                        return moment(row.CreatedDate).format('DD/MM/YYYY hh:mm:ss');
                    }
                }, {
                    data: 'Id',
                    title: "Actions",
                    bSortable: false,
                    mRender: function (data, type, row) {
                        return '<a class="warning p-0" data-original-title="" title="" onclick=EditSizeCategoryDetail(' + data + ')> <i class="ft-edit-2 font-medium-3 mr-2" ></i ></a>';
                    }
                }]
        })
    })
}

function AddSizeCategory() {
    $("#AddSizeCategoryModal").modal('show');
}

function EditSizeCategoryDetail(Id) {
    ajaxDataParams.Id = Id;
    ajaxCallParams.Type = 'POST';
    ajaxCallParams.Url = '/SizeCategory/ViewSizeCategoryDetails';
    try {
        ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
            
            
            if (result.success) {
                
                document.getElementById("evId").value = result.Data.Id;
                document.getElementById("evCreatedBy").value = result.Data.CreatedBy;
                document.getElementById("evCreatedDate").value = result.Data.CreatedDate;
                document.getElementById("evSize").value = result.Data.Size;
                document.getElementById("evIsActive").checked = result.Data.IsActive;
                $("#EditSizeCategoryModal").modal('show');
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

function ChangeStatus(Id, Status) {
    
    ajaxDataParams.Id = Id;
    ajaxDataParams.IsActive = Status == true ? false : true;
    ajaxCallParams.Type = 'POST';
    ajaxCallParams.Url = '/SizeCategory/UpdateSizeCategoryStatus';
    try {
        ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
            
            
            if (result.success) {
                swal({
                    title: 'Status Updated',
                    text: result.message,
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonColor: '#0CC27E'
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        GetAllSizeCategoryDetails()
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
function SaveSizeCategoryDetail() {
    
    if ($('#frmAddSizeCategoryDetail').valid()) {
        
        var ajaxDataParams = $('#frmAddSizeCategoryDetail').serializeArray().reduce(function (obj, item) {
            obj[item.name] = item.value;
            return obj;
        }, {});
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/SizeCategory/SaveSizeCategoryDetails';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                
                
                if (result.success) {
                    $("#AddSizeCategoryModal").modal('hide');
                    swal({
                        title: 'SizeCategory Added',
                        text: result.message,
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#0CC27E'
                    }).then(function (isConfirm) {
                        if (isConfirm) {
                            GetAllSizeCategoryDetails()
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
    else {

    }
}
function UpdateSizeCategoryDetail() {
    
    if ($('#frmUpdateSizeCategoryDetail').valid()) {
        
        var ajaxDataParams = $('#frmUpdateSizeCategoryDetail').serializeArray().reduce(function (obj, item) {
            obj[item.name] = item.value;
            return obj;
        }, {});
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/SizeCategory/UpdateSizeCategoryDetails';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                
                
                if (result.success) {
                    $("#EditSizeCategoryModal").modal('hide');
                    swal({
                        title: 'SizeCategory Updated',
                        text: result.message,
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#0CC27E'
                    }).then(function (isConfirm) {
                        if (isConfirm) {
                            GetAllSizeCategoryDetails()
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
    else {

    }
}