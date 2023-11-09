function validar_campos_de_texto() {
  var identificacion = $("#Identificacion").val();
  var nombre = $("#Nombre").val();
  var apellidos = $("#Apellidos").val();

  return (
    identificacion.trim() !== "" ||
    nombre.trim() !== "" ||
    apellidos.trim() !== ""
  );
}

function enviar_formulario() {
  var colaborador = {
    identificacion: $("#Identificacion").val(),
    nombre: $("#Nombre").val(),
    apellidos: $("#Apellidos").val(),
    estado: $("#Estado").find(":selected").val() === "true",
  };

  return $.ajax({
    type: "POST",
    url: "/api/Colaborador/registrar",
    data: JSON.stringify(colaborador),
    success: function (data, status) {
      animate_feedback("exito_formulario", 5000, 500, 500);
    },
    error: function (data, status) {
      window.location.replace("/Home/Error?errorMessage=" +
        encodeURIComponent(data.responseText) + "&httpError=" +
        encodeURIComponent(data.status + " " + data.statusText));
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

$(document).ready(function () {
  console.log(
    "colaboradores.js JavaScript - Daniel Guzman Chaves - 03101 – Programación avanzada en web - UNED IIIQ 2023"
  );

  registrar_evento_formulario();
});
