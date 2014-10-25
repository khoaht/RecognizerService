using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecognizerApi.Controllers
{
    public class HomeController : Controller
    {
        private RecognizerService.IRecognizerService service;

        public HomeController(RecognizerService.IRecognizerService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public JsonResult GetFraz(string points)
        {
            try
            {
                var arrs = points.Split(',');
                int length = arrs.Length;
                int[] pnts = new int[length];
                for (int i = 0; i < length; i++)
                {
                    pnts[i] = int.Parse(string.IsNullOrEmpty(arrs[i]) ? "0" : arrs[i]);
                }
                var result = service.Submit(pnts);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return Json(false);
        }
    }
}
