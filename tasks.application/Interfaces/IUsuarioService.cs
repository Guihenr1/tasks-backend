using System;
using System.Threading.Tasks;
using tasks.domain.Entities;
using tasks.domain.ViewModels;

namespace tasks.application.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task<Usuario> ObterPorId(Guid id);
        Task<AutenticacaoResposta> Autenticacao(AutenticacaoRequisicao autenticacao);
    }
}
