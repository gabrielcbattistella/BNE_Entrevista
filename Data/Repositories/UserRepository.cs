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
    public class UserRepository : IUserRepository
    {
        SQLiteConnection conn = DataConnection.CreateConnection();
        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>{ };

            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = "SELECT * FROM Users";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                users.Add(new User
                {
                    Id = Convert.ToInt32(sqliteReader["id"]),
                    Name = sqliteReader["name"].ToString(),
                    Address = sqliteReader["address"].ToString(),
                    Email = sqliteReader["email"].ToString(),
                    Gender = sqliteReader["gender"].ToString(),
                    Phone = sqliteReader["phone"].ToString(),
                });
            };
            conn.Close();
            return users;
        }

        public User GetUser(int id)
        {
            User user = new User { };
            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"SELECT * FROM Users WHERE Id = {id};";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                user = user with
                {
                    Id = Convert.ToInt32(sqliteReader["id"]),
                    Name = sqliteReader["name"].ToString(),
                    Address = sqliteReader["address"].ToString(),
                    Email = sqliteReader["email"].ToString(),
                    Gender = sqliteReader["gender"].ToString(),
                    Phone = sqliteReader["phone"].ToString(),
                };
            };
            conn.Close();
            return user;
        }

        public User CreateUser(User user)
        {
            SQLiteDataReader sqliteReader;
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"SELECT * FROM Users WHERE email = '{user.Email}';";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                sqliteReader.Close();
                conn.Close();
                return null;
            };
            sqliteReader.Close();
            sqliteCommand.CommandText = $"INSERT INTO Users(name, email, phone, address, gender) VALUES ('{user.Name}', '{user.Email}', '{user.Phone}', '{user.Address}', '{user.Gender}') ;";
            if(sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return null;
            }

            sqliteCommand.CommandText = "SELECT last_insert_rowid() as id";
            sqliteReader = sqliteCommand.ExecuteReader();
            while (sqliteReader.Read())
            {
                user = user with
                {
                    Id = Convert.ToInt32(sqliteReader["id"])
                };
            };

            conn.Close();
            return user;
        }

        public User UpdateUser(User user)
        {
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"UPDATE Users SET name = '{user.Name}', email = '{user.Email}', phone = '{user.Phone}', address = '{user.Address}', gender = '{user.Gender}' WHERE id = '{user.Id}' ;";
            if (sqliteCommand.ExecuteNonQuery() == 0)
            {
                conn.Close();
                return null;
            }
            conn.Close();
            return user;
        }

        public bool DeleteUser(int id)
        {
            SQLiteCommand sqliteCommand;
            sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = $"DELETE FROM Users WHERE id = {id};";
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
