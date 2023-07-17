using ApiToyLand.Models;
using ApiToyLand.Repository.Interfaces;
using LibraryToyLand.Data;
using LibraryToyLand.Data.Lists;
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

        public Client_OrderModel GetOrder(int idAccount, int idProduct)
        {
            var order = new Client_Order();
            order.LoadByAccountAndProduct(idAccount, idProduct);

            if (order.idClientOrder > 0)
            {
                var productOrder = new Product_Order();
                productOrder.Load(order.idClientOrder);

                var model = new Client_OrderModel();
                model.idClientOrder = order.idClientOrder;
                model.idProduct = idProduct;
                model.idAccount = idAccount;
                model.idStatus = order.finished ? (int)eStatusClientOrder.Finished : (int)eStatusClientOrder.Attending;
                model.location = productOrder.CLIENT_LOCATION;
                model.email = productOrder.EMAIL;
                model.orderHashCode = productOrder.HASH_CODE;
                return model;
            }
            else
            {
                var productOrder = new Product_Order();
                productOrder.Load(order.idClientOrder);

                var model = new Client_OrderModel();
                return model;
            }
        }

        public List<Client_OrderModel> GetOrderList(int idAccount)
        {            
            if (idAccount > 0)
            {
                var list = new ListClientOrder().LoadClientOrderList(idAccount);
                return FillProductModelList(list);
            }
            else
            {
                var list = new ListClientOrder().LoadClientOrderList(-1);
                return (List<Client_OrderModel>)FillProductModelList(list).AsEnumerable().OrderByDescending(x => x.idClientOrder);
            }
        }

        private List<Client_OrderModel> FillProductModelList(List<Client_Order> pList)
        {
            var list = new List<Client_OrderModel>();
            foreach (Client_Order p in pList)
            {
                var model = new Client_OrderModel();
                model.idClientOrder = p.idClientOrder;
                model.idProduct = p.idProduct;
                model.finished = p.finished;
                model.productName = new Product(p.idProduct).ProductName;

                var productOrder = new Product_Order();
                productOrder.Load(p.idClientOrder);
                model.orderHashCode = productOrder.HASH_CODE;
                model.idStatus = productOrder.ID_STATUS_ORDER;
                model.location = productOrder.CLIENT_LOCATION;
                model.email = productOrder.EMAIL;
                switch (productOrder.ID_STATUS_ORDER)
                {
                    case 1:
                        model.statusDetail = "New order on the system, soon this will by replaied.";
                        break;
                    case 2:
                        model.statusDetail = "This order is beeing attended, check your e-mail for updates";
                        break;
                    case 3:
                        model.statusDetail = "This order is finished, check your e-mail for more information";
                        break;
                }

                list.Add(model);
            }
            return list;
        }
        private Client_OrderModel FillProductModel(Client_Order p)
        {
            var model = new Client_OrderModel();
            model.idClientOrder = p.idClientOrder;
            model.idProduct = p.idProduct;
            model.finished = p.finished;
            model.productName = new Product(p.idProduct).ProductName;

            var productOrder = new Product_Order();
            productOrder.Load(p.idClientOrder);
            model.orderHashCode = productOrder.HASH_CODE;
            model.idStatus = productOrder.ID_STATUS_ORDER;
            model.location = productOrder.CLIENT_LOCATION;
            model.email = productOrder.EMAIL;
            switch (productOrder.ID_STATUS_ORDER)
            {
                case 1:
                    model.statusDetail = "New order on the system, soon this will by replaied.";
                    break;
                case 2:
                    model.statusDetail = "This order is beeing attended, check your e-mail for updates";
                    break;
                case 3:
                    model.statusDetail = "This order is finished, check your e-mail for more information";
                    break;
            }
            return model;
        }
    }     
}