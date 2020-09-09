using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using tasks.application.Interfaces;
using tasks.domain.Entities;
using tasks.domain.Interfaces;
using tasks.domain.ViewModels;

namespace tasks.application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper mapper;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly AppSettings appSettings;
        public UsuarioService(IMapper mapper
            , IUsuarioRepository usuarioRepository
            , IOptions<AppSettings> appSettings)
        {
            this.mapper = mapper;
            this.usuarioRepository = usuarioRepository;
            this.appSettings = appSettings.Value;
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            return await usuarioRepository.ObterPorId(id);
        }

        public async Task<AutenticacaoResposta> Autenticacao(AutenticacaoRequisicao autenticacao)
        {
            AutenticacaoResposta resposta;
            var usuario = await usuarioRepository.ObterPorEmailESenha(
                mapper.Map<Usuario>(autenticacao)
            );

            if(usuario == null) return null;

            resposta = mapper.Map<AutenticacaoResposta>(usuario);
            resposta.Token = gerarJwtToken(usuario);

            return resposta;
        }

        private string gerarJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", usuario.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}