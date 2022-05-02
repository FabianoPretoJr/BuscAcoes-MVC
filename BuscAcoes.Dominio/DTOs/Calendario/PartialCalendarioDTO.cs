using BuscAcoes.Dominio.DTOs.DiasIndisponiveis;
using BuscAcoes.Dominio.Enum;
using System.Collections.Generic;
using System.Text.Json;

namespace BuscAcoes.Dominio.DTOs.Calendario
{
    public class PartialCalendarioDTO
    {
        public EPerfil Perfil { get; set; }
        public List<DiasIndisponiveisOutputDTO> DiasIndisponiveis { get; set; }
        public string DiasIndisponiveisJson
        {
            get
            {
                return JsonSerializer.Serialize(this.DiasIndisponiveis?.ToArray());
            }
        }
    }
}
