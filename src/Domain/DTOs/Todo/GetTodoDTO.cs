using System;
using src.Domain.Entities.Enum;

namespace src.Domain.DTOs.Todo
{
    public class GetTodoDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Priority Priority { get; set; }
    }
}