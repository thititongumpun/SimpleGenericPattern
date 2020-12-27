using AutoMapper;
using src.Domain.DTOs.Todo;

namespace src.Core.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, GetTodoDTO>().ReverseMap();
            CreateMap<InsertTodoDTO, Todo>();
            CreateMap<UpdateTodoDTO, Todo>();
        }
    }
}