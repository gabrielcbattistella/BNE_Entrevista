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
        User Get(Guid id);
        IEnumerable<UserDTO> GetAll();
        User Post(CreateUserDTO createUserDTO);
        User Update(User user);
        Task<bool> Delete(Guid id);
    }
}
