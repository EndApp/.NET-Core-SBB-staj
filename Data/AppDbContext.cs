using Microsoft.EntityFrameworkCore;
using Staj.Web.Models;

namespace Staj.Web.Data
{
    public class AppDbContext : DbContext //veritabanı ilişkisi
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {//veritabanı bılgılerını ıcerır
        }

        public DbSet<ToDoModel> ToDo { get; set; } //tablodakı her bır satırı temsıl eder
    }
}
