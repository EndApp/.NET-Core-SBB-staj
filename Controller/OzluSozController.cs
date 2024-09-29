using Microsoft.AspNetCore.Mvc;
using Staj.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

public class OzluSozController : Controller
{
    private readonly HttpClient httpClient; //http isteği yapma nesnesı
    private readonly Random random; //rastgele sayı üretme (pdf de istenen)

    public OzluSozController() // nesnelerı baslatır
    {
        httpClient = new HttpClient();
        random = new Random();
    }

    public IActionResult Index() //göruntu dondurur
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> RastgeleSoz() //json döner
    {
        List<OzluSozModel> ozluSozler = await GetRandomSozler(); //özlü söz alır

        if (ozluSozler.Count == 0) //özlü söz listesi boşsa
        {
            return Json("Ozlu Soz Bulunamadı");
        }

        int randomIndex = random.Next(ozluSozler.Count); //özlü sözden rastgele ceker
        return Json(ozluSozler[randomIndex]); //seçilen sözü json dondurur
    }

    private async Task<List<OzluSozModel>> GetRandomSozler() //özlü söz alır 
    {
        List<OzluSozModel> ozluSozler = new List<OzluSozModel>(); //Boş özlü söz listesi oluştur

        try
        {
            string apiUrl = "https://type.fit/api/quotes"; //özlü söz url si
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl); //apiden veri al

            if (response.IsSuccessStatusCode) //api başarılıysa
            {
                var content = await response.Content.ReadAsStringAsync(); //yanıtı string oku
                var quotes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OzluSozModel>>(content); //json içeriği ozlusozmodel lıstesıne donustur

                if (quotes != null) //json bos degılse
                {
                    ozluSozler = quotes.Where(q => q.Text != null).ToList(); //ozlu sozlerı fıltreler textı boş olmayan ve listeler
                }
            }
        }
        catch (Exception ex) //hata
        {
            Console.WriteLine(ex.Message);
        }

        return ozluSozler;
    }
}