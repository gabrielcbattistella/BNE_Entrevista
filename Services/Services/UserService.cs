using Domain.Interfaces.Services;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.DTO.Users;
using Domain;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            var user = _userRepository.GetUser(id);
            return user;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = _userRepository.GetUsers().Select(user => user.AsDTO());
            return users;
        }

        public User Post(CreateUserDTO createUserDTO)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                Name = createUserDTO.Name,
                Address = createUserDTO.Address,
                Email = createUserDTO.Email,
                Gender = createUserDTO.Gender,
                Phone = createUserDTO.Phone,
                CreatedDate = DateTimeOffset.UtcNow
            };

            User userCreated = _userRepository.CreateUser(user);

            return userCreated;
        }

        public User Update(User user)
        {
            User updatedUser = _userRepository.UpdateUser(user);
            return updatedUser;
        }
    }
}
