using Domain.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Users
{
    public record UpdateUserDTO
    {
        [Required]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Deve ter no mínimo 5 caracteres")]
        [StringLength(150, ErrorMessage = "Máximo de {2} caracteres excedido")]
        public string Name { get; init; }
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        [Required]
        public string Phone { get; init; }
        [Required]
        public string Address { get; init; }
        [Required]
        [GenderValidation(ErrorMessage = "Selecione seu gênero")]
        public string Gender { get; init; }
    }
}
