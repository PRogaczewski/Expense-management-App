using Infrastructure.EF;
using Infrastructure.EF.Database;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net;

namespace Application.Tests.ExpensesList.Tests.Query
{
    [TestFixture]
    public class ExpensesListServiceTestQuery
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly HttpClient _client;

        public ExpensesListServiceTestQuery()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.Test.json", true, true)
                .Build();

            var services = new ServiceCollection();

            services.InfrastructureRegistrationService(configuration);


            _serviceProvider = services.BuildServiceProvider();

            var factory = new WebApplicationFactory<Program>();
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        FakeAuthenticationPolicy.UserId = "2";
                        //FakeAuthenticationPolicy.UserId = "9";//not existing user
                        services.AddSingleton<IPolicyEvaluator, FakeAuthenticationPolicy>();

                        var dbContext = services
                        .SingleOrDefault(services => services.ServiceType == typeof(DbContextOptions<ExpenseDbContext>));

                        services.Remove(dbContext);

                        services.AddDbContext<ExpenseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ExpenseDbStringTests")));
                    });
                })
                .CreateClient();
        }

        [Test]
        public async Task GetAllExpensesList_NotExistingUserId_ReturnNotFound()
        {
            //Arrange
            //Act
            var response = await _client.GetAsync($"/Home");

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task GetAllExpensesList_ExistingUserId_ReturnOk()
        {
            //Arrange
            //Act
            var response = await _client.GetAsync($"/Home");

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test] //DatabseTransactionDispose
        [TestCase(4)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetExpensesList_NotExistingUserId_ReturnNotFound(int id)
        {
            //Arrange
            //Act
            var response = await _client.GetAsync($"/Home/{id}");

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test] //DatabseTransactionDispose
        [TestCase(1030)]
        public async Task GetExpensesList_ExistingUserId_ReturnOk(int id)
        {
            //Arrange
            //Act
            var response = await _client.GetAsync($"/Home/{id}");

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
