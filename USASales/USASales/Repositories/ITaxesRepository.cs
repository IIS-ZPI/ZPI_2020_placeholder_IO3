using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Tax>> GetAll()
        {
            var taxes = (await _connection.QueryAsync<Tax>("SELECT * FROM Taxes")).ToList();
            foreach (var tax in taxes)
            {
                const string sql = "SELECT Name FROM Categories WHERE Id = @Id";
                tax.Category = await _connection.QuerySingleAsync<string>(sql, new { id = tax.CategoryId });
            }

            return taxes;
        }

        public async Task<IEnumerable<Tax>> GetMany(string state)
        {
            const string sql = "SELECT * FROM Taxes WHERE LOWER(State) LIKE LOWER(@state)";
            var taxes = (await _connection.QueryAsync<Tax>(sql, new { state })).ToList();

            foreach (var tax in taxes)
            {
                const string sql2 = "SELECT Name FROM Categories WHERE Id = @Id";
                tax.Category = await _connection.QuerySingleAsync<string>(sql2, new { id = tax.CategoryId });
            }

            return taxes;
        }

        public async Task<Tax> Get(long id)
        {
            const string sql = "SELECT * FROM Taxes WHERE Id = @id";
            var tax = await _connection.QuerySingleAsync<Tax>(sql, new { id });

            const string sql2 = "SELECT Name FROM Categories WHERE Id = @Id";
            tax.CategoryId = await _connection.QuerySingleAsync<string>(sql2, new { id = tax.CategoryId });

            return tax;
        }

        public async Task<Tax> Get(string state, string category)
        {
            const string sql =
                "SELECT * FROM Taxes JOIN Categories ON Taxes.CategoryId = Categories.id WHERE LOWER(State) LIKE LOWER(@state) AND LOWER(Name) LIKE LOWER(@category)";
            var tax = await _connection.QuerySingleAsync<Tax>(sql, new { state, category });

            const string sql2 = "SELECT Name FROM Categories WHERE Id = @Id";
            tax.CategoryId = await _connection.QuerySingleAsync<string>(sql2, new { id = tax.CategoryId });

            return tax;
        }
    }
}