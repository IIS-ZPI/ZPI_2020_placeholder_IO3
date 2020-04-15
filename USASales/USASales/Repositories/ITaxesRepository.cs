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
        Task<Tax> Get(long id);
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

        public Task<Tax> Get(long id)
        {
            return _connection.QuerySingleAsync<Tax>("SELECT * FROM Taxes WHERE Id = @id", new { id });
        }
    }
}