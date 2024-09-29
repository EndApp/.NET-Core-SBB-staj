using Microsoft.AspNetCore.Mvc;
using Staj.Web.Models;
using Staj.Web.Repositories;

namespace Staj.Web.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoRepository _toDoRepository; //arayuzun ornegını tutar ToDo verilerini yönetmek için gerekli metodları tanımlar.

        public ToDoController(IToDoRepository toDoRepository)
        {
            _toDoRepository=toDoRepository;
        }
        
         public IActionResult Index()
        {
            var toDoList = _toDoRepository.GetAll(); //to do ogelerıne erıs al
            return View(toDoList); //gorunumu aktar
        }

         [HttpPost]
        public JsonResult Create([FromForm] ToDoModel todo) //to do form verılerını al json don
        {
            if (ModelState.IsValid) //model geçerliyse
            {
                _toDoRepository.Add(todo); //gereklı olanı db ye ekle eklenenı json dondur
                return Json(todo);
            }
            return Json(""); //model gecersızse boş
        }

            [HttpPost]
        public IActionResult ToggleIsDone(int id, bool isDone)
        {
            var todo = _toDoRepository.GetById(id); //id ye göre to do al
            if (todo != null) //oge varsa
            {
                todo.IsDone = isDone; // tik güncelle
                _toDoRepository.Update(todo); //db kaydet
                return Ok(); //işlem başarılı
            }

            
            return BadRequest(); //başarısız
        }

           [HttpPost]
        public IActionResult Delete(int id) //silme
        {
            _toDoRepository.Delete(id); // belirtilen id li işi siler
            return Ok(); //basarılı
        }
    }
}

