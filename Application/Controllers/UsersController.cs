using Data.Repositories;
using Domain;
using Domain.DTO.Users;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<UserDTO> GetUsers()
        {
            var users =  _userService.GetAll();
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUser(int id)
        {
            var user = _userService.Get(id);

            if(user is null)
            {
                return NotFound();
            }
            return user.AsDTO();
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateUser(CreateUserDTO user)
        {
            var createdUser = _userService.Post(user);

            if (createdUser is null)
                return NotFound();

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser.AsDTO());
        }


        [HttpPut("{id}")]
        public ActionResult<UserDTO> UpdateUser(UpdateUserDTO user, int id)
        {
            var updatedUser = _userService.Update(user, id);

            if (updatedUser is null)
                return NotFound();

            return Ok(updatedUser.AsDTO());
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteUser(int id)
        {
            bool user = _userService.Delete(id);

            if (user is false)
            {
                return NotFound();
            }
            return user;
        }
    }
}
