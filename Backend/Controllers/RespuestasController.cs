using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebFinal.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using ProyectoWebFinal.DTOs;

namespace ProyectoWebFinal.Controllers
{
    public class RespuestasController : Microsoft.AspNetCore.Mvc.Controller
    {
 private readonly HttpClient _httpClient;

        public RespuestasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5159");
        }

        // ==========================================
        // 1️⃣ Página principal: Listar todas las respuestas
        // ==========================================
        public async Task<IActionResult> Respuestas()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/RespuestasApi");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var respuestas = JsonSerializer.Deserialize<List<RespuestaDTO>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(respuestas);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = $"❌ Error al conectar con la API: {ex.Message}";
                return View(new List<RespuestaDTO>());
            }
        }

        // ==========================================
        // 2️⃣ Eliminar encuestas
        // ==========================================
        public async Task<IActionResult> RespuestasEliminar()
        {
            var response = await _httpClient.GetAsync("api/RespuestasApi");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<RespuestaDTO>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarEliminar(int numero)
        {
            var response = await _httpClient.DeleteAsync($"api/RespuestasApi/{numero}");
            TempData["Mensaje"] = response.IsSuccessStatusCode
                ? "✅ Encuesta eliminada correctamente"
                : "❌ Error al eliminar la encuesta";

            return RedirectToAction("RespuestasEliminar");
        }

        // ==========================================
        // 3️⃣ Editar encuestas
        // ==========================================
        public async Task<IActionResult> RespuestasEditar()
        {
            var response = await _httpClient.GetAsync("api/RespuestasApi");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<RespuestaDTO>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> RespuestasEditar(RespuestaDTO respuesta)
        {
            var json = JsonSerializer.Serialize(respuesta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/RespuestasApi/{respuesta.Numero}", content);
            TempData["Mensaje"] = response.IsSuccessStatusCode
                ? "✅ Encuesta actualizada correctamente"
                : "❌ Error al actualizar la encuesta";

            return RedirectToAction("RespuestasEditar");
        }

        // ==========================================
        // 4️⃣ Exportación
        // ==========================================
        public async Task<IActionResult> RespuestasExportar()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/RespuestasApi");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var respuestasExportar = JsonSerializer.Deserialize<List<RespuestaDTO>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(respuestasExportar);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = $"❌ Error al conectar con la API: {ex.Message}";
                return View(new List<RespuestaDTO>());
            }
        }

        public async Task<IActionResult> ExportarPDF(int id)
        {
            var response = await _httpClient.GetAsync($"api/RespuestasApi/exportarpdf/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "❌ No se pudo generar el PDF";
                return RedirectToAction("RespuestasExportar");
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();
            return File(bytes, "application/pdf", $"Encuesta_{id}.pdf");
        }

        public async Task<IActionResult> ExportarExcel(int id)
        {
            var response = await _httpClient.GetAsync($"api/RespuestasApi/exportarexcel/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "❌ No se pudo generar el Excel";
                return RedirectToAction("RespuestasExportar");
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();
            return File(bytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"Encuesta_{id}.xlsx");
        }

        public async Task<IActionResult> ExportarTodasPDF()
        {
            var response = await _httpClient.GetAsync("api/RespuestasApi/exportartodospdf");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "❌ No se pudo generar el PDF";
                return RedirectToAction("RespuestasExportar");
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();
            return File(bytes, "application/pdf", "Todas_Encuestas.pdf");
        }

        public async Task<IActionResult> ExportarTodasExcel()
        {
            var response = await _httpClient.GetAsync("api/RespuestasApi/exportartodosexcel");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "❌ No se pudo generar el Excel";
                return RedirectToAction("RespuestasExportar");
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();
            return File(bytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Todas_Encuestas.xlsx");
        }
    }
}
