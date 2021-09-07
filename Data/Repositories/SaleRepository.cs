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
    public class SaleRepository : ISaleRepository
    {
        SQLiteConnection conn = DataConnection.CreateConnection();
        public IEnumerable<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale> { };

            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = "SELECT * FROM Sales";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                sales.Add(new Sale
                {
                    Id = Convert.ToInt32(sqliteReader["id"]),
                    ProductId = Convert.ToInt32(sqliteReader["id_product"]),
                    UserId = Convert.ToInt32(sqliteReader["id_user"]),
                    Quantity = Convert.ToInt32(sqliteReader["quantity"]),
                    Total = Convert.ToDecimal(sqliteReader["total"])
                });
            };
            conn.Close();
            return sales;
        }

        public Sale GetSale(int id)
        {
            Sale sale = new Sale { };
            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"SELECT * FROM Sales WHERE Id = {id};";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                sale = sale with
                {
                    Id = Convert.ToInt32(sqliteReader["id"]),
                    ProductId = Convert.ToInt32(sqliteReader["id_product"]),
                    UserId = Convert.ToInt32(sqliteReader["id_user"]),
                    Quantity = Convert.ToInt32(sqliteReader["quantity"]),
                    Total = Convert.ToDecimal(sqliteReader["total"])
                };
            };
            conn.Close();
            return sale;
        }

        public Sale CreateSale(Sale sale)
        {
            int? previousStock = 0;
            decimal? price = 0;
            decimal? total = 0;
            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"SELECT stock, price FROM Products WHERE id = {sale.ProductId}";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                previousStock = Convert.ToInt32(sqliteReader["stock"]);
                price = Convert.ToDecimal(sqliteReader["price"]);
            }

            if((previousStock - sale.Quantity) < 0)
            {
                conn.Close();
                return null;
            }

            total = price * sale.Quantity;
            int? newStock = previousStock - sale.Quantity;

            sqliteReader.Close();

            sqliteCommand.CommandText = $"UPDATE Products SET stock = {newStock} WHERE id = '{sale.ProductId}' ;";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return null;
            }

            sqliteCommand.CommandText = $"INSERT INTO Sales(id_product, id_user, quantity, total) VALUES ({sale.ProductId}, {sale.UserId}, {sale.Quantity}, '{total}');";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return null;
            }

            sqliteCommand.CommandText = "SELECT last_insert_rowid() as id";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                sale = sale with
                {
                    Id = Convert.ToInt32(sqliteReader["id"]),
                    Total = total
                };
            };

            conn.Close();
            return sale;
        }

        public Sale UpdateSale(Sale sale)
        {
            int prevProduct = 0;
            int prevStock = 0;
            int prevStockUpdate = 0;
            int prevUser = 0;
            int prevQuant = 0;
            int? newQuant = 0;
            int originalQuant = 0;
            decimal price = 0;
            SQLiteCommand sqliteCommand;
            SQLiteDataReader sqliteReader;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"SELECT id_product, id_user, quantity FROM Sales WHERE id = {sale.Id}";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                prevProduct = Convert.ToInt32(sqliteReader["id_product"]);
                prevUser = Convert.ToInt32(sqliteReader["id_user"]);
                prevQuant = Convert.ToInt32(sqliteReader["quantity"]);
            }

            if(prevProduct == 0 || prevUser == 0)
            {
                conn.Close();
                return null;
            }

            sqliteReader.Close();

            sqliteCommand.CommandText = $"SELECT stock FROM Products WHERE id = {prevProduct}";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                prevStock = Convert.ToInt32(sqliteReader["stock"]);
            }

            originalQuant = prevStock + prevQuant;

            sqliteReader.Close();

            sqliteCommand.CommandText = $"UPDATE Products SET stock = {originalQuant} WHERE id = '{prevProduct}' ;";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return null;
            }


            sqliteCommand.CommandText = $"SELECT stock, price FROM Products WHERE id = {sale.ProductId}";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                prevStockUpdate = Convert.ToInt32(sqliteReader["stock"]);
                price = Convert.ToDecimal(sqliteReader["price"]);
            }

            newQuant = prevStockUpdate - sale.Quantity;

            if(newQuant < 0)
            {
                conn.Close();
                return null;
            }

            sqliteReader.Close();

            sqliteCommand.CommandText = $"UPDATE Products SET stock = {newQuant} WHERE id = '{sale.ProductId}' ;";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return null;
            }

            decimal? total = sale.Quantity * price;

            sqliteCommand.CommandText = $"UPDATE Sales SET id_product = {sale.ProductId}, id_user = {sale.UserId}, quantity = {sale.Quantity}, total = '{total}' WHERE id = '{sale.Id}' ;";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return null;
            }

            conn.Close();

            sale = sale with
            {
                Total = total
            };

            return sale;
        }

        public bool DeleteSale(int id)
        {
            int prevQuan = 0;
            int prevProd = 0;
            int prevStock = 0;
            int newStock = 0;

            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();

            sqliteCommand.CommandText = $"SELECT quantity, id_product FROM Sales WHERE id = {id}";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                prevQuan = Convert.ToInt32(sqliteReader["quantity"]);
                prevProd = Convert.ToInt32(sqliteReader["id_product"]);
            }

            if(prevProd == 0 || prevProd == 0)
            {
                conn.Close();
                return false;
            }

            sqliteReader.Close();

            sqliteCommand.CommandText = $"SELECT stock FROM Products WHERE id = {prevProd}";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                prevStock = Convert.ToInt32(sqliteReader["stock"]);
            }

            newStock = prevStock + prevQuan;

            sqliteReader.Close();

            sqliteCommand.CommandText = $"UPDATE Products SET stock = {newStock} WHERE id = {prevProd};";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return false;
            }

            sqliteCommand.CommandText = $"DELETE FROM Sales WHERE id = {id};";
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
