using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ToDoTasks.Commands.DeleteToDoTaskCmd
{
    public class DeleteToDoTaskCmd : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string DeletedBy { get; set; }
    }

    /// <summary>
    /// Clase mediadora al hacer la solicitud del comando DeleteToDoTask desde la capa de Presentación
    /// </summary>
    public class DeleteToDoTaskCmdHandler : IRequestHandler<DeleteToDoTaskCmd, Response<int>>
    {
        private readonly IRepositoryAsync<ToDoTask> _repositoryAsync;

        public DeleteToDoTaskCmdHandler(IRepositoryAsync<ToDoTask> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteToDoTaskCmd request, CancellationToken cancellationToken)
        {
            var resultData = await _repositoryAsync.GetByIdAsync(request.Id);

            if(resultData == null)
            {
                throw new KeyNotFoundException($"Record not found with id {request.Id}.");
            }

            resultData.IsDeleted = true;
            resultData.DeletedBy = request.DeletedBy;

            await _repositoryAsync.UpdateAsync(resultData);

            return new Response<int>(resultData.Id);
        }
    }
}
