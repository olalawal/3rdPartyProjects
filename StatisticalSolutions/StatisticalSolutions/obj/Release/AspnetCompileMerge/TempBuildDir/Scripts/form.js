var form = {};
$(function () {
    $('.calendar').datepicker({
        changeMonth: true,
        changeYear: true,
        autoclose: true,
        dateFormat: 'mm/dd/yy',
        yearRange: "-100:+100",
        todayHighlight: true
    });

    $('.calendar').datepicker().on('changeDate', function () {
        $('.datepicker').hide();
    });
    $('#ui-datepicker-div').css("display", "none");

    $(".html-editor").jqte();
});