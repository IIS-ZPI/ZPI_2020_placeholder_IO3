using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace USASales.Repositories
{
    public interface IProductsRepository
    {
        Task<string> Get();
    }

    public class ProductsRepository : IProductsRepository
    {
        private readonly MySqlConnection _connection;

        public ProductsRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> Get()
        {
            return JsonConvert.SerializeObject(await _connection.QueryAsync("SELECT * FROM test"));
        }
    }
}
