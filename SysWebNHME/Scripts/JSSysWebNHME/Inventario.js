var EstadoSelect = 0;
var id = -1;
$(document).ready(function () {
    //$('#ModalCreate').modal('show')
    var table =  $('#tblMovimientos').DataTable({
        "language": {
            "emptyTable": "No hay datos disponibles en la tabla",
            "lengthMenu": '_MENU_ ',
            "search": '<i class="fa fa-search" aria-hidden="true"></i> Buscar ',
            "loadingRecords": "cargando...",
            "infoEmpty": "Mostrando 0 a 0 de 0 resultados",
            "info": "Mostrando _START_ de _END_ en _TOTAL_ resultados",
            "zeroRecords": "No se encontraron resultados",
            "infoFiltered": "(Filtrados de _MAX_ registros en total)",
            "paginate": {
                "first": "Primera",
                "last": "Última ",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });

    table.column(0).visible(false);
    table.column(5).visible(false);
    
    $('#tblMovimientos tbody').on('click', 'tr', function () {
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
            id = -1;
        }
        editar(row.data()[0], row.data()[1], row.data()[2], row.data()[5]);
        id = row.data()[0];
        
    });
});
$("#btnCrear").click(function () {
    validar();
});

function validar() {
    $('#TipoMov').popover('dispose')
    $('#Descripcion').popover('dispose')
    $('#DescCorta').popover('dispose')

    if ($("#Descripcion").val() == "") {
        $('#Descripcion').popover('show')
        $("#Descripcion").focus();
        return false;
    } else if ($("#DescCorta").val() == "") {
        $('#DescCorta').popover('show')
        $("#DescCorta").focus();
        return false;
    } else if ($("#TipoMov option:selected").val() == "") {
        $('#TipoMov').popover('show')
        return false;
    }
    else {
        $("#frmCreate").submit();
    }
}
function editar(idTipo, decrip, descripCorta, tipo) {

    $("#IDTipoMov").val(idTipo);
    $("select#TipoMovEdit").val(tipo);
    $("#DescripcionEdit").val(decrip);
    $("#DescCortaEdit").val(descripCorta);
    //$('#ModalEditar').modal('show');
}

$("#btnEditarValidar").click(function () {
    validarEditar();
});

$("#btnEditar").click(function () {
    if (EstadoSelect) {
        $('#ModalEditar').modal('show');
    } else {
        swal({
            type: 'info',
            html: 'Favor seleccione un concepto',
            focusCancel: true,
            confirmButtonText: 'Aceptar',
            allowOutsideClick: false,
        })
    }
});

function validarEditar() {
    $('#TipoMovEdit').popover('dispose')
    $('#DescripcionEdit').popover('dispose')
    $('#DescCortaEdit').popover('dispose')

    if ($("#DescripcionEdit").val() == "") {
        $('#DescripcionEdit').popover('show')
        $("#DescripcionEdit").focus();
        return false;
    } else if ($("#DescCortaEdit").val() == "") {
        $('#DescCortaEdit').popover('show')
        $("#DescCortaEdit").focus();
        return false;
    } else if ($("#TipoMovEdit option:selected").val() == "") {
        $('#TipoMovEdit').popover('show')
        return false;
    }
    else {
        swal({
            title: 'Editando Movimientos',
            text: "Seleccione 'Si' para editar la el movimiento o 'No' para cancelar.",
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

function bajaMo() {
    if (id != -1 && id != "") {
        $("#IDTipoMovBaja").val(id);
        $('#ConfirmacionBajaAlta').modal('show');
    } else {
        swal({
            type: 'info',
            html: 'Favor seleccione un concepto',
            focusCancel: true,
            confirmButtonText: 'Aceptar',
            allowOutsideClick: false,
        })
    }
}

$("#btnBajaAlta").click(function () {
    $("#frmBajaAlta").submit();
});