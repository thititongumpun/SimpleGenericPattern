using src.Core.Repositories;
using src.Domain;
using src.Infrastructure.Persistence;

namespace src.Core
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoContext dbContext) : base(dbContext) { }
    }
}