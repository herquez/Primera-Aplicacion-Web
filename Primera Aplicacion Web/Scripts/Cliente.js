
function itemToDelete(idCliente) {
    document.getElementById("idCliente").value = idCliente;
}

function borrar() {
    document.getElementById("deleteForm").submit();
}

document.getElementById("searchName").onkeyup = function () {
    document.getElementById("searchForm").submit();
}