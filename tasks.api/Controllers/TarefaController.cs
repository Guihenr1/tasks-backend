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

        [HttpGet("ObterTodas/{dataConclusao}")]
        [Authorize]
        public IActionResult ObterTodos(DateTime dataConclusao)
        {
            IEnumerable<TarefaResponseViewModel> result = new List<TarefaResponseViewModel>();

            try
            {
                result = tarefaService.ObterTodos(GetUserId(), dataConclusao);
            }
            catch (Exception ex)
            {                
                CreateServerErrorResponse(ex, null);
            }

            return CreateResponse(result);
        }

        [HttpPost]
        [Authorize]
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

        [HttpGet("{idTarefa}")]
        [Authorize]
        public async Task<IActionResult> Alternar(Guid idTarefa)
        {
            try
            {
                var result = await tarefaService.Alternar(idTarefa);
            }
            catch (Exception ex)
            {                
                return CreateServerErrorResponse(ex, null);
            }

            return NoContent();
        }

        protected Guid GetUserId()
        {
            return Guid.Parse(this.User.Identity.Name);
        }
    }
}
