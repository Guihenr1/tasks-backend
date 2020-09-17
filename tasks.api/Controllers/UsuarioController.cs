using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tasks.application.Interfaces;
using tasks.core.Utils;
using tasks.domain.ViewModels;

namespace tasks.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : BaseController<UsuarioController>
    {
        private readonly IUsuarioService usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar(AutenticacaoRequisicaoViewModel dados)
        {
            var result = new AutenticacaoRespostaViewModel();

            try
            {
                result = await usuarioService.Autenticar(dados);

                if (dados.ValidationResult.Errors.Count > 0)
                    return CreateValidationErrorResponse(dados.ValidationResult.Errors);
                    
                if (result == null) return CreateErrorResponse(ResourceMessages.LOGIN_INCORRETO, 401);
            }
            catch (Exception ex)
            {                
                CreateServerErrorResponse(ex, null);
            }

            return CreateResponse(result);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(AdicionarUsuarioViewModel dados)
        {
            try
            {
                var result = await usuarioService.Adicionar(dados);

                if (dados.ValidationResult.Errors.Count > 0)
                    return CreateValidationErrorResponse(dados.ValidationResult.Errors);
            }
            catch (Exception ex)
            {                
                CreateServerErrorResponse(ex, null);
            }

            return NoContent();
        }
    }
}
