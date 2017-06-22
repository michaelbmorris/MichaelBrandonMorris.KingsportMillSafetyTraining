using System;
using System.Net;
using System.Web.Mvc;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }     
        }

        public ActionResult Contact()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return this.CreateError(
                    HttpStatusCode.InternalServerError,
                    e.Message);
            }           
        }

        public ActionResult Index()
        {
            try
            {
                return View();
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