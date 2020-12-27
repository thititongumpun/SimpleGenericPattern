using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Core.Interfaces.Todo;
using src.Domain;
using src.Domain.DTOs.Todo;

namespace src.Core.Services
{
    public class TodoService : ITodoService
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;
        public TodoService(IMapper mapper, ITodoRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<GetTodoDTO> CreateTodo(InsertTodoDTO todo)
        {
            var todoCreate = _repository.Create(_mapper.Map<Todo>(todo));
            await _repository.SaveChangesAsync();
            return _mapper.Map<GetTodoDTO>(todoCreate);
        }

        public async Task<bool> DeleteTodo(Guid id)
        {
            await _repository.Delete(id);
            return await _repository.SaveChangesAsync() > 0;
        }

        public async Task<List<GetTodoDTO>> GetAllTodos()
        {
            var todos = _repository.GetAll();
            return await _mapper.ProjectTo<GetTodoDTO>(todos).ToListAsync();
        }

        public async Task<GetTodoDTO> GetTodoById(Guid id)
        {
            return _mapper.Map<GetTodoDTO>(await _repository.GetById(id));
        }

        public async Task<GetTodoDTO> UpdateTodo(UpdateTodoDTO todo)
        {
            var todoUpdate = _repository.Update(_mapper.Map<Todo>(todo));
            await _repository.SaveChangesAsync();
            return _mapper.Map<GetTodoDTO>(todoUpdate);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
        }
    }
}