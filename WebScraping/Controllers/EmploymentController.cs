using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebScraping.Models;

namespace WebScraping.Controllers
{
    public class EmploymentController : Controller
    {
        // GET: Employment
        public ActionResult Index()
        {
            List<Home> homes = new List<Home>();
            var web = new HtmlWeb();
            var doc = web.Load("https://www.hepsiemlak.com/satilik");

            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='upper sibling']"))
            {
                string title = item.ChildNodes[0].ChildNodes[0].InnerText.Trim();
                homes.Add(new Home()
                {
                    title = title
                });
            }
            

            return View(homes);
        }
    }
}