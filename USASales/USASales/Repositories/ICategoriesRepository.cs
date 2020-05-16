using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using USASales.Models;

namespace USASales.Repositories
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetCategoriesNames();
    }

    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly MySqlConnection _connection;

        public CategoriesRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public Task<IEnumerable<Category>> GetCategoriesNames()
        {
            return _connection.QueryAsync<Category>("SELECT * FROM Categories");
        }
    }
}
