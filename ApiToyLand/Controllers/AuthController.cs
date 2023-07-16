using ApiToyLand.Models;
using ApiToyLand.Repository;
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
        #region Endpoints
        [HttpGet]
        [EnableCors()]
        [Route("Validate/{username}/{key}")]
        //https://localhost:44393/Auth/Validate/
        public ActionResult Validate(string username, string password)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                AuthRepository ac = new AuthRepository();
                var logged = ac.LogIn(username, password);

                if (logged)
                {
                    switch (ac.Validate())
                    {
                        case 404:
                            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            var res404 = new ContentResult();
                            res404.Content = "Account not found.";
                            res404.ContentType = "application/json";
                            res404.ContentEncoding = System.Text.Encoding.UTF8;
                            return res404;

                        case 401:
                            HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            var res401 = new ContentResult();
                            res401.Content = "This account is deactivated.";
                            res401.ContentType = "application/json";
                            res401.ContentEncoding = System.Text.Encoding.UTF8;
                            return res401;

                        case 200:
                            var DataResult = new ContentResult();
                            var jsonString = JsonConvert.SerializeObject(FillModel(ac.GetData()));
                            DataResult.ContentType = "application/json";
                            DataResult.ContentEncoding = System.Text.Encoding.UTF8;
                            DataResult.Content = jsonString;
                            return DataResult;

                        default:
                            break;
                    }
                }

                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var res400 = new ContentResult();
                res400.Content = "Something went wrong.";
                res400.ContentType = "application/json";
                res400.ContentEncoding = System.Text.Encoding.UTF8;
                return res400;
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion      

        #region Validation Methods
        AuthModel FillModel(Account account)
        {
            var model = new AuthModel();
            model.IdAccount = account.IdAccount;
            model.UserName = account.Username;
            return model;
        }
        #endregion
    }
}