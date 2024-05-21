using Microsoft.Data.SqlClient;

namespace mvccore.Abstraction
{
    public interface ISqlConnection
    {
        SqlConnection CreateConnection();
    }
}
