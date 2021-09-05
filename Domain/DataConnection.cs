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
            string createUserSQL = "CREATE TABLE IF NOT EXISTS Users(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, email TEXT NOT NULL UNIQUE, phone TEXT NOT NULL, address TEXT NOT NULL, gender TEXT NOT NULL);";
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = createUserSQL;
            sqliteCommand.ExecuteNonQuery();
            string createProductSQL = "CREATE TABLE IF NOT EXISTS Products(id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, price DECIMAL NOT NULL, stock INT NOT NULL);";
            sqliteCommand.CommandText = createProductSQL;
            sqliteCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}
