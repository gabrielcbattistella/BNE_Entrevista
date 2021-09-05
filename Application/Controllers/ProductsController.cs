using Domain;
using Domain.DTO.Product;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<ProductDTO> GetProducts()
        {
            var products = _productService.GetAll();
            return products;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            var product = _productService.Get(id);

            if (product is null)
            {
                return NotFound();
            }
            return product.AsDTO();
        }

        [HttpPost]
        public ActionResult<ProductDTO> CreateProduct(CreateProductDTO product)
        {
            var createdProduct = _productService.Post(product);

            if (createdProduct is null)
                return NotFound();

            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct.AsDTO());
        }


        [HttpPut("{id}")]
        public ActionResult<ProductDTO> UpdateProduct(UpdateProductDTO product, int id)
        {
            var updatedProduct = _productService.Update(product, id);

            if (updatedProduct is null)
                return NotFound();

            return Ok(updatedProduct.AsDTO());
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteProduct(int id)
        {
            bool product = _productService.Delete(id);

            if (product is false)
            {
                return NotFound();
            }
            return product;
        }
    }
}
