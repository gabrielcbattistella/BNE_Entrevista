using Domain.DTO.Product;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IProductService
    {
        Product Get(int id);
        IEnumerable<ProductDTO> GetAll();
        Product Post(CreateProductDTO createProductDTO);
        Product Update(UpdateProductDTO product, int id);
        bool Delete(int id);
    }
}
