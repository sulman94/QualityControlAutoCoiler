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

function CalculateLengthRecovered() {
    var GoodCoils = parseFloat($('#GoodCoils').val()) || 0;
    var ShortLengthMeters = parseFloat($('#ShortLengthMeters').val()) || 0;
    var DrumWiseScrap = parseFloat($('#DrumWiseScrap').val()) || 0;

    var LengthRecovered = (GoodCoils * 90) + ShortLengthMeters + DrumWiseScrap;

    $('#LengthRecovered').val(LengthRecovered).trigger('change');
}

function CalculateDifference() {
    var LengthMentioned = parseFloat($('#LengthMentioned').val()) || 0;
    var LengthRecovered = parseFloat($('#LengthRecovered').val()) || 0;
    var Difference = LengthMentioned - LengthRecovered;

    $('#Difference').val(Difference);
    $('#DifferencePercentage').val(((Difference / LengthMentioned) * 100).toFixed(2));
}