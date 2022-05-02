using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Parametro
{
    public class HorarioInputDTO : IValidatableObject
    {
        [DisplayName("Horário de Abertura")]
        public TimeSpan HoraAbertura { get; set; }
        [DisplayName("Horário de Fechamento")]
        public TimeSpan HoraFechamento { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HoraAbertura >= HoraFechamento)
            {
                yield return new ValidationResult("O Horário está inválido.");
            }
        }
    }
}
