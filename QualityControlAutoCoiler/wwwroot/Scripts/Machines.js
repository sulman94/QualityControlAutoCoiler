
$(document).ready(function () {
    GetAllMachineDetails();

});

$('.check input:checkbox').click(function () {
    $('.check input:checkbox').not(this).prop('checked', false);
});

function GetAllMachineDetails() {
    
    $.ajax({
        'url': "/Machine/GetAllMachineDetails",
        'method': "GET",
        'contentType': 'application/json'
    }).done(function (result) {
        $('#tblMachine').DataTable({
            destroy: true,
            pageLength: 50,
            lengthMenu: [[50, 100, 150, 200, -1], [50, 100, 150, 200, "All"]], 
            "aaData": result.Data,
            "columns": [
                { "data": "Id", "name": "Id", "title": "Id", visible: false },
                { "data": "Name", "name": "Name", "title": "Name" },
                { "data": "NameInUrdu", "name": "NameInUrdu", "title": "Name In Urdu" },
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
                        return '<a class="warning p-0" data-original-title="" title="" onclick=EditMachineDetail(' + data + ')> <i class="ft-edit-2 font-medium-3 mr-2" ></i ></a>';
                    }
                }]
        })
    })
}

function AddMachine() {
    $("#AddMachineModal").modal('show');
}

function EditMachineDetail(Id) {
    ajaxDataParams.Id = Id;
    ajaxCallParams.Type = 'POST';
    ajaxCallParams.Url = '/Machine/ViewMachineDetails';
    try {
        ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
            
            
            if (result.success) {
                
                document.getElementById("evId").value = result.Data.Id;
                document.getElementById("evCreatedBy").value = result.Data.CreatedBy;
                document.getElementById("evCreatedDate").value = result.Data.CreatedDate;
                document.getElementById("evName").value = result.Data.Name;
                document.getElementById("evNameInUrdu").value = result.Data.NameInUrdu;
                document.getElementById("evIsActive").checked = result.Data.IsActive;
                $("#EditMachineModal").modal('show');
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
    ajaxCallParams.Url = '/Machine/UpdateMachineStatus';
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
                        GetAllMachineDetails()
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
function SaveMachineDetail() {
    
    if ($('#frmAddMachineDetail').valid()) {
        
        var ajaxDataParams = $('#frmAddMachineDetail').serializeArray().reduce(function (obj, item) {
            obj[item.name] = item.value;
            return obj;
        }, {});
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/Machine/SaveMachineDetails';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                
                
                if (result.success) {
                    $("#AddMachineModal").modal('hide');
                    swal({
                        title: 'Machine Added',
                        text: result.message,
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#0CC27E'
                    }).then(function (isConfirm) {
                        if (isConfirm) {
                            GetAllMachineDetails()
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
function UpdateMachineDetail() {
    
    if ($('#frmUpdateMachineDetail').valid()) {
        
        var ajaxDataParams = $('#frmUpdateMachineDetail').serializeArray().reduce(function (obj, item) {
            obj[item.name] = item.value;
            return obj;
        }, {});
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/Machine/UpdateMachineDetails';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                
                
                if (result.success) {
                    $("#EditMachineModal").modal('hide');
                    swal({
                        title: 'Machine Updated',
                        text: result.message,
                        type: 'success',
                        showCancelButton: false,
                        confirmButtonColor: '#0CC27E'
                    }).then(function (isConfirm) {
                        if (isConfirm) {
                            GetAllMachineDetails()
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