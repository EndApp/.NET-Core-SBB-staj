using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Staj.Web.Models;
using Staj.Web.Repositories;

namespace Staj.Controllers
{
    public class WeatherController : Controller
    {
        WeatherRepository weatherRepository = new WeatherRepository(); //hava durumu alma ve yonetme
        List<string> lastCities = new List<string> { "London", "Istanbul", "Berlin", "Paris"};  // sayfada hep gosterılecek olan sehırler

        public IActionResult Index() //varsayılan sayfa
        {
            foreach (var city in lastCities) //her şehir için yukarıdakı lıstedeki
            {
                weatherRepository.Add(weatherRepository.FindNewByName(city)); //hava durumunu al ve repository e ekle
            }
            return View(weatherRepository.GetAll()); //hava durumu verılerını gorunume gecır
        }

        [HttpPost]
        public JsonResult GetCities() //hava durumu veırlerını json formatında dondur AJAX için
        {
            return Json(weatherRepository.GetAll());
        }

        [HttpPost]
        public JsonResult SearchWeather(string city) //aranan şehir için döndürür json ajax
        {
            return Json(weatherRepository.FindNewByName(city));
        }

    }
}