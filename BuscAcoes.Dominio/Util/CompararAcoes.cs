using BuscAcoes.Dominio.DTOs.Acao;
using System;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Util
{
    public class CompararAcoes : IEqualityComparer<AcaoMonitoradaDTO>
    {
        public bool Equals(AcaoMonitoradaDTO x, AcaoMonitoradaDTO y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.AcaoId == y.AcaoId && x.Codigo_Negociacao == y.Codigo_Negociacao;
        }

        public int GetHashCode(AcaoMonitoradaDTO acaoMonitoradaDTO)
        {
            if (Object.ReferenceEquals(acaoMonitoradaDTO, null)) return 0;

            int hashAcaoMonitoradaName = acaoMonitoradaDTO.Codigo_Negociacao == null ? 0 : acaoMonitoradaDTO.Codigo_Negociacao.GetHashCode();

            int hashAcaoMonitoradaCode = acaoMonitoradaDTO.Codigo_Negociacao.GetHashCode();

            return hashAcaoMonitoradaName ^ hashAcaoMonitoradaCode;
        }
    }
}
