$(document).ready(function () {
    $("#Usuario").focus();
    //$('[data-toggle="popover"]').popover()
});
$("#btn_Aceptar").click(function () {
    validar();
});
$("#Clave").keypress(function (event) {
    if (event.which == 13) {
        validar();
    }
});
function validar() {
    $('#Usuario').popover('dispose')
    $('#Clave').popover('dispose')
    
    if ($("#Usuario").val() == "") {
        $('#Usuario').popover('show')
        $("#Usuario").focus();
        return false;
    }
    else if ($("#Clave").val() == "") {
        $('#Clave').popover('show')
        $("#Clave").focus();
        return false;
    }
    else {        
        var md5 = $.md5($('#Clave').val());
        $('#Clave').val(md5);
        $("#FrmLogin").submit()
        return false;
    }
}

