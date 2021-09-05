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

        public bool Delete(int id)
        {
            var userDeleted = _userRepository.DeleteUser(id);
            return userDeleted;
        }

        public User Get(int id)
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
            User user = new User
            {
                Name = createUserDTO.Name,
                Address = createUserDTO.Address,
                Email = createUserDTO.Email,
                Gender = createUserDTO.Gender,
                Phone = createUserDTO.Phone,
            };

            User userCreated = _userRepository.CreateUser(user);

            return userCreated;
        }

        public User Update(UpdateUserDTO userDTO, int id)
        {
            User user = new User
            {
                Id = id,
                Name = userDTO.Name,
                Address = userDTO.Address,
                Email = userDTO.Email,
                Gender = userDTO.Gender,
                Phone = userDTO.Phone,
            };

            User updatedUser = _userRepository.UpdateUser(user);
            return updatedUser;
        }
    }
}
