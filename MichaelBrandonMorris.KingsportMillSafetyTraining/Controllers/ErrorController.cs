using System;
using System.Net;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var error = this.GetError();

                var model = new ErrorViewModel
                {
                    Code = error.Code,
                    Message = error.Message
                };

                Response.StatusCode = (int)error.Code;
                return View(model);
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }
        }
    }
}