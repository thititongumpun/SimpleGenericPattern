using System;
using System.Collections.Generic;
using System.Linq;
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


        [Fact]
        public async Task Update_Todo()
        {
            int result;
            Guid? id;
            using (var context = CreateDbContext("Update_Todo"))
            {
                var createdTodo = new TodoItem()
                {
                    Description = "For Test",
                    Author = "Thiti Tongumpun",
                    Priority = 0,
                    Status = true
                };
                context.Set<TodoItem>().Add(createdTodo);
                context.Set<TodoItem>().Add(new TodoItem() { Description = "xunit test", Status = false});
                await context.SaveChangesAsync();
                id = createdTodo.Id;
            }

            TodoItem updateTodo;
            using (var context = CreateDbContext("Update_Todo"))
            {
                updateTodo = await context.Set<TodoItem>().FirstOrDefaultAsync(x => x.Id == id);
                updateTodo.Description = "Update test";
                var repository = new TodoRepository(context);
                repository.Update(updateTodo);
                result = await repository.SaveChangesAsync();
            }

            result.Should().BeGreaterThan(0);
            result.Should().Be(1);
            using (var context = CreateDbContext("Update_Todo"))
            {
                (await context.Todos.FirstAsync(x => x.Id == updateTodo.Id)).Should().Be(updateTodo);
            }
        }

        [Fact]
        public async Task Delete_Todo()
        {
            int result;
            Guid? id;
            using (var context = CreateDbContext("Delete_Todo"))
            {
                var createdTodo = new TodoItem()
                {
                    Description = "For Test",
                    Author = "Thiti Tongumpun",
                    Priority = 0,
                    Status = true
                };
                context.Set<TodoItem>().Add(createdTodo);
                context.Set<TodoItem>().Add(new TodoItem() { Description = "xunit test", Status = false});
                await context.SaveChangesAsync();
                id = createdTodo.Id;
            }
            
            using (var context = CreateDbContext("Delete_Todo"))
            {
                var repository = new TodoRepository(context);
                await repository.Delete(id.Value);
                result = await repository.SaveChangesAsync();
            }

            result.Should().BeGreaterThan(0);
            result.Should().Be(1);
            using (var context = CreateDbContext("Delete_Todo"))
            {
                (await context.Set<TodoItem>().FirstOrDefaultAsync(x => x.Id == id)).Should().BeNull();
                (await context.Set<TodoItem>().ToListAsync()).Should().NotBeEmpty();
            }
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public async Task GetAll_todos(int count)
        {
            using (var context = CreateDbContext($"GetAll_with_todos_{count}"))
            {
                for (var i = 0; i < count; i++)
                {
                    context.Set<TodoItem>().Add(new TodoItem());
                }
                await context.SaveChangesAsync();
            }
            List<TodoItem> todos = null;

            using (var context = CreateDbContext($"GetAll_with_todos_{count}"))
            {
                var repository = new TodoRepository(context);
                todos = await repository.GetAll().ToListAsync();
            }

            todos.Should().NotBeNull();
            todos.Count().Should().Be(count);
        }


        [Theory]
        [InlineData("4e1a20db-0533-4838-bd97-87d2afc89832")]
        [InlineData("ff57101b-d9c6-4b8a-959e-2d64c7ae8967")]
        [InlineData("2c0176d6-47d6-4ce1-b5e8-bed9a52b9e64")]
        [InlineData("bf15a502-37db-4d4c-ba4c-e231fb5487e6")]
        [InlineData("e141a755-98d4-44d3-a84f-528e6e75f543")]
        public async Task GetById_existing_todos(Guid id)
        {

            using (var context = CreateDbContext("GetById_todos"))
            {
                context.Set<TodoItem>().Add(new TodoItem { Id = id });
                await context.SaveChangesAsync();
            }
            TodoItem todo = null;

            using (var context = CreateDbContext("GetById_todos"))
            {
                var repository = new TodoRepository(context);
                todo = await repository.GetById(id);
            }

            todo.Should().NotBeNull();
            todo.Id.Should().Be(id);
        }

        [Theory]
        [InlineData("4e1a20db-0533-4838-bd97-87d2afc89832")]
        [InlineData("ff57101b-d9c6-4b8a-959e-2d64c7ae8967")]
        [InlineData("2c0176d6-47d6-4ce1-b5e8-bed9a52b9e64")]
        [InlineData("bf15a502-37db-4d4c-ba4c-e231fb5487e6")]
        [InlineData("e141a755-98d4-44d3-a84f-528e6e75f543")]
        public async Task GetById_inexistent_todos(Guid id)
        {
            using (var context = CreateDbContext("GetById_notexist_todos"))
            {
                context.Set<TodoItem>().Add(new TodoItem { Id = id });
                await context.SaveChangesAsync();
            }
            TodoItem todo = null;


            using (var context = CreateDbContext("GetById_notexist_todos"))
            {
                var repository = new TodoRepository(context);
                todo = await repository.GetById(new Guid());
            }

            todo.Should().BeNull();
        }
    }
}