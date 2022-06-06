﻿using HtmlAgilityPack;
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
                    //string city = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[2].ChildNodes[0].ChildNodes[2].ChildNodes[2].InnerText.Trim();
                    string age = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[6].ChildNodes[0].InnerText.Trim();
                    string floor = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[8].ChildNodes[0].InnerText.Trim();
                    string room = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[0].InnerText.Trim();
                    string metre = item.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[2].ChildNodes[4].ChildNodes[0].InnerText.Trim();
                    string link = "https://www.hepsiemlak.com" + item.ChildNodes[2].ChildNodes[0].ChildNodes[0].GetAttributeValue("href", "").Trim();
                    string img = item.ChildNodes[0].ChildNodes[2].ChildNodes[2].GetAttributeValue("data-src", "").Trim();

                    var doc2 = web.Load(link);


                    foreach (var item2 in doc2.DocumentNode.SelectNodes("//div[@class='cont-inner']"))
                    {
                        var shortcut= item2.ChildNodes[0].ChildNodes[6].ChildNodes[2].ChildNodes[1];
                        var shortcut1 = item2.ChildNodes[0].ChildNodes[6].ChildNodes[2].ChildNodes[0];

                        string city = item2.ChildNodes[0].ChildNodes[4].ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText.Trim();
                        string county = item2.ChildNodes[0].ChildNodes[4].ChildNodes[0].ChildNodes[2].ChildNodes[0].InnerText.Trim();
                        string district = item2.ChildNodes[0].ChildNodes[4].ChildNodes[0].ChildNodes[4].ChildNodes[0].InnerText.Trim();
                        string totalFloor= shortcut.ChildNodes[0].ChildNodes[2].ChildNodes[0].InnerText.Trim();
                        string heating = shortcut1.ChildNodes[8].ChildNodes[2].ChildNodes[0].InnerText.Trim();
                        string isFurnished = shortcut.ChildNodes[1].ChildNodes[2].ChildNodes[0].InnerText.Trim();
                        string formHousing = shortcut1.ChildNodes[3].ChildNodes[2].ChildNodes[0].InnerText.Trim();
                        string bathroom = shortcut.ChildNodes[2].ChildNodes[2].ChildNodes[0].InnerText.Trim();

                        homes.Add(new Homes()
                        {
                            price = price,
                            city = city,
                            county = county,
                            age = age,
                            floor = floor,
                            room = room,
                            metre = metre,
                            district = district,
                            totalFloor = totalFloor,
                            heating=heating,
                            isFurnished=isFurnished,
                            formHousing=formHousing,
                            bathroom=bathroom,
                            link = link,
                            image = img
                        }); ;
                        break;
                    }
                }
            }

            //sahibinden çekilen veriler
            List<HomesSahibinden> homesSahibinden = new List<HomesSahibinden>();
            for (int i = 20; i < 120; i += 20)
            {
                var web = new HtmlWeb();
                var doc = web.Load("https://www.sahibinden.com/kiralik?pagingOffset=" + i);

                foreach (var item in doc.DocumentNode.SelectNodes("//tr[@class='searchResultsItem     ']"))
                {
                    string price = item.ChildNodes[11].ChildNodes[1].InnerText.Trim();
                    string link = "https://www.sahibinden.com/" + item.ChildNodes[1].ChildNodes[1].GetAttributeValue("href", "").Trim();
                    string img = item.ChildNodes[1].ChildNodes[1].ChildNodes[1].GetAttributeValue("src", "").Trim();
                    string city = item.ChildNodes[15].ChildNodes[0].InnerText.Trim();
                    string county = item.ChildNodes[15].ChildNodes[2].InnerText.Trim();
                    string metre = item.ChildNodes[7].ChildNodes[0].InnerText.Trim();
                    string room = item.ChildNodes[9].ChildNodes[0].InnerText.Trim();
                    //string announcementDate = item.ChildNodes[13].ChildNodes[1].ChildNodes[0].InnerText.Trim();
                    //string announcementYear = item.ChildNodes[13].ChildNodes[5].ChildNodes[0].InnerText.Trim();
                    string formHousing = item.ChildNodes[3].ChildNodes[0].InnerText.Trim();

                    var doc2 = web.Load(link);

                    foreach (var item2 in doc2.DocumentNode.SelectNodes("//div[@class='classifiedDetail']"))
                    {
                        var shortcut = item2.ChildNodes[5].ChildNodes[13].ChildNodes[6];

                        string district = item2.ChildNodes[5].ChildNodes[13].ChildNodes[5].ChildNodes[9].ChildNodes[0].InnerText.Trim();
                        string age = shortcut.ChildNodes[13].ChildNodes[3].InnerText.Trim();
                        string floor= shortcut.ChildNodes[15].ChildNodes[3].InnerText.Trim();
                        string totalFloor= shortcut.ChildNodes[17].ChildNodes[3].InnerText.Trim();
                        string heating = shortcut.ChildNodes[19].ChildNodes[3].InnerText.Trim();
                        string isFurnished = shortcut.ChildNodes[25].ChildNodes[3].InnerText.Trim();
                        string bathroom = shortcut.ChildNodes[21].ChildNodes[3].InnerText.Trim();

                        homes.Add(new Homes()
                        {
                            price = price,
                            city = city,
                            county = county,
                            age = age,
                            floor = floor,
                            room = room,
                            metre = metre,
                            district = district,
                            totalFloor = totalFloor,
                            heating = heating,
                            isFurnished = isFurnished,
                            formHousing = formHousing,
                            bathroom = bathroom,
                            link = link,
                            image = img
                        }); ;
                        break;
                    }
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
            //db.HomesSahibinden.AddRange(homesSahibinden);
            if (db.SaveChanges() > 0)
            {
                //return RedirectToAction("Index");
            }
            else
            {
                //return View();
            }


            //önce homes verilerini hürriyet için oluşturduğun tabloya bas

            //sonra homessahibinden verilerini yeni oluşturacağın tabloya bas

            //iki tablo da tamamen aynı olacak 


            homesAllDatas.AddRange(homes);
            //homesAllDatas.AddRange(homesSahibinden);


            return View(homesAllDatas);
        }
    }
}