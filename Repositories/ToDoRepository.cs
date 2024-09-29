using System.Collections.Generic;
using System.Linq;
using Staj.Web.Models; //to do model ıcın
using Staj.Web.Data; //appdbcontext ıcın

namespace Staj.Web.Repositories
{   
    
    public class ToDoRepository : IToDoRepository //to do repository sınıfının ITODOREPOSitory sini implement eder
    {
        private readonly AppDbContext _context; //1 kez atanır

        public ToDoRepository(AppDbContext context) //veritabanı baglantısını saklar
        {
            _context = context;
        }

        public List<ToDoModel> GetAll() //to do tablosundakı tum verılerı to list metoduyla listeye donusturur
        {
            return _context.ToDo.ToList();
        }

        public ToDoModel? GetById(int id) //id alır ve id ye ait todo model nesnesını dondurur
        {
            return _context.ToDo.Find(id); //id yi veritabanında arar ve döndurur
        }

        public void Add(ToDoModel todo) //to do model alıp nesneyı db ye ekler
        {
            _context.ToDo.Add(todo); //veritabanına ekler
            _context.SaveChanges(); //kaydeder
        }

        public void Update(ToDoModel todo) //günceller
        {
            _context.ToDo.Update(todo);
            _context.SaveChanges();
        }

        public void Delete(int id) //belirli idliyi siler
        {
            var todo = _context.ToDo.Find(id); //id yi to do model nesnesınde arar
            if (todo != null) //boş değilse
            {
                _context.ToDo.Remove(todo); //dbden kaldırır
                _context.SaveChanges(); //kaydeder
            }
        }
    }
}
