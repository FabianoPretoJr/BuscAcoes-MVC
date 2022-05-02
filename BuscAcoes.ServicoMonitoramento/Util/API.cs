using BuscAcoes.Dominio.DTOs.Monitoramento;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento.Util
{
    public static class API
    {
        public static async Task<MonitoramentoInputDTO> Requisitar(string linkEndpointApi, string chaveApi, string symbol, int idAcao)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{linkEndpointApi}{symbol}&key={chaveApi}");
                response.EnsureSuccessStatusCode();
                string jsonResult = await response.Content.ReadAsStringAsync();

                if (jsonResult.Equals("{}"))
                    throw new ArgumentException("Retorno de requisicao invalido.");

                var acao = JsonConvert.DeserializeObject<dynamic>(jsonResult);

                var chaveValida = acao.valid_key;
                if (chaveValida == false) throw new ArgumentException("Chave de API invalida.");

                var monitoramentoDTO = new MonitoramentoInputDTO()
                {
                    Id = 0,
                    PrecoAcao = acao[symbol].price,
                    PercentualVariacao = acao[symbol].change_percent,
                    AtualizadoEm = acao[symbol].updated_at,
                    IdAcao = idAcao                    
                };

                return monitoramentoDTO;
            }
        }
    }
}
