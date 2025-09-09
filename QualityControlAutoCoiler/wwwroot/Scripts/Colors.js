
$(document).ready(function () {
    GetAllColorDetails();

});

$('.check input:checkbox').click(function () {
    $('.check input:checkbox').not(this).prop('checked', false);
});

function GetAllColorDetails() {
    
    $.ajax({
        'url': "/Color/GetAllColorDetails",
        'method': "GET",
        'contentType': 'application/json'
    }).done(function (result) {
        $('#tblColor').DataTable({
            destroy: true,
            pageLength: 50,
            lengthMenu: [[50, 100, 150, 200, -1], [50, 100, 150, 200, "All"]], 
            "aaData": result.Data,
            "columns": [
                { "data": "Id", "name": "Id", "title": "Id", visible: false },
                { "data": "ColorName", "name": "ColorName", "title": "Color Name" },
                { "data": "ColorNameInUrdu", "name": "ColorNameInUrdu", "title": "Color Name In Urdu" },
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
                        return '<a class="warning p-0" data-original-title="" title="" onclick=EditColorDetail(' + data + ')> <i class="ft-edit-2 font-medium-3 mr-2" ></i ></a>';
                    }
                }]
        })
    })
}

function AddColor() {
    $("#AddColorModal").modal('show');
}

function EditColorDetail(Id) {
    ajaxDataParams.Id = Id;
    ajaxCallParams.Type = 'POST';
    ajaxCallParams.Url = '/Color/ViewColorDetails';
    try {
        ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
            
            
            if (result.success) {
                
                document.getElementById("evId").value = result.Data.Id;
                document.getElementById("evCreatedBy").value = result.Data.CreatedBy;
                document.getElementById("evCreatedDate").value = result.Data.CreatedDate;
                document.getElementById("evColorName").value = result.Data.Name;
                document.getElementById("evColorNameInUrdu").value = result.Data.NameInUrdu;
                document.getElementById("evIsActive").checked = result.Data.IsActive;
                $("#EditColorModal").modal('show');
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
    ajaxCallParams.Url = '/Color/UpdateColorStatus';
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
                        GetAllColorDetails()
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
function SaveColorDetail() {
    
    if ($('#frmAddColorDetail').valid()) {
        
        var ajaxDataParams = $('#frmAddColorDetail').serializeArray().reduce(function (obj, item) {
            obj[item.name] = item.value;
            return obj;
        }, {});
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/Color/SaveColorDetails';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                
                
                if (result.success) {
                    $("#AddColorModal").modal('hide');
                    swal({
                        title: 'Color Added',
                        text: result.message,
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#0CC27E'
                    }).then(function (isConfirm) {
                        if (isConfirm) {
                            GetAllColorDetails()
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
function UpdateColorDetail() {
    
    if ($('#frmUpdateColorDetail').valid()) {
        
        var ajaxDataParams = $('#frmUpdateColorDetail').serializeArray().reduce(function (obj, item) {
            obj[item.name] = item.value;
            return obj;
        }, {});
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/Color/UpdateColorDetails';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                
                
                if (result.success) {
                    $("#EditColorModal").modal('hide');
                    swal({
                        title: 'Color Updated',
                        text: result.message,
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#0CC27E'
                    }).then(function (isConfirm) {
                        if (isConfirm) {
                            GetAllColorDetails()
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