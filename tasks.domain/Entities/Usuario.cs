using System;
using System.Text.Json.Serialization;
using tasks.domain.Interfaces;

namespace tasks.domain.Entities
{
    public class Usuario : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        
        [JsonIgnore]
        public string Senha { get; private set; }

        public Usuario(Guid id, string nome, string email, string senha)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}