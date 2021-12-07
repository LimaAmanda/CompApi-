using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CompAPI.DAL
{
    public class ConnectionFactory
    {
        public static string nomeConexao = "ConexaoSomee";

        public static IDbConnection GetStringConexao(IConfiguration config)
        {
            return new SqlConnection(config.GetConnectionString(nomeConexao));
        }
    }
}