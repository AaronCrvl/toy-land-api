using ApiToyLand.Models;
using ApiToyLand.Repository;
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
        #region Endpoints
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

                AccountRepository ac = new AccountRepository();
                if (ac.LogIn(id))
                {
                    if (ac.Auth.IdAccount <= 0)
                        return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.NotFound);

                    var DataResult = new ContentResult();
                    var jsonString = JsonConvert.SerializeObject(FillModel(ac.GetData()));
                    DataResult.ContentType = "application/json";
                    DataResult.ContentEncoding = System.Text.Encoding.UTF8;
                    DataResult.Content = jsonString;
                    return DataResult;
                }
                else
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
                var AccountRepo = new AccountRepository();

                #region Validations
                if (!AccountRepo.Validate(newAccount))
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    var response = new ContentResult();
                    response.Content = "Please fill all fields to create a account.";
                    response.ContentType = "application/json";
                    response.ContentEncoding = System.Text.Encoding.UTF8;
                    return response;
                }

                if (!AccountRepo.testUsername(newAccount.Username))
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
                AccountRepo.CreateAccount(newAccount);
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.OK, "The Account was created sucessfuly.");
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
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
                var AccountRepo = new AccountRepository();

                #region Validations
                if (!AccountRepo.Validate(userAccount))
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    var res406 = new ContentResult();
                    res406.Content = "Please fill all fields to create a account.";
                    res406.ContentType = "application/json";
                    res406.ContentEncoding = System.Text.Encoding.UTF8;
                    return res406;
                }

                if (!AccountRepo.testUsername(userAccount.UserName))
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    var res409 = new ContentResult();
                    res409.Content = "Please select another username.";
                    res409.ContentType = "application/json";
                    res409.ContentEncoding = System.Text.Encoding.UTF8;
                    return res409;
                }
                #endregion

                AccountRepo.AlterAccount(userAccount);
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
        #endregion        

        #region Methods
        AccountModel FillModel(Account account)
        {
            var model = new AccountModel();
            model.IdAccount = account.IdAccount;
            model.FirstName = account.First_Name;
            model.LastName = account.Last_Name;
            model.UserName = account.Username;
            model.Password = account.Password;
            model.Active = account.Active;
            return model;
        }
        #endregion
    }
}