using Application.Dto.Models.ExpensesList;
using Application.Tests.Attributes;
using Infrastructure.EF.Database;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Text;

namespace Application.Tests.ExpensesList.Tests.Command
{
    [TestFixture]
    public class ExpensesListServiceTest
    {
        private readonly HttpClient _client;

        public class TestCaseData
        {
            public UserExpensesListModel Model { get; set; }

            public int Id { get; set; }
        }

        public ExpensesListServiceTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        //UserId 7
                        FakeAuthenticationPolicy.UserId = "2";
                        services.AddSingleton<IPolicyEvaluator, FakeAuthenticationPolicy>();

                        var dbContext = services
                        .SingleOrDefault(services => services.ServiceType == typeof(DbContextOptions<ExpenseDbContext>));

                        services.Remove(dbContext);

                        services.AddDbContext<ExpenseDbContext>(options => options.UseInMemoryDatabase("ExpenseDbTest"));
                    });
                })
                .CreateClient();
        }


        [Test]
        [TestCaseSource(nameof(CreateExpensesList_NoSeeder_ReturnOk_TestCases))]
        public async Task CreateExpensesList_NoSeeder_ReturnOk(UserExpensesListModel model)
        {
            //Arrange
            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            //Act
            var response = await _client.PostAsync("/Home", httpContent);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCaseSource(nameof(CreateExpensesList_Seeder_ReturnOk_TestCases))]
        public async Task CreateExpensesList_Seeder_ReturnOk(UserExpensesListModel model)
        {
            //Arrange
            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            //Act
            var response = await _client.PostAsync("/Home", httpContent);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCaseSource(nameof(UpdateExpensesList_ExistingList_ReturnOk_TestCases))]
        public async Task UpdateExpensesList_ExistingList_ReturnOk(TestCaseData testModel)
        {
            //Arrange
            var json = JsonConvert.SerializeObject(testModel.Model);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            //Act
            var response = await _client.PutAsync($"/Home/{testModel.Id}", httpContent);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


        private static IEnumerable<UserExpensesListModel> CreateExpensesList_NoSeeder_ReturnOk_TestCases()
        {
            yield return new UserExpensesListModel() { Name = "Test 216" };
            yield return new UserExpensesListModel() { Name = "Test nowy7" };
            yield return new UserExpensesListModel() { Name = "223456789abcde7" };
            yield return new UserExpensesListModel() { Name = "Nowy27" };
            yield return new UserExpensesListModel() { Name = "se2edertest7" };
        }


        private static IEnumerable<UserExpensesListModel> CreateExpensesList_Seeder_ReturnOk_TestCases()
        {
            yield return new UserExpensesListModel() { Name = "2SeedER1" };
            yield return new UserExpensesListModel() { Name = "Seedertestcase1" };
            yield return new UserExpensesListModel() { Name = "SEEdER test 6" };
            yield return new UserExpensesListModel() { Name = "Name seeder 1" };
        }

        private static IEnumerable<TestCaseData> UpdateExpensesList_ExistingList_ReturnOk_TestCases()
        {
            yield return new TestCaseData() { Model = new UserExpensesListModel() { Name = "test01"}, Id = 1030 };
            yield return new TestCaseData() { Model = new UserExpensesListModel() { Name = "test078"}, Id = 1031 };
            yield return new TestCaseData() { Model = new UserExpensesListModel() { Name = "aaaaaa90"}, Id = 1031 };
            yield return new TestCaseData() { Model = new UserExpensesListModel() { Name = "mynewlist"}, Id = 1032 };
            yield return new TestCaseData() { Model = new UserExpensesListModel() { Name = "mytestlist90"}, Id = 1033 };
        }
    }
}
