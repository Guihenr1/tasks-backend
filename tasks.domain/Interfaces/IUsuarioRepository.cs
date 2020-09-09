using System;
using System.Threading.Tasks;
using tasks.domain.Entities;

namespace tasks.domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPorEmailESenha(Usuario usuario);
        Task<Usuario> ObterPorId(Guid id);
    }
}