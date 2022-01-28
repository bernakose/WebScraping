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
            //List<Home> homes = new List<Home>();
            //    for (int i = 1; i < 15; i++)
            //    {            
            //    var web = new HtmlWeb();
            //    var doc = web.Load("https://www.hepsiemlak.com/kiralik?page="+i);
            //        foreach (var item in doc.DocumentNode.SelectNodes("//a[@class='img-link']"))
            //        {
            //            // string title = item.ChildNodes[2].ChildNodes[0].ChildNodes[2].ChildNodes[2].ChildNodes[0].ChildNodes[0].InnerText.Trim();
            //            string title = item.GetAttributeValue("title", "").Trim();
            //            // string link = "https://www.hepsiemlak.com" + item.ChildNodes[2].ChildNodes[0].ChildNodes[2].ChildNodes[0].ChildNodes[0].GetAttributeValue("href", "").Trim();
            //            string link = "https://www.hepsiemlak.com" + item.GetAttributeValue("href", "").Trim();
            //            // string img = item.ChildNodes[2].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[2].GetAttributeValue("src", "").Trim();
            //            string img = item.ChildNodes[2].GetAttributeValue("src","").Trim();
            //            //string country = item.GetAttributeValue("","").Trim();
            //            homes.Add(new Home()
            //            {
            //                title = title,
            //                link = link,
            //                image = img
            //            });
            //        }              

            //    }
            //    return View(homes);




            List<Home> homes = new List<Home>();
            for (int i = 1; i < 25; i++)
            {
                var web = new HtmlWeb();
                var doc = web.Load("https://www.hepsiemlak.com/kiralik?page=" + i);
                foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='list-view-line']"))
                {
                    string price = item.ChildNodes[2].ChildNodes[2].ChildNodes[0].ChildNodes[0].InnerText.Trim();
                    string city = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[2].ChildNodes[0].ChildNodes[2].ChildNodes[2].InnerText.Trim();
                    string age = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[6].ChildNodes[0].InnerText.Trim();
                    string floor = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[8].ChildNodes[0].InnerText.Trim();
                    string room = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[0].InnerText.Trim();
                    string metre = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[4].ChildNodes[0].InnerText.Trim();
                    string link = "https://www.hepsiemlak.com" + item.ChildNodes[2].ChildNodes[0].ChildNodes[0].GetAttributeValue("href", "").Trim();
                    string img = item.ChildNodes[0].ChildNodes[2].ChildNodes[2].GetAttributeValue("data-src", "").Trim();
                    homes.Add(new Home()
                    {
                        price = price,
                        city = city,
                        room = room,
                        age = age,
                        floor=floor,
                        metre = metre,
                        link = link,
                        image = img
                    }); ;
                }

            }
            return View(homes);
        }
    }
}