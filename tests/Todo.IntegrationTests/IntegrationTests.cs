using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using src;
using src.Infrastructure.Persistence;

namespace Todo.IntegrationTests
{
    public class IntegrationTests
    {
        protected readonly HttpClient _client;

        public IntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(services => 
                {
                    services.RemoveAll(typeof(TodoContext));
                    services.AddDbContext<TodoContext>(options => 
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                });
            });

        _client = appFactory.CreateClient();
        }
    }
}