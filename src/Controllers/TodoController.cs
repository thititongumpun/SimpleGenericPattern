using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Core.Interfaces.Todo;
using src.Domain.DTOs.Todo;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
        }

        [HttpGet]
        public async Task<ActionResult<List<GetTodoDTO>>> GetTodos()
        {
            return Ok(await _todoService.GetAllTodos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTodoDTO>> GetTodoById(Guid id)
        {
            var todo = await _todoService.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<GetTodoDTO>> Create([FromBody] InsertTodoDTO model)
        {
            return Ok(await _todoService.CreateTodo(model));
        }

        [HttpPut]
        public async Task<ActionResult<GetTodoDTO>> Update([FromBody] UpdateTodoDTO model)
        {
            return Ok(await _todoService.UpdateTodo(model));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var delete = await _todoService.DeleteTodo(id);
            if (delete)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}