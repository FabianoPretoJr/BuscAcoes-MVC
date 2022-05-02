(function ($) {

    let acoes = [];


    // requisitar dados da empresa selecionada
    function obterDadosEmpresa(element) {
        const endereco = `${element.dataset.url}/${element.value}`;
        $.get(endereco, function (dados, status) {
            $("#cnpj").val(dados.cnpjEmpresa);
            $("#setor").val(dados.setorEmpresa);
            acoes = dados.acoes.map(a => {
                return { id: a.id, codigoNegociacao: a.codigoNegociacao, ativo: a.ativo }
            });
            
            drawTable();

            $("#SpanCodigosNegociacao").html("");
        });
    }

    document.getElementById("EmpresaId").addEventListener("change", function () { obterDadosEmpresa(this) })




    //Inserção e remoção dinâmica de Ação
    const inputAcao = document.getElementById("inputAcao");
    const btnAddAcao = document.getElementById("btnAddAcao");

    function drawTable() {
        const table = document.getElementById("tbAcoes");

        $("#tbAcoes tbody").empty();

        acoes.forEach((acao, index) => {
            const linha = table.insertRow(index);

            const coluna1 = linha.insertCell(0);
            
            coluna1.innerHTML = `<td>
                                    ${acao.ativo ? "" :
                                    "<span class='text-danger me-2'><i class='fas fa-exclamation-triangle'></i> Descontinuada</span>"}
                                    ${acao.codigoNegociacao}
                                   <input id="CodigosNegociacao_${index}" 
                                                    name="CodigosNegociacao[${index}]" 
                                                    value="${acao.codigoNegociacao}"
                                                    class="form-control acao" 
                                                    data-id="${acao.id}"
                                                    data-ativo="${acao.ativo}"
                                                    readonly type="hidden" />
                              </td>`;

            
            const coluna2 = linha.insertCell(1);

            if (acao.id > 0) {
                coluna2.innerHTML = "";
            } else {
                coluna2.innerHTML = `<td>
                                        <button data-codigo-negociacao="${acao.codigoNegociacao}" 
                                            type="button"
                                            class="deleteAcao btn btn-light-outline text-danger">
                                        <i class="fas fa-trash"></i>
                                            Excluir
                                        </button>
                                    </td>`;
            }
           
        });

        setDeleteEvents();

    }

    function deleteAcao(codigoNegociacao) {
        acoes = acoes.filter((acao) => acao.codigoNegociacao != codigoNegociacao);
        drawTable();
    }

    function setDeleteEvents() {
        let elements = document.getElementsByClassName("deleteAcao");

        Array.from(elements).forEach((element) => {
            const codigoNegociacao = element.dataset.codigoNegociacao;
            element.addEventListener("click", () => deleteAcao(codigoNegociacao))
        });
    }

    let elements = document.getElementsByClassName("acao");

    Array.from(elements).forEach((element) => {
        const codigoNegociacao = element?.value;
        const id = element?.dataset.id;
        const ativo = element?.dataset.ativo.toUpperCase() == "TRUE";

        acoes.push({ id, codigoNegociacao, ativo });
    });


    if (btnAddAcao && inputAcao) {
        inputAcao.addEventListener("keyup", function (event) {
            const value = inputAcao.value;
            const isValid = value.length >= 4;
            btnAddAcao.disabled = !(isValid);
        });

        btnAddAcao.addEventListener("click", function () {
            const codigoNegociacao = inputAcao.value?.replace(/\s/g, "").toUpperCase();
            const id = 0;
            const ativo = true;

            const jaExisteEssaAcaoNaTabela = acoes.find((acao) => acao.codigoNegociacao.toUpperCase() == codigoNegociacao);

            if (!jaExisteEssaAcaoNaTabela) {
                acoes.push({ id, codigoNegociacao, ativo });
                drawTable();
            }

            inputAcao.value = "";
        })

        drawTable();
    }

    

})(jQuery);