using ApiToyLand.Repository;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ApiToyLand.Controllers
{
    public class ChatBotController : Controller
    {
        [HttpGet]
        [EnableCors()]
        [Route("GetText/{username}/{password}")]
        //https://localhost:44393/ChatBot/SeeMyOrders/
        public ActionResult SeeMyOrders(string username, string password)
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

            AuthRepository auth = new AuthRepository();
            bool logged = auth.LogIn(username, password);                        

            if (logged)
            {
                var orderRepo = new ClientOrderRepository();
                var data = orderRepo.GetOrderList(auth.GetData().IdAccount);
                var DataResult = new ContentResult();
                var jsonString = JsonConvert.SerializeObject(data);
                DataResult.ContentType = "application/json";
                DataResult.ContentEncoding = System.Text.Encoding.UTF8;
                DataResult.Content = jsonString;
                return DataResult;
            }
            else 
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var response = new ContentResult();
                response.Content = "Option not found.";
                response.ContentType = "application/json";
                response.ContentEncoding = System.Text.Encoding.UTF8;
                return response;
            }
        }
    }
}