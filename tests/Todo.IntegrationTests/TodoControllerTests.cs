using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using src;
using Xunit;

namespace Todo.IntegrationTests
{
    public class TodoControllerTests : IntegrationTests
    {
        [Fact]
        public async Task GetAll_WithoutAnyTodo_ReturnsEmptyResponse()
        {
            var result = await _client.GetAsync("/api/Todo");

            
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            (await result.Content.ReadAsAsync<IEnumerable<TodoItem>>()).Should().BeEmpty();
        }
    }
}