document.addEventListener("DOMContentLoaded", function () {

    const containerDia = document.getElementById("containerDia");
    const containerBotoes = document.getElementById("containerBotoes");

    function limparContainers() {
        containerBotoes.innerHTML = "";
        containerDia.innerHTML = "";
    }


    let datasIndiponiveis = datasJson?.map(d => {
        return {
            ...d,
            Data: new Date(d.Data).toLocaleDateString()
        };
    });

    const modalElement = document.getElementById('modalDia');

    modalElement.addEventListener('hidden.bs.modal', function (event) {
        containerDia.innerHTML = "";
    })
    modalElement.addEventListener('hidden.bs.modal', function () {
        limparContainers();
    })

    const modal = new bootstrap.Modal(modalElement, {
        keyboard: false
    });

    const calendar = document.getElementById("datetimepicker-dashboard").flatpickr({
        locale: "pt",
        inline: true,
        prevArrow: "<span title=\"Previous month\"><i class=\"fas fa-arrow-left\"></i></span>",
        nextArrow: "<span title=\"Next month\"><i class=\"fas fa-arrow-right\"></i></span>",
        minDate: "today",
        onDayCreate: handleDayCreate,
        disable: [
            function (date) {
                const indexDia = date.getDay();
                const sabado = 6, domingo = 0;

                return (indexDia === domingo || indexDia === sabado);

            }
        ],
    });




    function handleDayCreate(dObj, dStr, fp, dayElem) {
        const localeDateString = dayElem.dateObj.toLocaleDateString();
        const diaIndisponivel = datasIndiponiveis?.find(d => d.Data === localeDateString);
        const diaValido = !(dayElem.classList.contains("flatpickr-disabled"));

        if (diaValido) {
            if (diaIndisponivel) {
                dayElem.title = diaIndisponivel.Descricao;
                dayElem.innerHTML += `<span class='event'></span>`;

                if (podeEditar) handleDiaEditavel(diaIndisponivel, dayElem);

            } else if (podeEditar) {
                console.log("Cadastravel");
                handleDiaCadastravel(localeDateString, dayElem);
            }
        }

        

    }

    function handleDiaCadastravel(dia, dayElem) {
        dayElem.addEventListener("click", function () {
            const dataInicial = {
                dataEscolhida: dia
            };

            definirConteudoModal(cadastrarDiaUrl, dia, dataInicial);
        })
    }

    function handleDiaEditavel(data, dayElem) {
        const dia = data.Data;

        const editUrl = `${editarDiaUrl}/${data.Id}`;

        dayElem.addEventListener("click", () => {
            definirConteudoModal(editUrl, dia);
        });
    }




    function definirConteudoModal(url, dia, dataInicial) {
        $.post(url, dataInicial, function (partialView) {
            containerDia.innerHTML = partialView;

            const formDeletarDia = document.getElementById("formDeletarDia");
            const formDia = document.getElementById("formDia");
            $(formDia).validate({
                debug: true,
                rules: {
                    Descricao: {
                        required: true
                    }
                },
                messages: {
                    Descricao: {
                        required: "O campo Descrição é obrigatório."
                    }
                }
            });

            formDia.addEventListener("keypress", function (event) {
                var keyPressed = event.keyCode || event.which;
                if (keyPressed === 13) {
                    event.preventDefault();
                    onSalvarClick(formDia, dia);
                    return false;
                }
            });


            const btnSalvarDia = document.createElement("button");
            btnSalvarDia.textContent = "Salvar";
            btnSalvarDia.classList = "btn btn-primary";

            btnSalvarDia.addEventListener("click", () => onSalvarClick(formDia, dia));

            const btnFechar = document.createElement("button");
            btnFechar.textContent = "Fechar";
            btnFechar.setAttribute("data-bs-dismiss", "modal");
            btnFechar.classList = "btn btn-secondary";

            containerBotoes.appendChild(btnFechar);

            const paraEditar = (dataInicial == null);
            if (paraEditar) {
                const btnDeletarDia = document.createElement("button");
                btnDeletarDia.textContent = "Deletar";
                btnDeletarDia.classList = "btn btn-danger";

                btnDeletarDia.addEventListener("click", () => onDeletarClick(formDeletarDia, dia));

                containerBotoes.appendChild(btnDeletarDia);
            }

            containerBotoes.appendChild(btnSalvarDia);

            modal.show();
        });
    }

    function onDeletarClick(form, dia) {
        datasIndiponiveis = datasIndiponiveis.filter(d => d.Data != dia);

        enviarFormParaController(form, () => {
            calendar.redraw();
        });
    }

    function onSalvarClick(form, dia) {
        if (!$(form).valid()) return;

        enviarFormParaController(form, (result) => {
            datasIndiponiveis = datasIndiponiveis.filter(d => d.Data != dia);
            datasIndiponiveis.push({
                Id: result.id,
                Data: new Date(result.data).toLocaleDateString(),
                Descricao: result.descricao,
                PodeEditar: true
            })

            calendar.redraw();
        });
    }

    function enviarFormParaController(form, callback) {
        const body = Object.fromEntries(new FormData(form).entries());

        $.post(form.action, body, function (result) {
            if (callback) callback(result);

            modal.hide();
        })
    }


});