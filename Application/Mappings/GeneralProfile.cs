using Application.Features.ToDoTasks.Commands.CreateToDoTaskCmd;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Commands

            CreateMap<CreateToDoTaskCmd, ToDoTask>();

            #endregion
        }
    }
}
