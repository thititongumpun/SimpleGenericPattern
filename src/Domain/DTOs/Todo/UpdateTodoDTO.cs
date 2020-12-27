using System;

namespace src.Domain.DTOs.Todo
{
    public class UpdateTodoDTO : InsertTodoDTO
    {
        public Guid Id { get; set; }
    }
}