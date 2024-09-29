// Controllers/HomeController.cs

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Staj.Web.Models;

namespace Staj.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() //controllerın action(eylem) metodu farklı turde dondurmeye yarar
        {
            // Örnek uygulama listesi oluştur
            var appList = new List<AppModel> //Uygulama Listesi
            {   new AppModel{Id = 1, Ad = "ToDo App",Aciklama="Yapilacaklar Listesi ",Fotograf="/resimler/todo.png",Url = "todo"},
                new AppModel{Id = 2, Ad = "Ozlu Soz",Aciklama="Rastgele Özlü Söz ",Fotograf="/resimler/ozlu_soz.png" ,Url = "OzluSoz"},
                new AppModel{Id = 3, Ad = "Hava Durumu",Aciklama="Hava Durumu ",Fotograf="/resimler/weather.png",Url = "Weather"},
                new AppModel{Id = 4, Ad = "VKE Hesaplama",Aciklama="Vücut Kitle Endeksi ", Fotograf = "/resimler/bmi.png",Url = "VKE"}
                // Daha fazla uygulama eklenebilir pdf de istenildiği gibi
            };

            return View(appList); //app list listesi dinamik görünümü döndürür
        }
    }
}
