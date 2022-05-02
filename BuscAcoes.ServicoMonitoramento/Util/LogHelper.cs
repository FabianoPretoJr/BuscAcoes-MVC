using System;
using System.IO;

namespace BuscAcoes.ServicoMonitoramento.Util
{
    public static class LogHelper
    {
        public static string ObterArquivoLog()
        {
            string docPath = Path.Combine(Environment.CurrentDirectory, "logs");

            Directory.CreateDirectory(docPath);

            return Path.Combine(docPath, "log_.txt");
        }
    }
}
