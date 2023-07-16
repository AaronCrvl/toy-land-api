using ApiToyLand.Models;
using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiToyLand.Repository.Interfaces
{
    public interface IClientOrderRepository
    {        
        int CreateProductOrder(Client_OrderModel model);
    }
}
