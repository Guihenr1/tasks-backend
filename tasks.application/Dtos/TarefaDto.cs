using System;
using tasks.domain.Enums;

namespace tasks.application.Dtos
{
    public class TarefaDto
    {
        public string Descricao { get; set; }
        public DateTime Estimado { get; set; }
        public DateTime? Concluido { get; set; }
    }
}