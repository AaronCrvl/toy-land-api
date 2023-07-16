using ApiToyLand.Models;
using LibraryToyLand.Data.Lists;
using LibraryToyLand.Data.Objects;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using ActionResult = System.Web.Mvc.ActionResult;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System;
using Microsoft.AspNetCore.Cors;
using ApiToyLand.Repository;

namespace ApiToyLand.Controllers
{
    public class ProductController : Controller
    {        
        #region Endpoints
        [HttpGet]
        [EnableCors()]
        [Route("GetProductById/{id}")]        
        //https://localhost:44393/Product/GetProductById/
        public ActionResult GetProductById(int id)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var product = new ProductRepository().GetProductById(id);
                if (product.IdProduct < 0)                                                                           
                    return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.NotFound);
                else
                {          
                    var model = FillProductModel(product);                    
                    var DataResult = new ContentResult();
                    var jsonString = JsonConvert.SerializeObject(model);
                    DataResult.ContentType = "application/json";
                    DataResult.ContentEncoding = System.Text.Encoding.UTF8;                    
                    DataResult.Content = "{\n\"Response\": " + jsonString + "}\n";            
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
        [Route("GetAllProducts")]        
        //https://localhost:44393/Product/GetAllProducts/
        public ActionResult GetAllProducts()
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var productRepository = new ProductRepository();
                var list = productRepository.GetAllProducts();
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

        [HttpGet]
        [EnableCors()]
        [Route("GetProductsByRegisterQuantity/{registers}")]
        //https://localhost:44393/Product/GetProductsByRegisterQuantity/
        public ActionResult GetProductsByRegisterQuantity(int registers = -1)
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

            if (registers == -1)
                return new System.Web.Mvc.HttpStatusCodeResult(501, "Quantity parameter was not sent.");

            try
            {
                var productRepository = new ProductRepository();
                var list = productRepository.GetAllProducts();
                var finalList = FillProductModelList(list).AsEnumerable().OrderBy(x => x.idProduct).Take(registers);

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

        [HttpGet]
        [EnableCors()]
        [Route("GetProductStock/{id}")]
        //https://localhost:44393/Product/GetProductStock/
        public ActionResult GetProductStock(int id)
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

            try
            {
                var repo = new ProductRepository();
                var stock = repo.GetProductStock(id);
                if (stock.Id_Product < 0)
                    return new HttpStatusCodeResult((int)HttpStatusCode.NotFound);
                else
                {
                    var DataResult = new ContentResult();
                    var jsonString = JsonConvert.SerializeObject(FillProductStockModel(stock));
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
        private ProductModel FillProductModel(Product p)
        {
            var model = new ProductModel();
            model.idProduct = p.IdProduct.ToString();
            model.productName = p.ProductName;
            model.shortDescription = p.ShortDescription;
            model.imageUrl = p.ImageUrl;
            return model;
        }

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

        private Product_StockModel FillProductStockModel(Product_Stock p)
        {
            var model = new Product_StockModel();
            model.IdProduct = p.Id_Product;
            model.Qtd = p.Available_Qtt;
            return model;
        }
        #endregion
    }
}