using Microsoft.Data.SqlClient;
using mvccore.Abstraction;


internal sealed class SqlConnectionService : ISqlConnection
{
        private readonly IConfiguration _configuration;

        public SqlConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(
                        _configuration.GetConnectionString("BookConnection"));
        }

}

