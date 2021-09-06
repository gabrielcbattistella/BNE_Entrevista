using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ISaleRepository
    {
        Sale GetSale(int id);
        IEnumerable<Sale> GetSales();
        Sale CreateSale(Sale sale);
        Sale UpdateSale(Sale sale);
        bool DeleteSale(int id);
    }
}
