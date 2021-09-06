using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DataConnection
    {
        public static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqliteConn;
            sqliteConn = new SQLiteConnection("Data Source=../Data/database.db; Version = 3; New = True; Compress = True;");
            try
            {
                sqliteConn.Open();
            }
            catch
            {

            }
            return sqliteConn;
        }

        public static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand sqliteCommand;
            string createUsersSQL = "CREATE TABLE IF NOT EXISTS Users(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, email TEXT NOT NULL UNIQUE, phone TEXT NOT NULL, address TEXT NOT NULL, gender TEXT NOT NULL);";
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = createUsersSQL;
            sqliteCommand.ExecuteNonQuery();

            string createProductsSQL = "CREATE TABLE IF NOT EXISTS Products(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, price DECIMAL NOT NULL, stock INT NOT NULL);";
            sqliteCommand.CommandText = createProductsSQL;
            sqliteCommand.ExecuteNonQuery();

            string createSalesSQL = "CREATE TABLE IF NOT EXISTS Sales(id INTEGER PRIMARY KEY AUTOINCREMENT, id_product INTEGER REFERENCES Products(id), id_user INTEGER REFERENCES Users(id), quantity INTEGER NOT NULL, total DECIMAL NOT NULL);";
            sqliteCommand.CommandText = createSalesSQL;
            sqliteCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}
