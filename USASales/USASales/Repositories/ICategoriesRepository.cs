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
        bool IsIdValid(int id);
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

        public bool IsIdValid(int id)
        {
            const string sql = "SELECT COUNT(*) FROM Categories WHERE Id = @Id";
            return _connection.QuerySingleOrDefault<int>(sql, new { id }) != 0;
        }
    }
}
