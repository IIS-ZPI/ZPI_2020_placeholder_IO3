using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using USASales.Models;

namespace USASales.Repositories
{
    public interface ITaxesRepository
    {
        Task<IEnumerable<Tax>> GetAll();
        Task<IEnumerable<Tax>> GetMany(string state);
        Task<Tax> Get(long id);
        Task<Tax> Get(string state, string category);
    }

    public class TaxesRepository : ITaxesRepository
    {
        private readonly MySqlConnection _connection;

        public TaxesRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public Task<IEnumerable<Tax>> GetAll()
        {
            return _connection.QueryAsync<Tax>("SELECT * FROM Taxes");
        }

        public Task<IEnumerable<Tax>> GetMany(string state)
        {
            return _connection.QueryAsync<Tax>("SELECT * FROM Taxes WHERE LOWER(State) LIKE LOWER(@state)");
        }

        public Task<Tax> Get(long id)
        {
            return _connection.QuerySingleAsync<Tax>("SELECT * FROM Taxes WHERE Id = @id", new { id });
        }

        public Task<Tax> Get(string state, string category)
        {
            const string sql =
                "SELECT * FROM Taxes WHERE LOWER(State) LIKE LOWER(@state) AND LOWER(Category) LIKE LOWER(@category)";
            return _connection.QuerySingleAsync<Tax>(sql, new { state, category });
        }
    }
}