using Application.Exceptions;
using Application.Features.ToDoTasks.Commands.CreateToDoTaskCmd;
using Application.Features.ToDoTasks.Commands.DeleteToDoTaskCmd;
using Application.Features.ToDoTasks.Commands.UpdateToDoTaskCmd;
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
        // POST api/<version>/<controller>/2
        [HttpPost]
        public async Task<IActionResult> createToDoTask(CreateToDoTaskCmd command) {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<version>/<controller>/2
        [HttpPut("{id}")]
        public async Task<IActionResult> updateToDoTask(int id,[FromBody] UpdateToDoTaskCmd command)
        {
            if(id != command.Id)
            {
                 throw new ApiException("The param Id key is't the same with the model Id.");
            }

            return Ok(await Mediator.Send(command));
        }

        // PUT api/<version>/<controller>/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> removeToDoTask(int id)
        {
            if (id <= 0)
            {
                throw new ApiException("The param Id key is required.");
            }

            return Ok(await Mediator.Send(new DeleteToDoTaskCmd() { Id= id, DeletedBy = "user delete"}));
        }
    }
}
