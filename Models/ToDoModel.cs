using System.ComponentModel.DataAnnotations;

namespace Staj.Web.Models
{
    public class ToDoModel
    {
        [Key]
        public int Id {get; set;}
        public string? Description {get; set;}
        public bool IsDone {get; set;}
    }
}