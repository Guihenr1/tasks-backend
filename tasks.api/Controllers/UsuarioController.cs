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

        [HttpPost]
        public async Task<IActionResult> Autenticar(AutenticacaoRequisicao dados)
        {
            var result = new AutenticacaoResposta();

            try
            {
                result = await usuarioService.Autenticacao(dados);

                if (result == null) return CreateErrorResponse(ResourceMessages.LOGIN_INCORRETO, 401);
            }
            catch (Exception ex)
            {                
                CreateServerErrorResponse(ex, null);
            }

            return CreateResponse(result);
        }
    }
}
