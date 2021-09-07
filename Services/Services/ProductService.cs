using Domain;
using Domain.DTO.Product;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool Delete(int id)
        {
            bool productDeleted = _productRepository.DeleteProduct(id);
            return productDeleted;
        }

        public Product Get(int id)
        {
            var product = _productRepository.GetProduct(id);
            return product;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var products = _productRepository.GetProducts().Select(product => product.AsDTO());
            return products;
        }

        public Product Post(CreateProductDTO createProductDTO)
        {
            Product product = new()
            {
                Name = createProductDTO.Name,
                Price = createProductDTO.Price,
                Stock = createProductDTO.Stock
            };

            Product productCreated = _productRepository.CreateProduct(product);

            return productCreated;
        }

        public Product Update(UpdateProductDTO productDTO, int id)
        {
            Product product = new Product
            {
                Id = id,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Stock = productDTO.Stock
            };
            Product updatedProduct = _productRepository.UpdateProduct(product);
            return updatedProduct;
        }
    }
}
