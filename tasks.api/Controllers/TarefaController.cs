using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tasks.application.Interfaces;
using tasks.domain.ViewModels;

namespace tasks.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TarefaController : BaseController<TarefaController>
    {
        private readonly ITarefaService tarefaService;
        public TarefaController(ITarefaService tarefaService)
        {
            this.tarefaService = tarefaService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ObterTodos()
        {
            IEnumerable<TarefaResponseViewModel> result = new List<TarefaResponseViewModel>();

            try
            {
                result = await tarefaService.ObterTodos();
            }
            catch (Exception ex)
            {                
                CreateServerErrorResponse(ex, null);
            }

            return CreateResponse(result);
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Adicionar([FromBody]TarefaRequestViewModel tarefa)
        {
            try
            {
                var result = await tarefaService.Adicionar(tarefa, GetUserId());
                
                if(!result)
                    return CreateValidationErrorResponse(tarefa.ValidationResult.Errors);
            }
            catch (Exception ex)
            {              
                return CreateServerErrorResponse(ex, null);
            }

            return NoContent();
        }

        [HttpPatch("")]
        [Authorize]
        public async Task<IActionResult> Fechar([FromBody]FecharTarefaRequestViewModel tarefa)
        {
            try
            {
                var result = await tarefaService.Fechar(tarefa);
                
                if(!result)
                    return CreateValidationErrorResponse(tarefa.ValidationResult.Errors);
            }
            catch (Exception ex)
            {                
                return CreateServerErrorResponse(ex, null);
            }

            return NoContent();
        }

        protected Guid GetUserId()
        {
            var aaa = User.Identity.Name;
            return Guid.Parse(this.User.Claims.First(i => i.Type == "Id").Value);
        }
    }
}
