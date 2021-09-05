using Domain.DTO.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        User Get(int id);
        IEnumerable<UserDTO> GetAll();
        User Post(CreateUserDTO createUserDTO);
        User Update(UpdateUserDTO user, int id);
        bool Delete(int id);
    }
}
