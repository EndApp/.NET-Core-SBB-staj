using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Staj.Web.Data;
using Staj.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

// MVC mimarisini kullanarak controller ve view'ları ekler.
builder.Services.AddControllersWithViews();


//  DbContext
builder.Services.AddDbContext<AppDbContext>(options => //farklu
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")));


//  repository
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); //Uygulama hata işleme
    app.UseHsts(); //HSTS protokolünü etkinleştir
}

app.UseHttpsRedirection(); //HTTP isteklerini HTTPS'ye yönlendirir.
app.UseStaticFiles(); //Statik dosyalar

app.UseRouting(); //Uygulama yönlendirmesi

app.UseAuthorization(); //Yetkilendirme middleware

app.MapControllerRoute( //Varsayılan Home
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();