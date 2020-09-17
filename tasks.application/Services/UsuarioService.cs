using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using tasks.application.Interfaces;
using tasks.domain.DTOs;
using tasks.domain.Entities;
using tasks.domain.Interfaces;
using tasks.domain.ViewModels;

namespace tasks.application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper mapper;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ISecurityService securityService;
        public UsuarioService(IMapper mapper
            , IUsuarioRepository usuarioRepository
            , ISecurityService securityService)
        {
            this.mapper = mapper;
            this.usuarioRepository = usuarioRepository;
            this.securityService = securityService;
        }

        public async Task<UsuarioDto> ObterPorId(Guid id)
        {
            return mapper.Map<UsuarioDto>(await usuarioRepository.ObterPorId(id));
        }

        public async Task<bool> Adicionar(AdicionarUsuarioViewModel usuario)
        {
            if (!usuario.EhValido()) return false;

            await usuarioRepository.Adicionar(mapper.Map<Usuario>(usuario));
            return await usuarioRepository.UnitOfWork.Commit();
        }

        public async Task<AutenticacaoRespostaViewModel> Autenticar(AutenticacaoRequisicaoViewModel autenticacao)
        {
            AutenticacaoRespostaViewModel resposta;
            if (!autenticacao.EhValido()) return null;

            var usuario = await usuarioRepository.ObterPorEmailESenha(
                mapper.Map<Usuario>(autenticacao)
            );

            if(usuario == null) return null;

            resposta = mapper.Map<AutenticacaoRespostaViewModel>(usuario);
            resposta.Token = securityService.gerarJwtToken(mapper.Map<UsuarioDto>(usuario));

            return resposta;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}