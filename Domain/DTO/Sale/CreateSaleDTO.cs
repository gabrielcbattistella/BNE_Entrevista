using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Sale
{
    public record CreateSaleDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "No mínimo 1 unidade do produto deve ser comprado")]
        public int Quantity { get; set; }
    }
}
