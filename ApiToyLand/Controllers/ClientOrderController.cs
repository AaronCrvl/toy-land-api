using ApiToyLand.Models;
using LibraryToyLand.Data;
using LibraryToyLand.Data.Lists;
using LibraryToyLand.Data.Objects;
using LibraryToyLand.eNums;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ApiToyLand.Controllers
{
    public class ClientOrderController : Controller
    {
        #region Endpoints
        [HttpPost]
        [EnableCors()]
        [Route("CreateProductOrder/")]
        //https://localhost:44393/Product/GetProductById/
        public ActionResult CreateProductOrder()
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var result = new StreamReader(HttpContext.Request.InputStream).ReadToEnd();
                var clientOrderModel = JsonConvert.DeserializeObject<Client_OrderModel>(result);

                var product = new Product(clientOrderModel.idProduct);
                var account = new Account(clientOrderModel.idAccount);

                if (account.Active = true && !string.IsNullOrEmpty(product.ProductName))
                {
                    var orderCheck = new Client_Order();
                    orderCheck.LoadByAccountAndProduct(clientOrderModel.idAccount, clientOrderModel.idProduct);
                    if (orderCheck.idClientOrder > 0 && !orderCheck.finished)
                    {
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                        var response = new ContentResult();
                        response.Content = "You already have unfinished orders with this product on your account, check your profile page. Try again when they finish.";
                        response.ContentType = "application/json";
                        response.ContentEncoding = System.Text.Encoding.UTF8;
                        return response;
                    }

                    var newClientOrder = new Client_Order();
                    newClientOrder.idProduct = clientOrderModel.idProduct;
                    newClientOrder.idAccount = clientOrderModel.idAccount;
                    newClientOrder.finished = false;

                    var newProductOrder = new Product_Order();
                    newProductOrder.ID_CLIENT_ORDER = newClientOrder.Save();
                    newProductOrder.PRODUCT_NAME = product.ProductName;
                    newProductOrder.ID_STATUS_ORDER = (int)eStatusClientOrder.New;
                    newProductOrder.CLIENT_LOCATION = clientOrderModel.location;
                    newProductOrder.EMAIL = clientOrderModel.email;
                    newProductOrder.HASH_CODE = hashGenerator.Hash(DateTime.Now).ToString();
                    newProductOrder.SaveNew();

                    string successMessage = $"New order created! Product {product.ProductName} on shipping for client {account.First_Name + " " + account.Last_Name}.";
                    var DataResult = new ContentResult();
                    DataResult.Content = successMessage;
                    return DataResult;
                }

                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [EnableCors()]
        [Route("GetOrdersByClient/")]
        //https://localhost:44393/Product/GetOrdersByClient/
        public ActionResult GetOrdersByClient(long idAccount_)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var list = new ListClientOrder().LoadClientOrderList(idAccount_);
                var finalList = FillProductModelList(list).AsEnumerable().OrderByDescending(x => x.idClientOrder);

                if (list.Count == 0)
                {
                    var DataResult = new ContentResult();
                    DataResult.ContentType = "application/json";
                    DataResult.ContentEncoding = System.Text.Encoding.UTF8;
                    return DataResult;
                }
                else
                {
                    var DataResult = new ContentResult();
                    var jsonString = JsonConvert.SerializeObject(finalList);
                    DataResult.ContentType = "application/json";
                    DataResult.ContentEncoding = System.Text.Encoding.UTF8;
                    DataResult.Content = jsonString;
                    return DataResult;
                }
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [EnableCors()]
        [Route("GetOrder/")]
        //https://localhost:44393/Product/GetOrder/
        public ActionResult GetClientOrder(int idClientOrder)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var clientOrder = new Client_Order();
                clientOrder.Load(idClientOrder);
                var finalList = FillProductModel(clientOrder);

                if (clientOrder.idClientOrder < 0)
                    return new HttpStatusCodeResult((int)HttpStatusCode.NotFound);
                else
                {
                    var DataResult = new ContentResult();
                    var jsonString = JsonConvert.SerializeObject(finalList);
                    DataResult.ContentType = "application/json";
                    DataResult.ContentEncoding = System.Text.Encoding.UTF8;
                    DataResult.Content = jsonString;
                    return DataResult;
                }
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion       

        #region Methods
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
        #endregion        
    }
}