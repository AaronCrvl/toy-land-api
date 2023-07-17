using ApiToyLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiToyLand.Repository.Interfaces
{
    public interface IChatBotRepository
    {
        List<Client_OrderModel> GetClientOrders(int idAccount);
    }
}
