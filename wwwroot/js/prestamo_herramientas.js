var colaborador = {};
var lista_herramientas_prestamo = [];

function configurar_limite_fecha() {
    var min = new Date();
    var max = new Date();
    max.setDate(min.getDate() + 5);

    var minDay = min.getDate() < 10 ? "0" + min.getDate() : min.getDate();
    var minMonth = (min.getMonth() + 1) < 10 ? "0" + (min.getMonth() + 1) : (min.getMonth() + 1);

    var maxDay = max.getDate() < 10 ? "0" + max.getDate() : max.getDate();
    var maxMonth = (max.getMonth() + 1) < 10 ? "0" + (max.getMonth() + 1) : (max.getMonth() + 1);

    $("#fecha_devolucion").val(min.getFullYear() + "-" + minMonth + "-" + minDay);
    $("#fecha_devolucion").attr("min", min.getFullYear() + "-" + minMonth + "-" + minDay);
    $("#fecha_devolucion").attr("max", max.getFullYear() + "-" + maxMonth + "-" + maxDay);
}

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

function limpiar_busqueda_herramienta() {
    $("#Filtro").removeClass("input-valid");
    $("#Filtro").removeClass("input-invalid");
    $("#error_filtro").hide();
}

function mostrar_error_seleccion_herramienta(mensaje) {
    $("#error_seleccion_msg").html(mensaje);
    animate_feedback("error_seleccion", 5000, 500, 500);
}

function actualizar_tabla_seleccion() {
    for (let index = 0; index < lista_herramientas_prestamo.length; index++) {
        var herramienta = lista_herramientas_prestamo[index];

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
            for (let index = 0; index < lista_herramientas_prestamo.length; index++) {
                var elemento = lista_herramientas_prestamo[index];
                if (elemento.id == herramienta.id) {
                    lista_herramientas_prestamo.splice(index, 1);
                    $("#seleccion-" + herramienta.id).remove();
                    break;
                }
            }
        });
    }
}

function agregar_herramienta(herramienta) {
    var esValida = true;
    $("#error_seleccion_msg").html("");
    $("#error_seleccion").hide();

    if (herramienta.colaboradorId === 0) {
        for (let index = 0; index < lista_herramientas_prestamo.length; index++) {
            var elmento = lista_herramientas_prestamo[index];

            if (elmento.id == herramienta.id) {
                esValida = false;
                break;
            }
        }

        if (esValida) {
            if (colaborador !== undefined && colaborador.id !== 0) {
                var fecha_devolucion = $("#fecha_devolucion").val();

                if (fecha_devolucion != undefined && fecha_devolucion !== '') {
                    herramienta.colaboradorId = colaborador.id;
                    herramienta.fechaDevolucion = $("#fecha_devolucion").val();
                    lista_herramientas_prestamo.push(herramienta);
                    actualizar_tabla_seleccion();
                } else {
                    animate_feedback("error_fecha", 5000, 500, 500);
                    mostrar_error_seleccion_herramienta("La fecha de devolucion es invalida");
                }
            } else {
                mostrar_error_seleccion_herramienta("El colaborador seleccionado es invalido");
            }
        } else {
            mostrar_error_seleccion_herramienta("La herramienta seleccionada ya ha sido seleccionada previamente");
        }
    } else {
        mostrar_error_seleccion_herramienta("La herramienta seleccionada no esta disponible para prestamo");
    }
}

function cargar_resultados_herramientas(herramientas) {
    $("#result-body").html("");

    if (herramientas.length > 0) {
        for (let index = 0; index < herramientas.length; index++) {
            var herramienta = herramientas[index];

            var estadoBoton = herramienta.colaboradorId !== 0 ? "disabled" : "";
            var estado = herramienta.colaboradorId !== 0 ? "No Disponible" : "Disponible";
            var fechaDevolucion = herramienta.colaboradorId !== 0 ? herramienta.fechaDevolucion : "N/A";
            var identificacionColaborador = herramienta.colaboradorId !== 0 ? herramienta.colaborador.identificacion : "N/A";

            var tr =
                '<tr id="' + herramienta.id + '">' +
                "<td>" + herramienta.codigo + "</td>" +
                "<td>" + herramienta.nombre + "</td>" +
                '<td class="desc-row">' +
                '<p class="w-100 h-100 overflow-scroll desc-row">' +
                herramienta.descripcion + "</p></td>" +
                "<td>" + estado + "</td>" +
                "<td>" + identificacionColaborador + "</td>" +
                "<td>" + fechaDevolucion + "</td>" +
                '<td><div class="d-flex justify-content-center">' +
                '<button id="herramienta-' + herramienta.id + '" type="button" class="btn btn-success ms-2" ' +
                estadoBoton + '><i class="fas fa-check"></i></button></div></td></tr>';

            $("#result-body").append(tr);
            $("#herramienta-" + herramienta.id).on("click", function (event) {
                agregar_herramienta(herramienta);
                $("#herramienta-" + herramienta.id).prop("disabled", true);
            });
        }
    } else {
        $("#result-body").html("No se han encontrado resultados");
    }
}

function buscar_herramienta(filtro) {
    return $.ajax({
        type: "GET",
        url:
            "/api/Herramienta/buscar/nombreOcodigo?filtro=" +
            encodeURIComponent(filtro),
        success: function (data, status) {
            cargar_resultados_herramientas(data);
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

function registrar_evento_busqueda_herramientas() {
    $("#buscar_herramienta").on("click", function (event) {
        limpiar_busqueda_herramienta();

        var filtro = $("#Filtro").val();

        if (filtro !== undefined && filtro.trim() !== "") {
            filtro = filtro.trim();
            buscar_herramienta(filtro);
        } else {
            $("#Filtro").addClass("input-invalid");
            animate_feedback("error_filtro", 6000, 500, 500);
        }
    });
}

$(document).ready(function () {
    console.log(
        "prestamo_herramientas.js JavaScript - Daniel Guzman Chaves - 03101 – Programación avanzada en web - UNED IIIQ 2023"
    );

    configurar_limite_fecha();
    registrar_evento_busqueda_colaborador();
    registrar_evento_busqueda_herramientas();
});
