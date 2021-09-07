using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Product
{
    public record ProductDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Por favor insira um valor acima de {1}")]
        public decimal? Price { get; init; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor insira um valor acima de {1}")]
        public int? Stock { get; init; }
    }
}
