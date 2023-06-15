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
    public class AccountController : Controller
    {
        [HttpGet]
        [EnableCors()]
        [Route("GetAccount/{id}")]
        //https://localhost:44393/Account/GetAccount/
        public ActionResult GetAccount(int id)
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                Account ac = new Account();
                ac.Load(id);

                if (ac.IdAccount > 0 && ac.Active)
                {
                    var DataResult = new ContentResult();
                    var jsonString = JsonConvert.SerializeObject(FillModel(ac));
                    DataResult.ContentType = "application/json";
                    DataResult.ContentEncoding = System.Text.Encoding.UTF8;
                    DataResult.Content = jsonString;
                    return DataResult;
                }
                else if (ac.IdAccount < 0)
                    return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.NotFound);

                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [EnableCors()]
        [Route("CreateAccount/")]
        //https://localhost:44393/Account/CreateAccount/
        public ActionResult CreateAccount()
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var result = new StreamReader(HttpContext.Request.InputStream).ReadToEnd();
                var newAccount = JsonConvert.DeserializeObject<newAccountModel>(result);

                #region Validations
                if (string.IsNullOrEmpty(newAccount.FirstName)
                    || string.IsNullOrEmpty(newAccount.LastName)
                    || string.IsNullOrEmpty(newAccount.Username))
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    var response = new ContentResult();
                    response.Content = "Please fill all fields to create a account.";
                    response.ContentType = "application/json";
                    response.ContentEncoding = System.Text.Encoding.UTF8;
                    return response;
                }

                var testAcct = new Account();
                testAcct.LoadByUsername(newAccount.Username);
                if (testAcct.IdAccount > 0)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    var response = new ContentResult();
                    response.Content = "Please select another username.";
                    response.ContentType = "application/json";
                    response.ContentEncoding = System.Text.Encoding.UTF8;
                    return response;
                }
                #endregion

                // sucess
                Account ac = new Account();
                ac.USERNAME = newAccount.Username;
                ac.First_Name = newAccount.FirstName;
                ac.Last_Name = newAccount.LastName;
                ac.Password = newAccount.Password;
                ac.Active = true;
                ac.SaveNew();
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.OK, "The Account was created sucessfuly.");
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [EnableCors()]
        [Route("AlterAccount/")]
        //https://localhost:44393/Account/AlterAccount/
        public ActionResult AlterAccount()
        {
            try
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "*");
                HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "*");

                var result = new StreamReader(HttpContext.Request.InputStream).ReadToEnd();
                var userAccount = JsonConvert.DeserializeObject<AccountModel>(result);

                #region Validations
                if (string.IsNullOrEmpty(userAccount.FirstName)
                    || string.IsNullOrEmpty(userAccount.LastName)
                    || string.IsNullOrEmpty(userAccount.UserName))
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    var rest406 = new ContentResult();
                    rest406.Content = "Please fill all fields to create a account.";
                    rest406.ContentType = "application/json";
                    rest406.ContentEncoding = System.Text.Encoding.UTF8;
                    return rest406;
                }

                var testAcct = new Account();
                testAcct.LoadByUsername(userAccount.UserName);
                if (testAcct.IdAccount > 0 && testAcct.IdAccount != userAccount.IdAccount)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    var res309 = new ContentResult();
                    res309.Content = "Please select another username.";
                    res309.ContentType = "application/json";
                    res309.ContentEncoding = System.Text.Encoding.UTF8;
                    return res309;
                }
                #endregion

                Account ac = new Account();
                ac.Load(userAccount.IdAccount);

                ac.USERNAME = userAccount.UserName;
                ac.First_Name = userAccount.FirstName;
                ac.Last_Name = userAccount.LastName;
                ac.Password = userAccount.Password;
                ac.Active = true;

                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                var response = new ContentResult();
                response.Content = "Account updated!";
                response.ContentType = "application/json";
                response.ContentEncoding = System.Text.Encoding.UTF8;
                return response;                
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #region Validation Methods
        AccountModel FillModel(Account account)
        {
            var model = new AccountModel();
            model.IdAccount = account.IdAccount;
            model.FirstName = account.First_Name;
            model.LastName = account.Last_Name;
            model.UserName = account.USERNAME;
            model.Password = account.Password;
            model.Active = account.Active;
            return model;
        }
        #endregion
    }
}