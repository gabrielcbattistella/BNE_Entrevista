using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Product
{
    public record ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public int Stock { get; init; }
    }
}
