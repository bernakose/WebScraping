using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebScraping.Models.Siniflar;

namespace WebScraping.Controllers
{
    public class HomeController : Controller
    {
        WebScrapingDBEntities db = new WebScrapingDBEntities();

        public ActionResult Index()
        {
            var getHome = db.Homes.ToList();
            var getHomeSahibinden = db.HomesSahibinden.ToList();

            List<Homes> homes = new List<Homes>();
            homes.AddRange(getHome);

            ViewBag.sahibindenveri = getHomeSahibinden;
            
            return View(homes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}