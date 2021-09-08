using Domain;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        SQLiteConnection conn = DataConnection.CreateConnection();
        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product> { };

            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = "SELECT * FROM Products";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                products.Add(new Product
                {
                    Id = Convert.ToInt32(sqliteReader["id"]),
                    Name = sqliteReader["name"].ToString(),
                    Price = Math.Truncate(Convert.ToDecimal(sqliteReader["price"])),
                    Stock = Convert.ToInt32(sqliteReader["stock"])
                });
            };
            conn.Close();
            return products;
        }

        public Product GetProduct(int id)
        {
            Product product = new Product { };
            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"SELECT * FROM Products WHERE Id = {id};";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                product = product with
                {
                    Id = Convert.ToInt32(sqliteReader["id"]),
                    Name = sqliteReader["name"].ToString(),
                    Price = Convert.ToDecimal(sqliteReader["price"]),
                    Stock = Convert.ToInt32(sqliteReader["stock"])
                };
            };
            conn.Close();
            return product;
        }

        public Product CreateProduct(Product product)
        {
            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"INSERT INTO Products(name, price, stock) VALUES ('{product.Name}', '{product.Price}', {product.Stock});";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return null;
            }

            sqliteCommand.CommandText = "SELECT last_insert_rowid() as id";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                product = product with
                {
                    Id = Convert.ToInt32(sqliteReader["id"])
                };
            };

            conn.Close();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"UPDATE Products SET name = '{product.Name}', price = '{product.Price}', stock = {product.Stock} WHERE id = '{product.Id}' ;";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return null;
            }
            conn.Close();
            return product;
        }

        public bool DeleteProduct(int id)
        {
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"DELETE FROM Products WHERE id = {id};";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }
    }
}
