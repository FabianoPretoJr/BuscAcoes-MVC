using BuscAcoes.Dominio.DTOs.Alerta;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento.Util
{
    public class AlertaEmailSender : IEmailSender
    {
        public AlertaEmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public EmailSettings _emailSettings { get; }

        public async Task EnviarAlertaPorEmailAsync(AlertaDTO alerta)
        {
            var client = new SendGridClient(_emailSettings.Key);

            var de = new EmailAddress(_emailSettings.FromEmail, "Buscações");

            var para = new EmailAddress(alerta.ClienteEmail, alerta.ClienteNome);

            var dadosParaEmail = new Dictionary<string, string>
            {
                {"tolerancia", $"{alerta.Tolerancia:F}%" },

                {"acao_cod", alerta.CodigoNegociacao},
                {"acao_preco", alerta.AcaoPreco.ToString("C", CultureInfo.CurrentCulture) },
                {"acao_percentual", $"{alerta.AcaoPercentual:F}%" },

                {"url", "https://www.google.com/" },
                {"data", alerta.DataCriacao.ToString() }
            };

            var msg = MailHelper.CreateSingleTemplateEmail(de, para, _emailSettings.TemplateId, dadosParaEmail);

            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao enviar um email.");
            }
        }

    }
}
