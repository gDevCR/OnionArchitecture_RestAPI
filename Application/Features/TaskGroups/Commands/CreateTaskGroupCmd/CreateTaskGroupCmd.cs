using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Features.TaskGroups.Commands.CreateTaskGroupCmd
{
    public class CreateTaskGroupCmd : IRequest<Response<int>>
    {        
        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Clase mediadora al hacer la solicitud del comando CreateToDoTask desde la capa de Presentación
    /// </summary>
    public class CreateTaskGroupCmdHandler : IRequestHandler<CreateTaskGroupCmd, Response<int>>
    {
        private readonly IRepositoryAsync<TaskGroup> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateTaskGroupCmdHandler(IRepositoryAsync<TaskGroup> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTaskGroupCmd request, CancellationToken cancellationToken)
        {
            var newRecord = _mapper.Map<TaskGroup>(request);

            var resultData = await _repositoryAsync.AddAsync(newRecord);

            return new Response<int>(resultData.Id);
        }
    }
}
