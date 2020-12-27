using AutoMapper;
using src.Domain.DTOs.Todo;

namespace src.Core.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, GetTodoDTO>().ReverseMap();
            CreateMap<InsertTodoDTO, TodoItem>();
            CreateMap<UpdateTodoDTO, TodoItem>();
        }
    }
}