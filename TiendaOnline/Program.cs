using Microsoft.EntityFrameworkCore;
using TiendaOnline.Data; // Asegúrate que este namespace sea el correcto

var builder = WebApplication.CreateBuilder(args);

// 🔧 Agregar cadena de conexión desde appsettings.json
builder.Services.AddDbContext<TiendaOnlineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
