using System;
using System.Threading.Tasks;
using tasks.domain.DTOs;
using tasks.domain.ViewModels;

namespace tasks.application.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task<UsuarioDto> ObterPorId(Guid id);
        Task<AutenticacaoRespostaViewModel> Autenticar(AutenticacaoRequisicaoViewModel autenticacao);
        Task<bool> Adicionar(AdicionarUsuarioViewModel usuario);
    }
}
