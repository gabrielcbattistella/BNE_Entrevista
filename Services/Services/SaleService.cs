using Domain;
using Domain.DTO.Sale;
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
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public bool Delete(int id)
        {
            var saleDeleted = _saleRepository.DeleteSale(id);
            return saleDeleted;
        }

        public Sale Get(int id)
        {
            var sale = _saleRepository.GetSale(id);
            return sale;
        }

        public IEnumerable<SaleDTO> GetAll()
        {
            var sales = _saleRepository.GetSales().Select(sale => sale.AsDTO());
            return sales;
        }

        public Sale Post(CreateSaleDTO createSaleDTO)
        {
            Sale sale= new()
            {
                ProductId = createSaleDTO.ProductId,
                UserId = createSaleDTO.UserId,
                Quantity = createSaleDTO.Quantity
            };

            Sale saleCreated = _saleRepository.CreateSale(sale);

            return saleCreated;
        }

        public Sale Update(UpdateSaleDTO saleDTO, int id)
        {
            Sale sale = new Sale
            {
                Id = id,
                ProductId = saleDTO.ProductId,
                UserId = saleDTO.UserId,
                Quantity = saleDTO.Quantity
            };
            Sale updatedSale = _saleRepository.UpdateSale(sale);
            return updatedSale;
        }
    }
}
