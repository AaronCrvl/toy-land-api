using LibraryToyLand.Data.Lists;
using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiToyLand.Repository.Interfaces
{
    public interface IProductRepository
    {
        Product GetProductById(int id);
        List<Product> GetAllProducts();
        Product_Stock GetProductStock(int registers);
        string GetProductName(int id);
    }
}
