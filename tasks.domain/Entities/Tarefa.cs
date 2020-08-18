using System;
using tasks.domain.Enums;

namespace tasks.domain.Entities
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public TarefaStatus Status { get; set; }
        public DateTime Estimado { get; set; }
        public DateTime? Concluido { get; set; }
    }
}