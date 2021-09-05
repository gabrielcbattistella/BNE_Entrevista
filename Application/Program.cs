using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./myDb.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText = "CREATE TABLE 'users' ('id' INT NOT NULL AUTO_INCREMENT, 'name' VARCHAR(100), 'email' VARCHAR(100), 'phone' VARCHAR(50), 'address' VARCHAR(200), 'gender' CHAR(1), 'created_date' TIMESTAMP, PRIMARY KEY ('id') );";
                tableCommand.ExecuteNonQuery();
            }

                CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
