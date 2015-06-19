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

    $.validator.setDefaults({
        ignore: []
    });

    $(".jqte-validation").validate({
        errorClass: 'maskError',
        errorPlacement: function (error, element) {
            var el = $(element).closest(".jqte");
            if (el.length == 1) {
                error.insertAfter(el);
            } else {
                error.insertAfter(element);
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass(errorClass).removeClass(validClass);

            var el = $(element).closest(".jqte");
            if (el.length == 1) {
                el.addClass(errorClass);
            }
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass(errorClass).addClass(validClass);
            var el = $(element).closest(".jqte");
            if (el.length == 1) {
                el.removeClass(errorClass);
            }
        },
        onkeyup: false
    });


});