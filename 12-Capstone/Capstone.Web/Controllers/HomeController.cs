using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAO parkDAO;
        private IWeatherDAO weatherDAO;
        private ISurveyResultDAO surveyDAO;
        public HomeController(IParkDAO parkDAO, IWeatherDAO weatherDAO, ISurveyResultDAO surveyDAO )
        {
            this.parkDAO = parkDAO;
            this.weatherDAO = weatherDAO;
            this.surveyDAO = surveyDAO;
        }

        

        [HttpGet]
        public IActionResult Index(Park park)
        {
            IList<Park> parks = parkDAO.GetParks();

            park.Parks = parks;

            return View(parks);
        }

        [HttpGet]
        public IActionResult Detail(string parkCode)
        {
            ParkDetailVM park = new ParkDetailVM();
            park.Park = parkDAO.GetParkDetails(parkCode);
            park.Weather = weatherDAO.GetWeather(parkCode);


            return View(park);
        }

        [HttpGet]
        public IActionResult Survey()
        {
            Survey survey = new Survey();

            return View(survey);
        }

        [HttpPost]
        public IActionResult AddSurvey(Survey survey)
        {
           int surveyId = surveyDAO.AddSurvey(survey);
            var obj = new { id = surveyId };

            return RedirectToAction("FavoriteParks", obj);
        }

       

        [HttpGet]
        public IActionResult FavoriteParks()
        {
            var surveyResult = surveyDAO.GetSurveyResults();

            return View(surveyResult);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
