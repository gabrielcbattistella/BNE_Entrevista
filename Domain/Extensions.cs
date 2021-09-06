using Domain.DTO.Product;
using Domain.DTO.Sale;
using Domain.DTO.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Extensions
    {
        public static UserDTO AsDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Gender = user.Gender,
            };
        }
        public static ProductDTO AsDTO(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
            };
        }

        public static SaleDTO AsDTO(this Sale sale)
        {
            return new SaleDTO
            {
                Id = sale.Id,
                ProductId = sale.ProductId,
                UserId = sale.UserId,
                Quantity = sale.Quantity,
                Total = sale.Total
            };
        }
    }
}
