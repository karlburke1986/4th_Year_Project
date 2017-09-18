using ProjectWebsite.Models;
using ProjectWebsite.Models.WeatherModels;
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Helpers;
using PagedList;

namespace ProjectWebsite.Controllers
{
    // Site URL : http://weathersiteitt1.azurewebsites.net/

    public class HomeController : Controller
    {


        WeatherDBContext db = new WeatherDBContext();
        public ActionResult Index(int? page)
        {
            try
            {
                List<weather> models = db.Weather.ToList();
                List<weather> temp = models;
                List<weather> tempyesterday = models;

                models = models.OrderByDescending(t => t.dateTime).ToList();

                string today = DateTime.Now.ToShortDateString();
                string yesterday = DateTime.Now.AddDays(-1).ToShortDateString();

                temp = temp.Where(d => d.dateTime.ToShortDateString().Equals(today)).ToList();

                temp = temp.OrderBy(t => t.temp).ToList();

                weather first = models.FirstOrDefault();

                if (first != null)
                {
                    ViewBag.LatestTemp = first.temp;

                    ViewBag.LatestTime = first.dateTime.ToShortTimeString();
                }
                else
                {
                    ViewBag.LatestTemp = "N/A";

                    ViewBag.LatestTime = "N/A";
                }


                tempyesterday = tempyesterday.Where(d => d.dateTime.ToShortDateString().Equals(yesterday)).ToList();

                tempyesterday = tempyesterday.OrderBy(t => t.temp).ToList();

                weather tempYesH = tempyesterday.FirstOrDefault();

                if (temp != null && temp.Count() != 0)
                {
                    ViewBag.HighestTemp = temp.FirstOrDefault().temp;
                    ViewBag.LowestTemp = temp.LastOrDefault().temp;
                }
                else
                {
                    ViewBag.HighestTemp = "N/A";
                    ViewBag.LowestTemp = "N/A";
                }

                if (tempyesterday != null && tempyesterday.Count() != 0)
                {
                    ViewBag.HighestTempYes = tempyesterday.FirstOrDefault().temp;
                    ViewBag.LowestTempYes = tempyesterday.LastOrDefault().temp;
                }
                else
                {
                    ViewBag.HighestTempYes = "N/A";
                    ViewBag.LowestTempYes = "N/A";
                }

                int pageNumber = (page ?? 1);
                int pageSize = 10;

                return View(models.ToPagedList(pageNumber, pageSize));
            }
            catch(Exception)
            {
                return View("Error");
            }
        }  
        
        public ActionResult Graph()
        {
            var model = new CharViewModel
            {
                Chart = GetChart()
            };

            return PartialView("_Graph", model);

        }

        private Chart GetChart()
        {
            List<weather> models = db.Weather.ToList();
            string[] dates = new string[models.Count];
            string[] temps = new string[models.Count]; 

            for(int i = 0; i < models.Count; i++)
            {
                dates[i] = models[i].dateTime.ToShortDateString();
                temps[i] = models[i].temp.ToString();
            }

            return new Chart(600, 400, ChartTheme.Blue)
                .AddTitle("Line Chart of Data Recorded in 2017")
                .AddLegend()
                .AddSeries(
                    name: "Temperature",
                    chartType: "line",
                    xValue: dates,
                    yValues: temps);
        }        
    }
}
