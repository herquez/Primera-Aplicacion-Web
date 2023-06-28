
function itemToDelete(idMarca) {
    document.getElementById("idMarca").value = idMarca;
}

function borrar() {
    document.getElementById("deleteForm").submit();
}

document.getElementById("nombre").onkeyup = function () {
    document.getElementById("searchForm").submit();
}
