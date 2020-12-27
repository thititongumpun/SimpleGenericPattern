using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.Domain.DTOs.Todo;

namespace src.Core.Interfaces.Todo
{
    public interface ITodoService
    {
        public Task<List<GetTodoDTO>> GetAllTodos();

        public Task<GetTodoDTO> GetTodoById(Guid id);

        public Task<GetTodoDTO> CreateTodo(InsertTodoDTO todo);

        public Task<GetTodoDTO> UpdateTodo(UpdateTodoDTO todo);

        public Task<bool> DeleteTodo(Guid id);

    }
}