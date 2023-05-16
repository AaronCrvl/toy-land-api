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

namespace ApiToyLand.Controllers
{
    public class ProductController : Controller
    {        
        #region GET
        [HttpGet]
        [EnableCors()]
        [Route("GetProductById/{id:int}")]        
        //https://localhost:44393/Product/GetProductById/
        public ActionResult GetProductById(int id)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var product = new Product(id);                       
                if (product.IdProduct < 0)                                                                           
                    return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.NotFound);
                else
                {          
                    var model = FillModel(product);                    
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
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                // criar log exceptions
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

                var list = new ListProducts().LoadProductList();
                var finalList = FillModelList(list).AsEnumerable().OrderBy(x => x.idProduct);                

                if (list.Count == 0)
                    return new HttpStatusCodeResult((int)HttpStatusCode.NotFound);
                else
                {                    
                    var DataResult = new ContentResult();
                    var jsonString = JsonConvert.SerializeObject(finalList);
                    DataResult.ContentType = "application/json";
                    DataResult.ContentEncoding = System.Text.Encoding.UTF8;

                    //var LowTag = "{\n\"ResultCount\": " + finalList.Count().ToString() + ", \"Content\": " + jsonString + "}\n";
                    //var MediumTag = "{\n\"Data\": " + LowTag + "}\n";                                        
                    //DataResult.Content = "{\n\"Response\": " + MediumTag + "}\n";
                    DataResult.Content = jsonString;
                    return DataResult;
                }
            }
            catch (Exception)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                // criar log exceptions
            }
        }

        [HttpGet]
        [EnableCors()]
        [Route("GetProductsByRegisterQuantity/{registers:int}")]
        //https://localhost:44393/Product/GetProductsByRegisterQuantity/
        public ActionResult GetProductsByRegisterQuantity(int registers = -1)
        {
            try
            {
                if (registers == -1)
                    registers = 5;

                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var list = new ListProducts().LoadProductList();
                var finalList = FillModelList(list).AsEnumerable().OrderBy(x => x.idProduct).Take(registers);

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
            catch (Exception)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                // criar log exceptions
            }
        }
        #endregion

        #region Fill Model Methods
        private ProductModel FillModel(Product p)
        {
            var model = new ProductModel();
            model.idProduct = p.IdProduct.ToString();
            model.productName = p.ProductName;
            model.shortDescription = p.ShortDescription;
            model.imageUrl = p.ImageUrl;
            return model;
        }

        private List<ProductModel> FillModelList(List<Product> pList)
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