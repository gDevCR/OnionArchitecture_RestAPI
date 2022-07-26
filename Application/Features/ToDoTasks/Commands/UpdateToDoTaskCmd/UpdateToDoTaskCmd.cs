using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ToDoTasks.Commands.UpdateToDoTaskCmd
{
    public class UpdateToDoTaskCmd : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }

    /// <summary>
    /// Clase mediadora al hacer la solicitud del comando UpdateToDoTask desde la capa de Presentación
    /// </summary>
    public class UpdateToDoTaskCmdHandler : IRequestHandler<UpdateToDoTaskCmd, Response<int>>
    {
        private readonly IRepositoryAsync<ToDoTask> _repositoryAsync;
        private readonly IMapper _mapper;

        public UpdateToDoTaskCmdHandler(IRepositoryAsync<ToDoTask> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateToDoTaskCmd request, CancellationToken cancellationToken)
        {
            var resultData = await _repositoryAsync.GetByIdAsync(request.Id);

            if(resultData == null)
            {
                throw new KeyNotFoundException($"Record not found with id {request.Id}.");
            }

            resultData.GroupId = request.GroupId;
            resultData.Name = request.Name;
            resultData.Description = request.Description;
            resultData.IsComplete = request.IsComplete;

            await _repositoryAsync.UpdateAsync(resultData);

            return new Response<int>(resultData.Id);
        }
    }
}
