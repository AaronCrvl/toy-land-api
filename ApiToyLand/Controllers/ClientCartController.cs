using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryToyLand.Data.Objects;
using LibraryToyLand.Data.Lists;
using ApiToyLand.Models;
using Newtonsoft.Json;

namespace ApiToyLand.Controllers
{
    public class ClientCartController : Controller
    {
        #region Endpoints
        [HttpPost]
        [EnableCors()]
        [Route("StoreInCard/{idAccount}/{idProduct}")]
        public ActionResult StoreInCart(int idAccount, int idProduct)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var success = ClientCart.AddProductToCart(idAccount, idProduct);
                if (success)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    var res200 = new ContentResult();
                    res200.Content = "Product stored in cart.";
                    res200.ContentType = "application/json";
                    res200.ContentEncoding = System.Text.Encoding.UTF8;
                    return res200;
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var res400 = new ContentResult();
                    res400.Content = "Something went wrong adding this product to cart.";
                    res400.ContentType = "application/json";
                    res400.ContentEncoding = System.Text.Encoding.UTF8;
                    return res400;
                }
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [EnableCors()]
        [Route("GetCartCount/{idAccount}")]
        public ActionResult GetCartCount(int idAccount)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                int count = ClientCart.GetCartCount(idAccount);

                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                var res200 = new ContentResult();
                res200.Content = count.ToString();
                res200.ContentType = "application/json";
                res200.ContentEncoding = System.Text.Encoding.UTF8;
                return res200;
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [EnableCors()]
        [Route("RemoveProductFromCart/{idAccount}/{idProduct}")]
        public ActionResult RemoveProductFromCart(int idAccount, int idProduct)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                bool removeSucceded = ClientCart.RemoveProductFromCart(idAccount, idProduct);
                if (removeSucceded)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    var res200 = new ContentResult();
                    res200.Content = "Product removed from cart";
                    res200.ContentType = "application/json";
                    res200.ContentEncoding = System.Text.Encoding.UTF8;
                    return res200;
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var res400 = new ContentResult();
                    res400.Content = "Failed removing product from cart";
                    res400.ContentType = "application/json";
                    res400.ContentEncoding = System.Text.Encoding.UTF8;
                    return res400;
                }
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [EnableCors()]
        [Route("GetCartCount/{idAccount}")]
        public ActionResult GetCartProducts(int idAccount)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var list = new ListClientCart().LoadProductList(idAccount);
                var finalList = FillProductModelList(list).AsEnumerable().OrderBy(x => x.idProduct);

                if (list.Count == 0)
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
        private List<ProductModel> FillProductModelList(List<Product> pList)
        {
            var list = new List<ProductModel>();
            foreach (Product p in pList)
            {
                var model = new ProductModel();
                model.idProduct = p.IdProduct.ToString();
                model.productName = p.ProductName;
                model.shortDescription = p.ShortDescription;
                model.imageUrl = p.ImageUrl;
                list.Add(model);
            }
            return list;
        }
        #endregion
    }
}