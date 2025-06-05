using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TiendaOnline.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de EF Core
builder.Services.AddDbContext<TiendaOnlineContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ) // Se cerró correctamente el paréntesis aquí
);

// MVC y sesiones
builder.Services.AddControllersWithViews( ); // No se requiere ningún cambio aquí, el error CS1503 no aplica a esta línea

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.WebHost.UseUrls("http://0.0.0.0:8080");
var app = builder.Build( );

// Manejo de errores
if (!app.Environment.IsDevelopment( ))
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles( );

app.UseRouting( );

app.UseSession( );
app.UseAuthentication( );
app.UseAuthorization( );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run( );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run( );
