Statistical_Alert = (function () {

    // class function a.k.a. constructor
    function cls() {
        // private instance fields
        var self = this;

    }

    // public static method
    cls.success = function (msg) {
        $('.bottom-right').notify({
            message: { html: "<b>" + msg.title + "</b> " + msg.text },
            type: 'success'
        }).show(); // for the ones that aren't closable and don't fade out there is a .close() function.
    };

    cls.info = function (msg) {
        $('.bottom-right').notify({
            message: { html: "<b>" + msg.title + "</b> " + msg.text },
            type: 'info'
        }).show(); // for the ones that aren't closable and don't fade out there is a .close() function.
    };

    cls.warning = function (msg) {
        $('.bottom-right').notify({
            message: { html: "<b>" + msg.title + "</b> " + msg.text },
            type: 'warning'
        }).show(); // for the ones that aren't closable and don't fade out there is a .close() function.
    };

    cls.error = function (msg) {
        $('.bottom-right').notify({
            message: { html: "<b>" + msg.title + "</b> " + msg.text },
            type: 'danger'
        }).show(); // for the ones that aren't closable and don't fade out there is a .close() function.
    };

    return cls;
})();