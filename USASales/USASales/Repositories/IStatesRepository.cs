using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using USASales.Models;

namespace USASales.Repositories
{
    public interface IStatesRepository
    {
        Task<IEnumerable<State>> GetAll();
    }

    public class StatesRepository : IStatesRepository
    {
        private readonly MySqlConnection _connection;

        public StatesRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public Task<IEnumerable<State>> GetAll()
        {
            return _connection.QueryAsync<State>("SELECT * FROM States");
        }
    }
}
