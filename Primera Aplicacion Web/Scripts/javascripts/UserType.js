var iidtipousuario = document.getElementById("iidtipousuario");
var nombre = document.getElementById("nombre");
var descripcion = document.getElementById("descripcion");

var search_form = document.getElementById("search_form");

iidtipousuario.onkeyup = function () {
    search_form.submit();
}

nombre.onkeyup = function () {
    search_form.submit();
}

descripcion.onkeyup = function () {
    search_form.submit();
}