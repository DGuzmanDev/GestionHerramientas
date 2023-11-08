var colaborador = {};
var lista_herramientas_prestamo = [];
var lista_herramientas_devolucion = [];


function limpiar_busqueda_colaborador() {
    $("#Identificacion").removeClass("input-valid");
    $("#Identificacion").removeClass("input-invalid");
    $("#error_id").hide();
    $("#error_busqueda_colaborador").hide();
    $("#datos-colaborador").hide(500);
}

function mostrar_error_busqueda_colaborador(error_id, timeout) {
    $("#Identificacion").addClass("input-invalid");
    animate_feedback(error_id, timeout, 500, 500);
}

function buscar_colaborador(identificacion) {
    // aqui tengo que usa otro endpoint o solicitar en otro adicional los datos asociados a este compa
    // es decir, los datos de las herramientas prestadas en base al colaborador ID
    return $.ajax({
        type: "GET",
        url:
            "/api/Colaborador/buscar/identificacion?identificacion=" +
            encodeURIComponent(identificacion),
        success: function (data, status) {
            colaborador = data;

            if (colaborador.id != null) {
                $("#DatosColaborador").val(
                    colaborador.nombre + " " + colaborador.apellidos
                );
                $("#datos-colaborador").show(1000);
            } else {
                mostrar_error_busqueda_colaborador("error_busqueda_colaborador", 6000);
            }
        },
        error: function (data, status) {
            console.log(
                "HTTP request error, status " + status + " data " + JSON.stringify(data)
            );

            // aqui tengo que validar cual es el error y reaccionar acorder
            // por ejemplo, si hay un error por duplicidad, por ahor solo navego al error page
            // window.location.replace("/Home/Error");
        },
        dataType: "json",
        contentType: "application/json; charset=utf-8",
    });
}

function registrar_evento_busqueda_colaborador() {
    $("#buscar_colaborador").on("click", function (event) {
        limpiar_busqueda_colaborador();

        var identificacion = $("#Identificacion").val();

        if (identificacion !== undefined && identificacion.trim() !== "") {
            $("#Identificacion").addClass("input-valid");
            identificacion = identificacion.trim();
            buscar_colaborador(identificacion);
        } else {
            mostrar_error_busqueda_colaborador("error_id", 3000);
        }
    });
}

function actualizar_tabla_seleccion(herramienta) {
    $("#seleccion-body").html("");

    var tr =
        '<tr id="seleccion-' + herramienta.id + '">' +
        "<td>" + herramienta.codigo + "</td>" +
        "<td>" + herramienta.nombre + "</td>" +
        "<td>" + herramienta.fechaDevolucion + "</td>" +
        '<td><div class="d-flex justify-content-center">' +
        '<button id="herramienta-seleccion-' + herramienta.id + '" type="button" class="btn btn-danger ms-2">' +
        '<i class="far fa-trash-alt"></i></button></div></td></tr>';

    $("#seleccion-body").append(tr);

    $("#herramienta-seleccion-" + herramienta.id).on("click", function (event) {
        var herramientaId = this.id.replace("herramienta-seleccion-", "");

        for (let index = 0; index < lista_herramientas_prestamo.length; index++) {
            var elemento = lista_herramientas_prestamo[index];

            if (elemento.id == herramientaId) {
                lista_herramientas_devolucion.splice(index, 1);
                $("#seleccion-" + herramientaId).remove();
                $("#herramienta-" + herramientaId).prop("disabled", false);
                break;
            }
        }
    });
}

function cargar_resultados_herramientas(herramientas) {
    $("#result-body").html("");

    if (herramientas.length > 0) {
        for (let index = 0; index < herramientas.length; index++) {
            var herramienta = herramientas[index];
            var tr =
                '<tr id="' + herramienta.id + '">' +
                "<td>" + herramienta.codigo + "</td>" +
                "<td>" + herramienta.nombre + "</td>" +
                '<td class="desc-row">' +
                '<p class="w-100 h-100 overflow-scroll desc-row">' +
                herramienta.descripcion + "</p></td>" +
                "<td>" + herramienta.fechaDevolucion + "</td>" +
                '<td><div class="d-flex justify-content-center">' +
                '<button id="herramienta-' + herramienta.id + '" type="button" class="btn btn-success ms-2">'
                + '<i class="fa-solid fa-right-left"></i></button></div></td></tr>';

            $("#result-body").append(tr);
            $("#herramienta-" + herramienta.id).on("click", function (event) {
                var herramientaId = this.id.replace("herramienta-", "");

                var herramientaSelecionada = herramientas.find(item => {
                    return item.id == herramientaId
                });

                lista_herramientas_devolucion.push(herramientaSelecionada);
                actualizar_tabla_seleccion(herramientaSelecionada);
            });
        }
    } else {
        $("#result-body").html("No se han encontrado resultados");
    }
}

function enviar_formulario() {
    //    aqui es mandar a algun endpoint del devolver herramientas
}

function registrar_evento_guardar() {
    $("#enviar").on("click", function (event) {
        enviar_formulario();
    });
}

$(document).ready(function () {
    console.log(
        "devolucion_herramientas.js JavaScript - Daniel Guzman Chaves - 03101 – Programación avanzada en web - UNED IIIQ 2023"
    );

    registrar_evento_busqueda_colaborador();
    registrar_evento_guardar();
});
