using Application.Features.ToDoTasks.Commands.CreateToDoTaskCmd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    /// <summary>
    /// El Controlador solo enruta la petición hacia el mediador que redirije hacia una validación y después hacia la persistencia
    /// </summary>
    [ApiVersion("1.0")]
    public class ToDoTaskController : BaseApiController
    {
        // POST
        [HttpPost]
        public async Task<IActionResult> createToDoTask(CreateToDoTaskCmd command) {
            return Ok(await Mediator.Send(command));
        }
    }
}
