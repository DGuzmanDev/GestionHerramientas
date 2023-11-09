var colaborador = {};
var lista_herramientas_prestamo = [];
var lista_herramientas_devolucion = [];


function reiniciar_formulario() {
    colaborador = {};
    lista_herramientas_prestamo = [];
    lista_herramientas_devolucion = [];
    $("#result-body").html("");
    $("#seleccion-body").html("");
    $("#Identificacion").removeClass("input-valid");
    $("#Identificacion").removeClass("input-invalid");
    $("#error_id").hide();
    $("#error_busqueda_colaborador").hide();
    $("#devolucion_error").hide();
    $("#datos-colaborador").hide();
}

function mostrar_error_busqueda_colaborador(error_id, timeout) {
    $("#Identificacion").addClass("input-invalid");
    animate_feedback(error_id, timeout, 500, 500);
}

function mostrar_error_devolucion_herramientas(msg, timeout) {
    $("#devolucion_error_msg").html(msg);
    animate_feedback("devolucion_error", timeout, 500, 500);
}

function cargar_resultados_herramientas() {
    for (let index = 0; index < lista_herramientas_prestamo.length; index++) {
        var herramienta = lista_herramientas_prestamo[index];
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

            var exists = lista_herramientas_devolucion.find(item => {
                return item.id == herramientaId
            }) !== undefined;

            if (!exists) {
                var herramientaSelecionada = lista_herramientas_prestamo.find(item => {
                    return item.id == herramientaId
                });

                lista_herramientas_devolucion.push(herramientaSelecionada);
                actualizar_tabla_seleccion(herramientaSelecionada);
            } else {
                mostrar_error_devolucion_herramientas("Esta herramienta ya ha sido seleccionada", 3000);
            }
        });
    }
}

function cargar_tabla_herramientas() {
    var hoy = new Date();
    var fechaLimite = new Date();
    fechaLimite.setDate(hoy.getDate() - 5);

    var fechaLimiteTimestamp = Date.parse(new Date());
    var herramientasAtrasadas = 0;

    for (let index = 0; index < lista_herramientas_prestamo.length; index++) {
        var herramienta = lista_herramientas_prestamo[index];
        if (Date.parse(herramienta.fechaDevolucion) < fechaLimiteTimestamp) {
            herramientasAtrasadas++;
        }
    }

    if (herramientasAtrasadas > 0) {
        mostrar_error_devolucion_herramientas('El colaborador posee <span class="fw-bolder">' + herramientasAtrasadas + '</span> herramientas atrasadas', 6000);
    }

    $("#datos-colaborador").show(1000);
    cargar_resultados_herramientas();
}

function buscar_herramientas(colaboradorId) {
    return $.ajax({
        type: "GET",
        url:
            "/api/Herramienta/buscar/colaboradorId?id=" +
            encodeURIComponent(colaboradorId),
        success: function (data, status) {
            lista_herramientas_prestamo = data;

            if (lista_herramientas_prestamo.length > 0) {
                cargar_tabla_herramientas();
            } else {
                $("#result-body").html("No se han encontrado resultados");
                mostrar_error_devolucion_herramientas("El colaborador seleccionado no posee herramientas prestadas", 6000);
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

function buscar_colaborador(identificacion) {
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

                buscar_herramientas(colaborador.id);
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
        reiniciar_formulario();

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

        for (let index = 0; index < lista_herramientas_devolucion.length; index++) {
            var elemento = lista_herramientas_devolucion[index];

            if (elemento.id == herramientaId) {
                lista_herramientas_devolucion.splice(index, 1);
                $("#seleccion-" + herramientaId).remove();
                $("#herramienta-" + herramientaId).prop("disabled", false);
                break;
            }
        }
    });
}

function enviar_formulario() {
    return $.ajax({
        type: "PUT",
        url: "/api/Herramienta/devolver/lista",
        data: JSON.stringify(lista_herramientas_devolucion),
        success: function (data, status) {
            animate_feedback("exito_formulario", 5000, 500, 500);
            reiniciar_formulario();
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
