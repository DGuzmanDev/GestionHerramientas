# GestionHerramientas
Repositorio para el desarrollo del proyecto 1 del curso 03101 – Programación avanzada en web de la UNED. 

# Pendientes
## TODO
1. Terminar el diseño de la pantalla de prestamo de herramienta
2. Agregar la funcionalidad JS al prestamo de herramienta
   1. Funcionalidad para buscar Colaborador
   2. Validacion de colaborador inactivo
   3. Consultar cantidad de herramientas prestadas al colaborador
   4. Buscar Herramienta
   5. Mostrar la tabla
      1. Deshabilitar herramientas prestadas, mostra info del colaborador que la posee y demas datos
   6. Seleccion de herramientas a prestar, maximo 5 del total del colaborador
   7. Seleccionar fecha de devolucion, mayor o igual a hoy y, maximo 5 dias
   8. HTTP request(s), idealmente solo 1 con todas las herramientas y el colaborador ID, sino serian varias por herramientas, nada mas ocuparia otro endpoint adicional para que reciba un list en lugar de 1 solo elemento, es mas, el que tengo puede hacer el brete, pero es todo o nada entonces tendria que actualizar el service para que valide que toda la lista esta bien antes de invocar la capa de datos.
   9. Pantalla de devolucion de herramientas
   10. Agregar las tablas de listado de herramienta y colaborador en las pantallas de registro
       1.  Para la de colaborador se puede meter un field que permita actualiza el campo de estado
       2.  Para la de herramienta mostrar los datos de las herramientas prestadas
   11. Agregar el error handling por duplicados y bad requests donde corresponda
   12. Agregar todos los docs faltantes
   13. Grabar el video
   14. Hacer el documento