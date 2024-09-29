using System.Collections.Generic;
using Staj.Web.Models; //models klasörundeki sınıfları kullanma

namespace Staj.Web.Repositories //namespaceler sınıf ve diğerlerini organize eder
{
    //interfaceler kodda bağımlılığı azaltır ve test edilebilir kod yazdırmamızı sağlar
    public interface IToDoRepository  //sınıfların uygulaması gereken metodları belirler
    {
        List<ToDoModel> GetAll(); //To do models deki tüm nesneleri döndürür liste turunde
        ToDoModel? GetById(int id); //id ye sahıp nesneleri ? null olabılen demek
        void Add(ToDoModel todo); //to do model nesnesını ekler
        void Update(ToDoModel todo); // gunceller
        void Delete(int id); //siler
    }
}
