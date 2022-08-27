
function itemToDelete(idCliente) {
    document.getElementById("idCliente").value = idCliente;
}

function borrar() {
    document.getElementById("deleteForm").submit();
}

document.getElementById("searchName").onkeyup = submitForm;

document.getElementById("iidsexo").onchange = submitForm;

function submitForm() {
    document.getElementById("searchForm").submit();
}