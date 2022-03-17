using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebScraping.Models.Siniflar;

namespace WebScraping.Controllers
{
    public class EmploymentController : Controller
    {
        // GET: Employment
        WebScrapingDBEntities db = new WebScrapingDBEntities();

        public ActionResult Index(Homes model)
        {
            List<Homes> homesAllDatas = new List<Homes>();


            //hepsiemlaktan çekilen veriler
            List<Homes> homes = new List<Homes>();
            for (int i = 2; i < 3; i++)
            {
                var web = new HtmlWeb();
                var doc = web.Load("https://www.hepsiemlak.com/kiralik?page=" + i);
                foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='list-view-line']"))
                {
                    string price = item.ChildNodes[2].ChildNodes[2].ChildNodes[0].ChildNodes[0].InnerText.Trim();
                    string city = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[2].ChildNodes[0].ChildNodes[2].ChildNodes[2].InnerText.Trim();
                    //string age = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[6].ChildNodes[0].InnerText.Trim();
                    //string floor = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[8].ChildNodes[0].InnerText.Trim();
                    string room = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[0].InnerText.Trim();
                    string metre = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[4].ChildNodes[0].InnerText.Trim();
                    string link = "https://www.hepsiemlak.com" + item.ChildNodes[2].ChildNodes[0].ChildNodes[0].GetAttributeValue("href", "").Trim();
                    string img = item.ChildNodes[0].ChildNodes[2].ChildNodes[2].GetAttributeValue("data-src", "").Trim();
                    homes.Add(new Homes()
                    {
                        price = price,
                        city = city,
                        room = room,
                        age = "",
                        floor = "",
                        metre = metre,
                        link = link,
                        image = img
                    }); ;
                }
            }

            //sahibinden çekilen veriler        
            List<HomesSahibinden> homesSahibinden = new List<HomesSahibinden>();
            for (int i = 1; i < 2; i++)
            {
                var web = new HtmlWeb();
                var doc = web.Load("https://www.sahibinden.com/kiralik?page=" + i);

                foreach (var item in doc.DocumentNode.SelectNodes("//tr[@class='searchResultsItem     ']"))
                {
                    string price = item.ChildNodes[11].ChildNodes[1].InnerText.Trim();
                    string link = "https://www.sahibinden.com/" + item.ChildNodes[1].ChildNodes[1].GetAttributeValue("href", "").Trim();
                    string img = item.ChildNodes[1].ChildNodes[1].ChildNodes[1].GetAttributeValue("src", "").Trim();
                    string city = item.ChildNodes[15].ChildNodes[0].InnerText.Trim();
                    string county = item.ChildNodes[15].ChildNodes[2].InnerText.Trim();
                    string metre = item.ChildNodes[7].ChildNodes[0].InnerText.Trim();
                    string numberOfRoom = item.ChildNodes[9].ChildNodes[0].InnerText.Trim();
                    //string announcementDate = item.ChildNodes[13].ChildNodes[1].ChildNodes[0].InnerText.Trim();
                    //string announcementYear = item.ChildNodes[13].ChildNodes[5].ChildNodes[0].InnerText.Trim();

                    homesSahibinden.Add(new HomesSahibinden()
                    {
                        price = price,
                        link = link,
                        image = img,
                        city = city,
                        age = "",
                        floor = "",
                        metre = metre,
                        room = numberOfRoom,
                    }); ;
                }

            }


            //veritabanına kayıt işlemleri
            
            
            //önce eski kayıtlar silinir
            var getHome = db.Homes.ToList();
            var getHomeSahibinden = db.HomesSahibinden.ToList();

            db.Homes.RemoveRange(getHome);
            db.HomesSahibinden.RemoveRange(getHomeSahibinden);

            //sonra yeni kayıtlar eklenir.
            db.Homes.AddRange(homes);
            db.HomesSahibinden.AddRange(homesSahibinden);
            if (db.SaveChanges() > 0)
            {
                //return RedirectToAction("Index");
            }
            else
            {
                //return View();
            }


            //if (ModelState.IsValid)
            //{
            //    db.Homes.Add();
            //    db.SaveChanges();
            //}

            //önce homes verilerini hürriyet için oluşturduğun tabloya basacaksın

            //sonra homessahibinden verilerini yeni oluşturacağın tabloya basacaksın

            //iki tablo da tamamen aynı olacak 

            
            homesAllDatas.AddRange(homes);
            //homesAllDatas.AddRange(homesSahibinden);


            return View(homesAllDatas);
        }





        //public ActionResult Create(List<Home> homenew)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        foreach (Home item in homenew)
        //        {
        //            db.Homes.Add(item);
        //            db.SaveChanges();
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    return View(homenew);
        //}
    }
}