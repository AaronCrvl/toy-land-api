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
        // GET: Auth
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //[HttpGet]
        //[EnableCors()]
        //[Route("CheckStatus")]
        //https://localhost:44393/Auth/CheckStatus/
        //public ActionResult CheckStatus()
        //{
        //    try
        //    {
        //        var model = FillModel(product);
        //        var DataResult = new ContentResult();
        //        var jsonString = JsonConvert.SerializeObject(model);
        //        DataResult.ContentType = "application/json";
        //        DataResult.ContentEncoding = System.Text.Encoding.UTF8;
        //        DataResult.Content = "{\n\"Response\": " + jsonString + "}\n";
        //        return DataResult;
        //    }
        //    catch (Exception)
        //    {
        //        return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
        //        // criar log exceptions
        //    }
        //}

        [HttpGet]
        [EnableCors()]
        [Route("Validate")]
        //https://localhost:44393/Auth/Validate/
        public ActionResult Validate(long key)
        {
            try
            {
                Account ac = new Account();
                ac.LoadByKey(key);

                if(ac.IdAccount < 0)                                                                    // BadRequest
                    return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.NotFound);

                else if (ac.IdAccount > 0 && ac.TxtKey > 0 && ac.ExpireDate > DateTime.Now)             // Valid
                    return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.Accepted);

                else if (ac.IdAccount > 0 && ac.TxtKey > 0 && ac.ExpireDate < DateTime.Now)             // Expired
                    return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.Gone);

                else                                                                                    // Default
                    return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return new System.Web.Mvc.HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                // criar log exceptions
            }
        }
        #region Validation Methods
        #endregion
    }
}