using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Product
{
    public record CreateProductDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public decimal Price { get; init; }
        [Required]
        public int Stock { get; init; }
    }
}
