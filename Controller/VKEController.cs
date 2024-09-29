using Microsoft.AspNetCore.Mvc;
using Staj.Web.Models;

namespace Staj.Web.Controllers
{
    public class VKEController : Controller
    {
        public IActionResult Index() 
        //controllerın varsayılan aksıyonudur genelde cagrıldıgında sayfa render edılır
        {
            return View(); 
        } //View dosyasını render etmek için kullanılan temel bir aksiyon metodu

    [HttpPost]
    public IActionResult Hesapla(VKEModel model)
    {
        if(ModelState.IsValid)//model çalışıyorsa
        {
            model.VKE= model.Kilo/(model.Boy*model.Boy)*10000;           //modelımızdekı VKE degıskenıne bu degerı ata
               //obez olup olmama kontrolu
            if (model.VKE > 0.0 && model.VKE < 18.5)         
                model.VKESonuc= "underweight";
            else if (model.VKE >= 18.5 && model.VKE < 25.0)
                model.VKESonuc= "normal";
            else if (model.VKE >= 25.0 && model.VKE < 30.0)
                model.VKESonuc= "overweight";
            else if (model.VKE >= 30.0)
                model.VKESonuc= "obese";
            else
                model.VKESonuc= "indetermined";
            return Json(new { vke = model.VKE, sonuc = model.VKESonuc });
            //modelımızdekı VKE, sonuc degıskenıne bu degerı ata json dön
        }
        return BadRequest(); //hata alırsa
    }
    }
}