﻿using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
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

        public Task<IEnumerable<Product>> GetAll()
        {
            return _connection.QueryAsync<Product>("SELECT * FROM Products");
        }

        public Task<Product> Get(long id)
        {
            return _connection.QuerySingleAsync<Product>("SELECT * FROM Products WHERE Id = @id", new { id });
        }

        public Task Add(Product product)
        {
            const string sql =
                "INSERT INTO Products(Name, Category, WholesalePrice, GrossPrice) VALUES(@Name, @Category, @WholesalePrice, @GrossPrice)";
            return _connection.ExecuteAsync(sql, product);
        }
        public Task Delete(long id)
        {
            return _connection.ExecuteAsync("DELETE FROM Products WHERE Id = @id", new { id });
        }

        public Task Update(Product product)
        {
            const string sql = "Update Products SET Name=@Name, Category=@Category, WholesalePrice=@WholesalePrice, GrossPrice=@GrossPrice WHERE Id=@Id";
            return _connection.ExecuteAsync(sql, product);
        }
    }
}
