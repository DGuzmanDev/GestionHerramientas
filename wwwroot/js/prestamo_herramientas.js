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
      var colaborador = data;

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

$(document).ready(function () {
  console.log(
    "prestamo_herramientas.js JavaScript - Daniel Guzman Chaves - 03101 – Programación avanzada en web - UNED IIIQ 2023"
  );

  registrar_evento_busqueda_colaborador();
});
