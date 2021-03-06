using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Users
{
    public record UserDTO
    {
        [Required]
        public int Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public string Phone { get; init; }
        [Required]
        public string Address { get; init; }
        [Required]
        public string Gender { get; init; }
    }
}
