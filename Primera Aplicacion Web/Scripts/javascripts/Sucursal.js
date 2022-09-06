
function itemToDelete(idSucursal) {
    document.getElementById("idSucursal").value = idSucursal;
}

function borrar() {
    document.getElementById("deleteForm").submit();
}

document.getElementById("nombre").onkeyup = function () {
    document.getElementById("searchForm").submit();
}