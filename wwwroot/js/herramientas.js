function validar_campos_de_texto() {
  var codigo = $("#Codigo").val();
  var nombre = $("#Nombre").val();
  var descripcion = $("#Decripcion").val();

  return (
    codigo.trim() !== "" || nombre.trim() !== "" || descripcion.trim() !== ""
  );
}

function enviar_formulario() {
  var herramienta = {
    codigo: $("#Codigo").val(),
    nombre: $("#Nombre").val(),
    descripcion: $("#Descripcion").val(),
  };

  return $.ajax({
    type: "POST",
    url: "/api/Herramienta/registrar",
    data: JSON.stringify(herramienta),
    success: function (data, status) {
      animate_feedback("exito_formulario", 5000, 500, 500);
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

function registrar_evento_formulario() {
  $("#enviar").on("click", function (event) {
    event.preventDefault();

    var validForms = true;
    var form = $(".needs-validation")[0];
    $(form).addClass("was-validated");

    if (form.checkValidity() === false) {
      validForms = false;
    }

    if (!validForms || !validar_campos_de_texto()) {
      animate_feedback("error_formulario", 5000, 500, 500);
    } else {
      $(form).removeClass("was-validated");
      $.when(enviar_formulario()).done(function (respuestaServidor) {
        form.reset();
      });
    }
  });
}

function registrar_evento_borrar() {
  $("#cancelar").on("click", function (event) {
    $("#formulario_registro")[0].reset();
  });
}

$(document).ready(function () {
  console.log(
    "herramientas.js JavaScript - Daniel Guzman Chaves - 03101 – Programación avanzada en web - UNED IIIQ 2023"
  );

  registrar_evento_borrar();
  registrar_evento_formulario();
});
