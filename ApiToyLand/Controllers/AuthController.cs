using ApiToyLand.Models;
using LibraryToyLand.Data.Objects;
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
    public class AuthController : Controller
    {        
        [HttpGet]
        [EnableCors()]
        [Route("Validate/{username}/{key}")]
        //https://localhost:44393/Auth/Validate/
        public ActionResult Validate(string username, long password)
        {                      
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                Account ac = new Account();
                ac.Load(username, password);

                if (ac.IdAccount < 0)                                                                    // BadRequest
                    return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.NotFound);
                
                if (ac.IdAccount > 0 && ac.Active)                                                 // Valid
                {                    
                    var DataResult = new ContentResult();
                    var jsonString = JsonConvert.SerializeObject(FillModel(ac));
                    DataResult.ContentType = "application/json";                    
                    DataResult.ContentEncoding = System.Text.Encoding.UTF8;                    
                    DataResult.Content = jsonString;                    
                    return DataResult;
                }

                if (ac.IdAccount > 0 && !ac.Active)                                                // Expired
                    return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.Gone);
                
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);                
            }
        }

        #region Validation Methods
        AuthModel FillModel(Account account)
        {
            var model = new AuthModel();
            model.IdAccount = account.IdAccount;
            model.UserName = account.USERNAME;         
            return model;
        }
        #endregion
    }
}