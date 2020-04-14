using System;
using MySql.Data.MySqlClient;
using USASales.Repositories;

namespace USASales
{
    public class DatabaseContext : IDisposable
    {
        private readonly MySqlConnection _connection;

        public DatabaseContext(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        public IProductsRepository ProductsRepository => new ProductsRepository(_connection);

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
