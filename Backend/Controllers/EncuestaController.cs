using Microsoft.AspNetCore.Mvc;
using ProyectoWebFinal.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProyectoWebFinal.Controllers
{
    public class EncuestaController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly HttpClient _httpClient;

        public EncuestaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _httpClient.BaseAddress = new Uri("http://localhost:5159"); // Cambia según tu API
        }

        // ==========================================
        // 1️⃣ Formulario vacío para llenar encuesta
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> LlenarEncuesta()
        {
            await CargarCombos();
            return View(new Respuesta());
        }

        [HttpPost]
        public async Task<IActionResult> LlenarEncuesta(Respuesta respuesta)
        {
            if (!ModelState.IsValid)
            {
                await CargarCombos();
                return View(respuesta);
            }

            var json = JsonSerializer.Serialize(respuesta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/EncuestaApi", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Mensaje = "✅ Encuesta guardada correctamente";
                ModelState.Clear();
                await CargarCombos();
                return View(new Respuesta());
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"❌ Error al guardar: {errorContent}");
            await CargarCombos();
            return View(respuesta);
        }

        // ==========================================
        // 2️⃣ Editar encuesta
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> ActualizarEncuesta(int? numero)
        {
            if (numero == null)
                return RedirectToAction("Respuestas", "Respuestas");

            await CargarCombosParaEdicion();

            var response = await _httpClient.GetAsync($"api/EncuestaApi/{numero}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "❌ No se pudo cargar la encuesta";
                return RedirectToAction("Respuestas", "Respuestas");
            }

            var json = await response.Content.ReadAsStringAsync();
            var encuesta = JsonSerializer.Deserialize<Respuesta>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (encuesta == null)
            {
                TempData["Mensaje"] = "❌ Encuesta vacía o inválida";
                return RedirectToAction("Respuestas", "Respuestas");
            }

            return View(encuesta);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarEncuesta(Respuesta respuesta)
        {

            if (!ModelState.IsValid)
            {
                await CargarCombosParaEdicion();
                return View(respuesta);
            }

            var json = JsonSerializer.Serialize(respuesta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/EncuestaApi/{respuesta.Numero}", content);

            await CargarCombosParaEdicion();

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Mensaje = "✅ Encuesta actualizada correctamente";
                return View(respuesta);
            }
            else
            {
                var apiResponseContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"❌ Error al actualizar: {apiResponseContent}");
                return View(respuesta);
            }
        }

        // ==========================================
        // 3️⃣ Combos para los select
        // ==========================================
        private async Task CargarCombos()
        {
            ViewBag.Sexos = await ObtenerCombo("sexos");
            ViewBag.Departamentos = await ObtenerCombo("departamentos");
            ViewBag.Ciudades = await ObtenerCombo("ciudades");
            ViewBag.Facultades = await ObtenerCombo("facultades");
            ViewBag.Carreras = await ObtenerCombo("carreras");
            ViewBag.Matriculas = await ObtenerCombo("matriculas");
            ViewBag.Becados = await ObtenerCombo("becados");
            ViewBag.Xii = await ObtenerCombo("xii");
            ViewBag.Xiii = await ObtenerCombo("xiii");
            ViewBag.Xiv = await ObtenerCombo("xiv");
            ViewBag.Xv = await ObtenerCombo("xv");
            ViewBag.Xvi = await ObtenerCombo("xvi");
            ViewBag.Xvii = await ObtenerCombo("xvii");
        }

        private async Task CargarCombosParaEdicion()
        {
            await CargarCombos();
        }

        private async Task<List<ClaveValor>> ObtenerCombo(string nombre)
        {
            var resp = await _httpClient.GetAsync($"api/EncuestaApi/combos/{nombre}");
            if (!resp.IsSuccessStatusCode) return new List<ClaveValor>();

            var json = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ClaveValor>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ClaveValor>();
        }

        // ==========================================
        // Clase para combos
        // ==========================================
        public class ClaveValor
        {
            public int Clave { get; set; }
            public string? Valor { get; set; }
        }
    }

}