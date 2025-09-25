$(document).ready(function () {
    $('.pickadate').pickadate({
        format: 'dd/mm/yyyy', // Customize the date format
        formatSubmit: 'yyyy-mm-dd', // Format used for form submission
        selectMonths: true, // Enables month selection
        selectYears: true // Enables year selection
    });
    var pkdate = $('#CreatedDate').pickadate('picker');
    pkdate.set('select', new Date());
});

function SaveEntry() {
    if ($('#frmCreateAutoCoilerEntry').valid()) {
        var ajaxDataParams = $('#frmCreateAutoCoilerEntry').serializeArray().reduce(function (obj, item) {
            obj[item.name] = item.value;
            return obj;
        }, {});
        
        ajaxCallParams.Type = 'POST';
        ajaxCallParams.Url = '/AutoCoiler/SaveProductionLogs';
        try {
            ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
                if (result.success) {
                    swal({
                        title: 'Success',
                        text: result.Message,
                        type: "success",
                        buttonsStyling: true,
                        confirmButtonText: "<i class='la la-headphones'></i>ok",
                        confirmButtonClass: "btn btn-default",
                        showCancelButton: false
                    }).then((result) => {

                        location.reload();
                    });
                }
                else {
                    swal("error", result.Message, "error");
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