using Domain.DTO.Sale;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ISaleService
    {
        Sale Get(int id);
        IEnumerable<SaleDTO> GetAll();
        Sale Post(CreateSaleDTO createSaleDTO);
        Sale Update(UpdateSaleDTO sale, int id);
        bool Delete(int id);
    }
}
