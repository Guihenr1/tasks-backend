using System;

namespace tasks.domain.ViewModels
{
    public class TarefaResponseViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Estimado { get; set; }
        public DateTime? Concluido { get; set; }
        public string Status { get; set; }
    }
}