using LibraryToyLand.Data.Lists;
using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiToyLand.Repository.Interfaces
{
    public interface IClientCartRepository
    {
        bool AddToCart(int idAccount, int idProduct);
        int GetCartCount(int idAccount);
        bool RemoveFromCart(int idAccount, int idProduct);
        List<Product> GetCartProducts(int idAccount);
    }
}
