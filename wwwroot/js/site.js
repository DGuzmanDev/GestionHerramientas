﻿function animate_feedback(element_id, timeout, show_duration, hide_duration) {
  $("#" + element_id).show(show_duration);
  setTimeout(function () {
    $("#" + element_id).hide(hide_duration);
  }, timeout);
}

$(document).ready(function () {
  console.log(
    "site.js JavaScript - Daniel Guzman Chaves - 03101 – Programación avanzada en web - UNED IIIQ 2023"
  );
});
