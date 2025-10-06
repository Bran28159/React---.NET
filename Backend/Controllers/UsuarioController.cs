using Microsoft.AspNetCore.Mvc;
using ProyectoWebFinal.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using ProyectoWebFinal.Filters;

namespace ProyectoWebFinal.Controllers
{
    public class UsuarioController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly HttpClient _httpClient;

        public UsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5159");
            // // ⚡ el puerto correcto

        }

        [HttpGet]
        public IActionResult Login()
        {
            var rol = HttpContext.Session.GetInt32("rol");

            if (rol != null)
            {
                if (rol == 1) return RedirectToAction("Admin", "Paginas");
                if (rol == 2) return RedirectToAction("Estudiante", "Paginas");
                if (rol == 3) return RedirectToAction("Profesor", "Paginas");
            }

            // También puedes comprobar Cookie si Session expiró:
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            if (User.Identity.IsAuthenticated)
            {
                var rolCookie = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Role).Value);
                if (rolCookie == 1) return RedirectToAction("Admin", "Paginas");
                if (rolCookie == 2) return RedirectToAction("Estudiante", "Paginas");
                if (rolCookie == 3) return RedirectToAction("Profesor", "Paginas");
            }
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Usuario model)
        {
            var json = JsonSerializer.Serialize(new { Usuario = model.login, Clave = model.clave });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/usuarioapi/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<JsonElement>(result);

                int rol = data.GetProperty("idrol").GetInt32();
                int idusuario = data.GetProperty("idusuario").GetInt32();

                // ✅ Guardar en Session
                HttpContext.Session.SetString("usuario", model.login);
                HttpContext.Session.SetInt32("rol", rol);
                HttpContext.Session.SetInt32("idusuario", idusuario);

                // ✅ Guardar en Cookie para login persistente
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.login),
            new Claim(ClaimTypes.Role, rol.ToString())
        };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(
                    "Cookies",
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true, // persiste aunque cierre navegador
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    });

                // Redirigir según rol
                if (rol == 1) return RedirectToAction("Admin", "Paginas");
                if (rol == 2) return RedirectToAction("Estudiante", "Paginas");
                if (rol == 3) return RedirectToAction("Profesor", "Paginas");
            }

            ViewBag.Error = "Usuario o clave incorrectos";
            return View(model);
        }

        // GET: /Usuario/Registrar
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: /Usuario/Registrar
        [HttpPost]
        public async Task<IActionResult> Registrar(Usuario model)
        {
            var json = JsonSerializer.Serialize(new
            {
                nombre = model.nombre,
                login = model.login,
                clave = model.clave,
                idrol = model.idrol
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/usuarioapi/registrar", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            Console.WriteLine($"Rol seleccionado: {model.idrol}");

            ViewBag.Error = "Error al registrar usuario";
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync("Cookies"); // Borra cookie de autenticación
            return RedirectToAction("Login");
        }

    }
}
