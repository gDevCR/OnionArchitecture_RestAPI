using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ToDoTasks.Commands.CreateToDoTaskCmd
{
    public class CreateToDoTaskCmd : IRequest<Response<int>>
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }

    /// <summary>
    /// Clase mediadora al hacer la solicitud del comando CreateToDoTask desde la capa de Presentación
    /// </summary>
    public class CreateToDoTaskCmdHandler : IRequestHandler<CreateToDoTaskCmd, Response<int>>
    {
        private readonly IRepositoryAsync<ToDoTask> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateToDoTaskCmdHandler(IRepositoryAsync<ToDoTask> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateToDoTaskCmd request, CancellationToken cancellationToken)
        {
            var newRecord = _mapper.Map<ToDoTask>(request);

            var resultData = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(resultData.Id);
        }
    }
}
