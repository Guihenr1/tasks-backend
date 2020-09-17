using System;
using tasks.domain.Entities;

namespace tasks.domain.ViewModels
{
    public class AutenticacaoRespostaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}