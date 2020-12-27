using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using src;
using src.Core;
using src.Infrastructure.Persistence;
using Xunit;

namespace Todo.UnitTests
{
    public class TodoRepositoryTests
    {
        private TodoContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(name)
            .Options;
            return new TodoContext(options);
        }

        [Fact]
        public async Task Create_Todo()
        {
            int result;
            var todo = new TodoItem()
            {
                Description = "For Test",
                Author = "Thiti Tongumpun",
                Priority = 0,
                Status = true
            };

            using (var context = CreateDbContext("Create_Todo"))
            {
                var repository = new TodoRepository(context);
                repository.Create(todo);
                result = await repository.SaveChangesAsync();
            }

            result.Should().BeGreaterThan(0);
            result.Should().Be(1);
            using (var context = CreateDbContext("Create_Todo"))
            {
                (await context.Todos.CountAsync()).Should().Be(1);
                (await context.Todos.FirstAsync()).Should().Be(todo);
            }
        }
    }
}