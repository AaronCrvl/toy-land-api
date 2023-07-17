using ApiToyLand.Models;
using ApiToyLand.Repository.Interfaces;
using LibraryToyLand.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiToyLand.Repository
{
    public class ChatBotRepository : IChatBotRepository
    {
        public List<Client_OrderModel> GetClientOrders(int idAccount)
        {
            var orderRepo = new ClientOrderRepository();
            return orderRepo.GetOrderList(idAccount);
        }
    }
}