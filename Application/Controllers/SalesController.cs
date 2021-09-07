using Domain;
using Domain.DTO.Sale;
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
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public IEnumerable<SaleDTO> GetSales()
        {
            var sales = _saleService.GetAll();
            return sales;
        }

        [HttpGet("{id}")]
        public ActionResult<SaleDTO> GetSale(int id)
        {
            var sale = _saleService.Get(id);

            if (sale is null)
            {
                return NotFound();
            }
            return sale.AsDTO();
        }

        [HttpPost]
        public ActionResult<SaleDTO> CreateSale(CreateSaleDTO sale)
        {
            var createdSale = _saleService.Post(sale);

            if (createdSale is null)
                return NotFound();

            return CreatedAtAction(nameof(GetSale), new { id = createdSale.Id }, createdSale.AsDTO());
        }


        [HttpPut("{id}")]
        public ActionResult<SaleDTO> UpdateSale(UpdateSaleDTO sale, int id)
        {
            var updatedSale = _saleService.Update(sale, id);

            if (updatedSale is null)
                return NotFound();

            return Ok(updatedSale.AsDTO());
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteSale(int id)
        {
            bool sale = _saleService.Delete(id);

            return sale;
        }
    }
}
