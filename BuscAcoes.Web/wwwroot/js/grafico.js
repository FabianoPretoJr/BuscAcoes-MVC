var labels = [];
var data = [];

$.get(endereco, function (dados, status) {
    for (let i = 0; i < dados.length; i++) {
        labels.push(dados[i].codigoNegociacao);
        data.push(dados[i].preco);
    }

    var canvas = document.getElementsByClassName("floating-bars");
    var grafico = new Chart(canvas, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: "Valor da ação",
                data: data,
                backgroundColor: ['rgba(54, 162, 235, 0.6)'],
                borderWidth: 1,
                maxBarThickness: 60
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                },
                title: {
                    display: true
                }
            }
        }
    });
});