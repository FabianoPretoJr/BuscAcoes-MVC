$("#limparFiltro").click(function () {
    $("#codigoAcao").val("");
    $("#dataInicio").val("");
    $("#dataFim").val("");
});

const startDateElement = document.getElementById("dataInicio");
const endDateElement = document.getElementById("dataFim");

startDateElement.addEventListener('change', (event) => { fixDates(); });
endDateElement.addEventListener('change', (event) => { fixDates(); });

function initializeDates() {
    const date = new Date();
    const year = date.getFullYear();
    const month = date.getMonth();
    const day = date.getDate();
    const now = new Date(year, month, day);
    const nowLess100years = new Date(year - 100, month, day);
    startDateElement.min = nowLess100years.toISOString().split("T")[0];
    endDateElement.max = now.toISOString().split("T")[0];
}

function fixDates() {
    endDateElement.min = startDateElement.value;
    startDateElement.max = endDateElement.value;
}