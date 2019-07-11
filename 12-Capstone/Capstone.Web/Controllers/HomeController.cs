using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Detail(string parkCode)  // Show Park Detail (includes weather)
        {
            ParkDetailVM parkVM = new ParkDetailVM();

            parkVM.TempUnit = HttpContext.Session.GetString("tempUnit");
            if (parkVM.TempUnit == null)
            {
                parkVM.TempUnit = "F";
                HttpContext.Session.SetString("tempUnit", parkVM.TempUnit);
            }
            parkVM.Park = parkDAO.GetParkDetails(parkCode);
            parkVM.Weather = weatherDAO.GetWeather(parkCode, parkVM.TempUnit);
            return View(parkVM);
        }

        [HttpPost]
        public IActionResult Detail(string parkCode, string tempUnit)
        {
            HttpContext.Session.SetString("tempUnit", tempUnit);
            
            return RedirectToAction("Detail", "Home", new { ParkCode = parkCode });

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

        //private void SaveTempInProgress(ParkDetailVM parkDetailVM)
        //{
        //    // Convert the Recipe object to a string using the JSON library
        //    string json = JsonConvert.SerializeObject(parkDetailVM);

        //    // Put the string into session under the key="RecipeInProgress"
        //    HttpContext.Session.SetString("TempInProgress", json);
        //}

        //private void ClearTempInProgress()
        //{
        //    // Put the string into session under the key="RecipeInProgress"
        //    HttpContext.Session.Remove("TempInProgress");
        //}

        //private ParkDetailVM GetTempInProgress()
        //{
        //    ParkDetailVM parkDetailVM = null;

        //    // Get the serialized json string from the session, key="Cart"
        //    string json = HttpContext.Session.GetString("TempInProgress");

        //    if (json == null)
        //    {
        //        parkDetailVM = new ParkDetailVM();
        //    }
        //    else
        //    {
        //        // De-serialize the json string into a SC object
        //        parkDetailVM = (ParkDetailVM)JsonConvert.DeserializeObject<ParkDetailVM>(json);
        //    }
        //    return parkDetailVM;
        //}
        





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
