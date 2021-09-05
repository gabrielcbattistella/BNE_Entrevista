using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> users = new()
        {
            new User
            {
                Id = Guid.NewGuid(),
                Name = "NomeUsuario",
                Email = "emailuser@gmail.com",
                Phone = "998645901",
                Address = "Endereço usuario",
                Gender = "M",
                CreatedDate = DateTimeOffset.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name = "NomeUsuario2",
                Email = "emailuser2@gmail.com",
                Phone = "998645902",
                Address = "Endereço usuario2",
                Gender = "M",
                CreatedDate = DateTimeOffset.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name = "NomeUsuario3",
                Email = "emailuser3@gmail.com",
                Phone = "998645903",
                Address = "Endereço usuario3",
                Gender = "F",
                CreatedDate = DateTimeOffset.UtcNow
            }
        };

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public User GetUser(Guid id)
        {
            return users.Where(user => user.Id == id).SingleOrDefault();
        }

        public User CreateUser(User user)
        {
            users.Add(user);
            return user;
        }

        public User UpdateUser(User user)
        {
            var index = users.FindIndex(existingUser => existingUser.Id == user.Id);
            users[index] = user;
            return users[index];
        }
    }
}
