using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using USASales.Models;

namespace USASales.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(long id);
        Task Add(Product product);
        Task Delete(long id);
        Task Update(Product product);
    }

    public class ProductsRepository : IProductsRepository
    {
        private readonly MySqlConnection _connection;

        public ProductsRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = (await _connection.QueryAsync<Product>("SELECT * FROM Products")).ToList();
            foreach (var product in products)
            {
                product.Category = await _connection.QuerySingleAsync<string>("SELECT Name FROM Categories WHERE Id = @Id",
                    new { id = product.CategoryId });
            }

            return products;
        }

        public async Task<Product> Get(long id)
        {
            var product = await _connection.QuerySingleAsync<Product>("SELECT * FROM Products WHERE Id = @id", new { id });
            product.Category = await _connection.QuerySingleAsync<string>("SELECT Name FROM Categories WHERE Id = @Id",
                new { id = product.CategoryId });
            return product;
        }

        public Task Add(Product product)
        {
            const string sql =
                "INSERT INTO Products(Name, CategoryId, WholesalePrice, GrossPrice) VALUES(@Name, @CategoryId, @WholesalePrice, @GrossPrice)";
            return _connection.ExecuteAsync(sql, product);
        }
        public Task Delete(long id)
        {
            return _connection.ExecuteAsync("DELETE FROM Products WHERE Id = @id", new { id });
        }

        public Task Update(Product product)
        {
            const string sql = "Update Products SET Name=@Name, CategoryId=@CategoryId, WholesalePrice=@WholesalePrice, GrossPrice=@GrossPrice WHERE Id=@Id";
            return _connection.ExecuteAsync(sql, product);
        }
    }
}
