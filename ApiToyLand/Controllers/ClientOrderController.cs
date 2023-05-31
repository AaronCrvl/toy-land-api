using ApiToyLand.Models;
using LibraryToyLand.Data.Objects;
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
                    var newClientOrder = new Client_Order();                    
                    newClientOrder.idProduct = clientOrderModel.idProduct;
                    newClientOrder.idAccount = clientOrderModel.idAccount;
                    newClientOrder.finished = false;

                    string successMessage = $"New order created! Product {product.ProductName} on shipping for client {account.First_Name + account.Last_Name}.";
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

        private object FillProductModel(Product product)
        {
            throw new NotImplementedException();
        }
    }
}