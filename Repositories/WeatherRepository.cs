using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using Staj.Web.Models;
using System.Linq;
using Staj.Web.Data;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace Staj.Web.Repositories
{
    public interface IWeatherRepository
    {
        void Add(WeatherModels model);
        void Update(WeatherModels model);
        void Delete(WeatherModels model);
        WeatherModels GetByName(string name);
        WeatherModels FindNewByName(string name);
        List<WeatherModels> GetAll();
    }
    public class WeatherRepository : IWeatherRepository
    {
        static List<WeatherModels> _object = new List<WeatherModels>(); //nesneleri saklıcaz static nesnede

        private string baseurl = "https://api.openweathermap.org/data/2.5/";
        private string apiId;

        private WeatherModels getWeather(string city) //şehirin havadurumunu çekicez
        {
            string url = baseurl + "weather?q=" + city + "&appid=" + apiId +"&lang=tr" ; //api için istek url si
            ServicePointManager.Expect100Continue = false; //https den devam et yanıtı
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //guvenlık protokolu
            WeatherModels weather = new WeatherModels(); 

            try   
            {
                string html;
                using (WebClient client = new WebClient()) //webClient url den veri indirmeye yarar
                {
                    html = client.DownloadString(url);
                    weather = JsonConvert.DeserializeObject<WeatherModels>(html); //json verisini weather model nesnesine donusturur
                }
            }
            catch (WebException ex) //hata yakalar
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound) // HTTP 404
                    {
                    }
                }
            }

            return weather!;
        }

        public WeatherRepository(string api) //api anahtarını alır
        {
            apiId = api;
        }

        public WeatherRepository() //apiid kullanır
        {
            apiId = "7292f194914dcf14d3d7c87d2eed44d7";
        }

        public void Add(WeatherModels model) 
        {
           if( _object.FindIndex(x => x.id == model.id)==-1) //listede yoksa ekler
            _object.Add(model);//nesneyı lısteye ekler
        }

        public void Delete(WeatherModels model) //listeden siler
        {
            _object.Remove(model);
        }

        public List<WeatherModels> GetAll() //tüm nesnelerın lıstesını dondurur
        {
            return _object;
        }

        public WeatherModels FindNewByName(string name) //girilen şehrin apisini çeker
        {
            return getWeather(name);
        }
        public WeatherModels GetByName(string name) //listeden döndürür
        {
            WeatherModels result = _object.Find(x => x.name == name);
            return result!;
        }
        public void Update(WeatherModels model)
        {
            WeatherModels result = _object.Find(x => x.id == model.id); //güncellencek modeli bulur
            _object.Remove(result); //eskı modelı cıkarır 
            _object.Add(model); //guncellenmısı ekler
        }
    }
}



