using ApiToyLand.Models;
using ApiToyLand.Repository.Interfaces;
using LibraryToyLand.Data;
using LibraryToyLand.Data.Objects;
using LibraryToyLand.eNums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiToyLand.Repository
{
    public class ClientOrderRepository : IClientOrderRepository
    {
        public int CreateProductOrder(Client_OrderModel model)
        {
            var product = new Product(model.idProduct);
            var account = new Account(model.idAccount);

            if (account.Active = true && !string.IsNullOrEmpty(product.ProductName))
            {
                var orderCheck = new Client_Order();
                orderCheck.LoadByAccountAndProduct(model.idAccount, model.idProduct);
                if (orderCheck.idClientOrder > 0 && !orderCheck.finished)
                    return 409;          
                
                var newClientOrder = new Client_Order();
                newClientOrder.idProduct = model.idProduct;
                newClientOrder.idAccount = model.idAccount;
                newClientOrder.finished = false;

                var newProductOrder = new Product_Order();
                newProductOrder.ID_CLIENT_ORDER = newClientOrder.Save();
                newProductOrder.PRODUCT_NAME = product.ProductName;
                newProductOrder.ID_STATUS_ORDER = (int)eStatusClientOrder.New;
                newProductOrder.CLIENT_LOCATION = model.location;
                newProductOrder.EMAIL = model.email;
                newProductOrder.HASH_CODE = hashGenerator.Hash(DateTime.Now).ToString();
                newProductOrder.SaveNew();
                return 200;
            }

            return 500;
        }
    }
}