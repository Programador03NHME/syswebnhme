var EstadoSelect = 0;
$(document).ready(function () {    
    //$('#ModalCreate').modal('show')
    var table = $('#tblAreas').DataTable({
        "language": {
            "emptyTable": "No hay datos disponibles en la tabla",
            "lengthMenu": '_MENU_ ',
            "search": '<i class="fa fa-search" aria-hidden="true"></i> Buscar ',
            "loadingRecords": "cargando...",            
            "infoEmpty": "Mostrando 0 a 0 de 0 resultados",
            "info": "Mostrando _START_ de _END_ en _TOTAL_ resultados",
            "zeroRecords": "No se encontraron resultados",           
            "paginate": {
                "first": "Primera",
                "last": "Última ",
                "next": "Siguiente",
                "previous": "Anterior"                
            }
        }
    });

    table.column(0).visible(false);
    table.column(3).visible(false);
    $('#tblAreas tbody').on('click', 'tr', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            EstadoSelect = 0;
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            EstadoSelect = 1;
        }        
        editar(row.data()[0], row.data()[3], row.data()[5], row.data()[1], row.data()[2]);
        DarDeBaja(row.data()[0]);
    });
});
//Crear Sub Bodegas
$("#btnCrear").click(function () {
    validar();
});
function validar() {
    $('#Direcciones').popover('dispose')
    $('#Descripcion').popover('dispose')
    $('#DescCorta').popover('dispose')

    if ($("#Direcciones option:selected").text() == "") {
        $('#Direcciones').popover('show')
        return false;
    } else if ($("#Descripcion").val() == "") {
        $('#Descripcion').popover('show')
        $("#Descripcion").focus();
        return false;
    } else if ($("#DescCorta").val() == "") {
        $('#DescCorta').popover('show')
        $("#DescCorta").focus();
        return false;
    }
    else {
        $("#frmCreate").submit();
    }
}
//Editar Sub Bodegas
function editar(idArea, idDireccion, esBodega, Descripcion, descCorta) {   
    $("#IdAreaEditar").val(idArea);
    $("select#DireccionesEditar").val(idDireccion);
    $('#EsBodegaEditar').attr('checked', esBodega);   
    $("#DescripcionEditar").val(Descripcion);
    $("#DescCortaEditar").val(descCorta);    
}
$("#btnEditarValidar").click(function () {
    validarEditar();
});
function validarEditar() {
    $('#DireccionesEditar').popover('dispose')
    $('#DescripcionEditar').popover('dispose')
    $('#DescCortaEditar').popover('dispose')

    if ($("#DireccionesEditar option:selected").text() == "") {
        $('#DireccionesEditar').popover('show')       
        return false;
    } else if ($("#DescripcionEditar").val() == "") {
        $('#DescripcionEditar').popover('show')
        $("#DescripcionEditar").focus();
        return false;
    } else if ($("#DescCortaEditar").val() == "") {
        $('#DescCortaEditar').popover('show')
        $("#DescCortaEditar").focus();
        return false;
    }
    else{       
        swal({
            title: 'Editando Sub Bodegas',
            text: "Seleccione 'Si' para editar la Sub Bodega o 'No' para cancelar.",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si',
            cancelButtonText: 'No',
            confirmButtonClass: 'btn btn-success',
            cancelButtonClass: 'btn btn-danger',
            buttonsStyling: false,
            allowOutsideClick: false,
        }).then(function () {
            $("#frmEditar").submit();
        }, function (dismiss) {
        })
    }    
}
$("#btnAceptarEditar").click(function () {
    $("#frmEditar").submit();
});
$("#btnEditar").click(function () {
    if (EstadoSelect) {
        $('#ModalEditar').modal('show');
    } else {
        swal({
            type: 'info',
            html: 'Favor seleccione una sub bodega',
            focusCancel: true,
            confirmButtonText: 'Aceptar',
            allowOutsideClick: false,
        })
    }
});
//Cambiar Estado Sub Bodegas
$("#btnCambioEstado").click(function () {
    if (EstadoSelect) {
        $('#ConfirmacionBajaAlta').modal('show');
    } else {
        swal({
            type: 'info',
            html: 'Favor seleccione una sub bodega',
            focusCancel: true,
            confirmButtonText: 'Aceptar',
            allowOutsideClick: false,
        })
    }
});
function DarDeBaja(idArea) {
    $("#IdAreaBajaAlta").val(idArea);        
}
$("#btnBajaAlta").click(function () {
    ValidarDarDeBaja();
});
function ValidarDarDeBaja() {   
    $("#frmBajaAlta").submit();
}

