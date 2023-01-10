using Microsoft.Extensions.Configuration;

namespace Infrastructure.Dapper.Database
{
    public class DataConnection
    {
        protected readonly string _connectionString;

        public DataConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ExpenseDbString");
        }
    }
}
