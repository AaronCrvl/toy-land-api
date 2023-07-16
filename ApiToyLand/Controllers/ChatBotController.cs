using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiToyLand.Controllers
{
    public class ChatBotController : Controller
    {        
        [HttpGet]
        [EnableCors()]
        [Route("GetText/")]
        //https://localhost:44393/ChatBot/GetText/
        public ActionResult GetText()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

            var DataResult = new ContentResult();
            var jsonString = JsonConvert.SerializeObject("Thanks for using our services! Test");
            DataResult.ContentType = "application/json";
            DataResult.ContentEncoding = System.Text.Encoding.UTF8;
            DataResult.Content = jsonString;
            return DataResult;
        }
    }
}