using Microsoft.EntityFrameworkCore;
using ProyectoWebFinal.DATA; 
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddHttpContextAccessor();

QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5159"); // ⚡ Usa el puerto de tu API
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
// DbContext principal
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});

// DbContext de encuestas
builder.Services.AddDbContext<ApplicationDbContextEncuesta>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("EncuestaConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("EncuestaConnection"))
    );
});


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10); // tiempo de expiración
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Usuario/Login";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
        options.SlidingExpiration = true; // extiende el tiempo si hay actividad
    });



// MVC + Razor Views
builder.Services.AddControllersWithViews();
// HTTP context para sesiones
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();              // 🔹 Session primero
app.UseAuthentication();       // 🔹 Authentication después de Session
app.UseAuthorization();        // 🔹 Authorization después de Authentication
app.UseCors("AllowAll");       

// Middleware de redirección según sesión/rol
app.Use(async (context, next) =>
{
    var usuario = context.Session.GetString("usuario");
    var rol = context.Session.GetInt32("rol");

    if (!string.IsNullOrEmpty(usuario) && rol != null)
    {
        var path = context.Request.Path.Value?.ToLower();
        if (path == "/" || path == "/home" || path == "/home/index")
        {
            string redirectUrl = rol switch
            {
                1 => "/Paginas/Admin",
                2 => "/Paginas/Estudiante",
                3 => "/Paginas/Profesor",
                _ => "/"
            };

            context.Response.Redirect(redirectUrl);
            return; // detener pipeline
        }
    }

    await next();
});

app.UseRouting();

// Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
