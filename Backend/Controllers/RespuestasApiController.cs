using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebFinal.Models;
using ProyectoWebFinal.DATA;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ProyectoWebFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestasApiController : ControllerBase
    {
        private readonly ApplicationDbContextEncuesta _context;

        public RespuestasApiController(ApplicationDbContextEncuesta context)
        {
            _context = context;
        }

        // GET: api/Respuestas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetRespuestas()
        {
            var respuestas = await _context.respuestas
                .Include(r => r.Sexo)
                .Include(r => r.Departamento)
                .Include(r => r.Ciudad)
                .Include(r => r.Facultad)
                .Include(r => r.Carrera)
                .Include(r => r.Matricula)
                .Include(r => r.Becado)
                .Include(r => r.Xii)
                .Include(r => r.Xiii)
                .Include(r => r.Xiv)
                .Include(r => r.Xv)
                .Include(r => r.Xvi)
                .Include(r => r.Xvii)
                .Select(r => new
                {
                    r.Numero,
                    r.Nombre,
                    r.Apellido,
                    Sexo = r.Sexo != null ? r.Sexo.Valor : null,
                    r.Identidad,
                    Departamento = r.Departamento != null ? r.Departamento.Valor : null,
                    Ciudad = r.Ciudad != null ? r.Ciudad.Valor : null,
                    Facultad = r.Facultad != null ? r.Facultad.Valor : null,
                    Carrera = r.Carrera != null ? r.Carrera.Valor : null,
                    r.PreguntaIX,
                    Matricula = r.Matricula != null ? r.Matricula.Valor : null,
                    Becado = r.Becado != null ? r.Becado.Valor : null,
                    Xii = r.Xii != null ? r.Xii.Valor : null,
                    Xiii = r.Xiii != null ? r.Xiii.Valor : null,
                    Xiv = r.Xiv != null ? r.Xiv.Valor : null,
                    Xv = r.Xv != null ? r.Xv.Valor : null,
                    Xvi = r.Xvi != null ? r.Xvi.Valor : null,
                    Xvii = r.Xvii != null ? r.Xvii.Valor : null
                })
                .ToListAsync();

            return Ok(respuestas);
        }

        // GET: api/Catalogo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetRespuestas(int id)
        {
            var respuesta = await _context.respuestas
                .Include(r => r.Sexo)
                .Include(r => r.Departamento)
                .Include(r => r.Ciudad)
                .Include(r => r.Facultad)
                .Include(r => r.Carrera)
                .Include(r => r.Matricula)
                .Include(r => r.Becado)
                .Include(r => r.Xii)
                .Include(r => r.Xiii)
                .Include(r => r.Xiv)
                .Include(r => r.Xv)
                .Include(r => r.Xvi)
                .Include(r => r.Xvii)
                .Where(r => r.Numero == id)
                .Select(r => new
                {
                    r.Numero,
                    r.Nombre,
                    r.Apellido,
                    Sexo = r.Sexo != null ? r.Sexo.Valor : null,
                    r.Identidad,
                    Departamento = r.Departamento != null ? r.Departamento.Valor : null,
                    Ciudad = r.Ciudad != null ? r.Ciudad.Valor : null,
                    Facultad = r.Facultad != null ? r.Facultad.Valor : null,
                    Carrera = r.Carrera != null ? r.Carrera.Valor : null,
                    r.PreguntaIX,
                    Matricula = r.Matricula != null ? r.Matricula.Valor : null,
                    Becado = r.Becado != null ? r.Becado.Valor : null,
                    Xii = r.Xii != null ? r.Xii.Valor : null,
                    Xiii = r.Xiii != null ? r.Xiii.Valor : null,
                    Xiv = r.Xiv != null ? r.Xiv.Valor : null,
                    Xv = r.Xv != null ? r.Xv.Valor : null,
                    Xvi = r.Xvi != null ? r.Xvi.Valor : null,
                    Xvii = r.Xvii != null ? r.Xvii.Valor : null
                })
                .FirstOrDefaultAsync();

            if (respuesta == null) return NotFound();
            return Ok(respuesta);
        }
         // POST: api/Respuestas/Editar
        [HttpPost]
        public async Task<IActionResult> Edit(Respuesta respuesta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(respuesta);
            }

            try
            {
                _context.Entry(respuesta).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                // Encuesta actualizada correctamente!
                return Ok(new { mensaje = "Encuesta actualizada correctamente!" });
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Error al actualizar la encuesta: " + ex.Message);
                return BadRequest(respuesta);
            }
        }
    

        // POST: api/Respuestas
        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostRespuesta(Respuesta respuesta)
        {
            if (respuesta == null)
                return BadRequest("No se recibió ninguna respuesta.");

            try
            {
                _context.respuestas.Add(respuesta);
                await _context.SaveChangesAsync();

                // Devuelve la respuesta creada con su ID generado
                return CreatedAtAction(nameof(GetRespuestas), new { id = respuesta.Numero }, respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error guardando la encuesta: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RespuestasEliminar(int id)
        {
            var respuesta = await _context.respuestas.FindAsync(id);
            if (respuesta == null)
            {
                return NotFound();
            }

            _context.respuestas.Remove(respuesta);
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }

        // PUT: api/Respuestas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRespuesta(int id, Respuesta respuesta)
        {
            if (id != respuesta.Numero)
            {
                return BadRequest("El id no coincide con el registro.");
            }

            _context.Entry(respuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.respuestas.Any(e => e.Numero == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204
        }




        #region Métodos privados PDF/Excel
        [HttpGet("exportarpdf/{id}")]
        public async Task<IActionResult> ExportarEncuestaPDF(int id)
        {
            var r = await _context.respuestas
                .Include(x => x.Sexo)
                .Include(x => x.Departamento)
                .Include(x => x.Ciudad)
                .Include(x => x.Facultad)
                .Include(x => x.Carrera)
                .Include(x => x.Matricula)
                .Include(x => x.Becado)
                .Include(x => x.Xii)
                .Include(x => x.Xiii)
                .Include(x => x.Xiv)
                .Include(x => x.Xv)
                .Include(x => x.Xvi)
                .Include(x => x.Xvii)
                .FirstOrDefaultAsync(x => x.Numero == id);

            if (r == null) return NotFound();

            using var stream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().Text("Encuesta de Análisis de Sistemas de Información").Bold().FontSize(16);

                    page.Content().Column(col =>
                    {
                        col.Item().Text("Datos Generales:").Bold();

                        col.Item().Text($"Nombres: {r.Nombre}");
                        col.Item().Text($"Apellidos: {r.Apellido}");
                        col.Item().Text($"Sexo: {r.Sexo?.Valor}");
                        col.Item().Text($"Cédula de Identificación: {r.Identidad}");
                        col.Item().Text($"Departamento: {r.Departamento?.Valor}");
                        col.Item().Text($"Ciudad: {r.Ciudad?.Valor}");

                        col.Item().Text("Datos Académicos:").Bold();
                        col.Item().Text($"Facultad: {r.Facultad?.Valor}");
                        col.Item().Text($"Carrera: {r.Carrera?.Valor}");
                        col.Item().Text($"Año de estudios: {r.PreguntaIX}");
                        col.Item().Text($"Tipo de Matrícula: {r.Matricula?.Valor}");
                        col.Item().Text($"Becado: {r.Becado?.Valor}");

                        col.Item().Text("Desarrollo:").Bold();

                        col.Item().Text($"I. Uso del Sistema de Matrícula: {r.Xii?.Valor}");
                        col.Item().Text($"II. Opinión del nuevo sistema: {r.Xiii?.Valor}");
                        col.Item().Text($"III. Accesibilidad del sistema: {r.Xiv?.Valor}");
                        col.Item().Text($"IV. Cómo le ha ayudado: {r.Xv?.Valor}");
                        col.Item().Text($"V. Ahorro de dinero: {r.Xvi?.Valor}");
                        col.Item().Text($"VI. Recomendaciones de mejora: {r.Xvii?.Valor}");
                    });
                });
            }).GeneratePdf(stream);

            return File(stream.ToArray(), "application/pdf", $"Encuesta_{id}.pdf");
        }

        [HttpGet("exportartodospdf")]
        public async Task<IActionResult> ExportarTodasPDFFormatoIndividual()
        {
            var todas = await _context.respuestas
                .Include(x => x.Sexo)
                .Include(x => x.Departamento)
                .Include(x => x.Ciudad)
                .Include(x => x.Facultad)
                .Include(x => x.Carrera)
                .Include(x => x.Matricula)
                .Include(x => x.Becado)
                .Include(x => x.Xii)
                .Include(x => x.Xiii)
                .Include(x => x.Xiv)
                .Include(x => x.Xv)
                .Include(x => x.Xvi)
                .Include(x => x.Xvii)
                .ToListAsync();

            using var stream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4);

                    page.Content().Column(col =>
                    {
                        foreach (var r in todas)
                        {
                            col.Item().Text("Datos Generales:").Bold();

                            col.Item().Text($"Nombres: {r.Nombre}");
                            col.Item().Text($"Apellidos: {r.Apellido}");
                            col.Item().Text($"Sexo: {r.Sexo?.Valor}");
                            col.Item().Text($"Cédula de Identificación: {r.Identidad}");
                            col.Item().Text($"Departamento: {r.Departamento?.Valor}");
                            col.Item().Text($"Ciudad: {r.Ciudad?.Valor}");

                            col.Item().Text("Datos Académicos:").Bold();
                            col.Item().Text($"Facultad: {r.Facultad?.Valor}");
                            col.Item().Text($"Carrera: {r.Carrera?.Valor}");
                            col.Item().Text($"Año de estudios: {r.PreguntaIX}");
                            col.Item().Text($"Tipo de Matrícula: {r.Matricula?.Valor}");
                            col.Item().Text($"Becado: {r.Becado?.Valor}");

                            col.Item().Text("Desarrollo:").Bold();

                            col.Item().Text($"I. Uso del Sistema de Matrícula: {r.Xii?.Valor}");
                            col.Item().Text($"II. Opinión del nuevo sistema: {r.Xiii?.Valor}");
                            col.Item().Text($"III. Accesibilidad del sistema: {r.Xiv?.Valor}");
                            col.Item().Text($"IV. Cómo le ha ayudado: {r.Xv?.Valor}");
                            col.Item().Text($"V. Ahorro de dinero: {r.Xvi?.Valor}");
                            col.Item().Text($"VI. Recomendaciones de mejora: {r.Xvii?.Valor}");

                            // Salto de página para la siguiente encuesta
                            col.Item().PageBreak();
                        }
                    });
                });
            }).GeneratePdf(stream);

            return File(stream.ToArray(), "application/pdf", "Todas_Encuestas_Formato_Individual.pdf");
        }





        [HttpGet("exportarexcel/{id}")]
        public async Task<IActionResult> ExportarExcel(int id)
        {
            var r = await _context.respuestas
                .Include(x => x.Sexo)
                .Include(x => x.Departamento)
                .Include(x => x.Ciudad)
                .Include(x => x.Facultad)
                .Include(x => x.Carrera)
                .Include(x => x.Matricula)
                .Include(x => x.Becado)
                .Include(x => x.Xii)
                .Include(x => x.Xiii)
                .Include(x => x.Xiv)
                .Include(x => x.Xv)
                .Include(x => x.Xvi)
                .Include(x => x.Xvii)
                .FirstOrDefaultAsync(x => x.Numero == id);

            if (r == null) return NotFound();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Encuesta");

            int row = 1;

            ws.Cell(row, 1).Value = "Encuesta de Análisis de Sistemas de Información";
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Range(row, 1, row, 2).Merge();

            row += 2;
            ws.Cell(row, 1).Value = "Datos Generales";
            ws.Cell(row, 1).Style.Font.Bold = true;

            row++;
            ws.Cell(row, 1).Value = "Nombres";
            ws.Cell(row, 2).Value = r.Nombre;

            row++;
            ws.Cell(row, 1).Value = "Apellidos";
            ws.Cell(row, 2).Value = r.Apellido;

            row++;
            ws.Cell(row, 1).Value = "Sexo";
            ws.Cell(row, 2).Value = r.Sexo?.Valor;

            row++;
            ws.Cell(row, 1).Value = "Cédula de Identificación";
            ws.Cell(row, 2).Value = r.Identidad;

            row++;
            ws.Cell(row, 1).Value = "Departamento";
            ws.Cell(row, 2).Value = r.Departamento?.Valor;

            row++;
            ws.Cell(row, 1).Value = "Ciudad";
            ws.Cell(row, 2).Value = r.Ciudad?.Valor;

            row += 2;
            ws.Cell(row, 1).Value = "Datos Académicos";
            ws.Cell(row, 1).Style.Font.Bold = true;

            row++;
            ws.Cell(row, 1).Value = "Facultad";
            ws.Cell(row, 2).Value = r.Facultad?.Valor;

            row++;
            ws.Cell(row, 1).Value = "Carrera";
            ws.Cell(row, 2).Value = r.Carrera?.Valor;

            row++;
            ws.Cell(row, 1).Value = "Año de estudios";
            ws.Cell(row, 2).Value = r.PreguntaIX;

            row++;
            ws.Cell(row, 1).Value = "Tipo de Matrícula";
            ws.Cell(row, 2).Value = r.Matricula?.Valor;

            row++;
            ws.Cell(row, 1).Value = "Becado";
            ws.Cell(row, 2).Value = r.Becado?.Valor;

            row += 2;
            ws.Cell(row, 1).Value = "Desarrollo";
            ws.Cell(row, 1).Style.Font.Bold = true;

            row++;
            ws.Cell(row, 1).Value = "I. Uso del Sistema de Matrícula";
            ws.Cell(row, 2).Value = r.Xii?.Valor;

            row++;
            ws.Cell(row, 1).Value = "II. Opinión del nuevo sistema";
            ws.Cell(row, 2).Value = r.Xiii?.Valor;

            row++;
            ws.Cell(row, 1).Value = "III. Accesibilidad del sistema";
            ws.Cell(row, 2).Value = r.Xiv?.Valor;

            row++;
            ws.Cell(row, 1).Value = "IV. Cómo le ha ayudado";
            ws.Cell(row, 2).Value = r.Xv?.Valor;

            row++;
            ws.Cell(row, 1).Value = "V. Ahorro de dinero";
            ws.Cell(row, 2).Value = r.Xvi?.Valor;

            row++;
            ws.Cell(row, 1).Value = "VI. Recomendaciones de mejora";
            ws.Cell(row, 2).Value = r.Xvii?.Valor;

            // Ajuste automático de columnas
            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Encuesta_{id}.xlsx");
        }



// Exportar todas las encuestas a Excel con formato de ficha individual
[HttpGet("exportartodosexcel")]
    public async Task<IActionResult> ExportarTodasExcel()
    {
        var todas = await _context.respuestas
            .Include(x => x.Sexo)
            .Include(x => x.Departamento)
            .Include(x => x.Ciudad)
            .Include(x => x.Facultad)
            .Include(x => x.Carrera)
            .Include(x => x.Matricula)
            .Include(x => x.Becado)
            .Include(x => x.Xii)
            .Include(x => x.Xiii)
            .Include(x => x.Xiv)
            .Include(x => x.Xv)
            .Include(x => x.Xvi)
            .Include(x => x.Xvii)
            .ToListAsync();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Encuestas");

        int currentRow = 1;
        int row = 1;

            foreach (var r in todas)
        {
                
                ws.Cell(row, 1).Value = "Encuesta de Análisis de Sistemas de Información";
                ws.Cell(row, 1).Style.Font.Bold = true;
                ws.Range(row, 1, row, 2).Merge();

                row += 2;
                ws.Cell(row, 1).Value = "Datos Generales";
                ws.Cell(row, 1).Style.Font.Bold = true;

                row++;
                ws.Cell(row, 1).Value = "Nombres";
                ws.Cell(row, 2).Value = r.Nombre;

                row++;
                ws.Cell(row, 1).Value = "Apellidos";
                ws.Cell(row, 2).Value = r.Apellido;

                row++;
                ws.Cell(row, 1).Value = "Sexo";
                ws.Cell(row, 2).Value = r.Sexo?.Valor;

                row++;
                ws.Cell(row, 1).Value = "Cédula de Identificación";
                ws.Cell(row, 2).Value = r.Identidad;

                row++;
                ws.Cell(row, 1).Value = "Departamento";
                ws.Cell(row, 2).Value = r.Departamento?.Valor;

                row++;
                ws.Cell(row, 1).Value = "Ciudad";
                ws.Cell(row, 2).Value = r.Ciudad?.Valor;

                row += 2;
                ws.Cell(row, 1).Value = "Datos Académicos";
                ws.Cell(row, 1).Style.Font.Bold = true;

                row++;
                ws.Cell(row, 1).Value = "Facultad";
                ws.Cell(row, 2).Value = r.Facultad?.Valor;

                row++;
                ws.Cell(row, 1).Value = "Carrera";
                ws.Cell(row, 2).Value = r.Carrera?.Valor;

                row++;
                ws.Cell(row, 1).Value = "Año de estudios";
                ws.Cell(row, 2).Value = r.PreguntaIX;

                row++;
                ws.Cell(row, 1).Value = "Tipo de Matrícula";
                ws.Cell(row, 2).Value = r.Matricula?.Valor;

                row++;
                ws.Cell(row, 1).Value = "Becado";
                ws.Cell(row, 2).Value = r.Becado?.Valor;

                row += 2;
                ws.Cell(row, 1).Value = "Desarrollo";
                ws.Cell(row, 1).Style.Font.Bold = true;

                row++;
                ws.Cell(row, 1).Value = "I. Uso del Sistema de Matrícula";
                ws.Cell(row, 2).Value = r.Xii?.Valor;

                row++;
                ws.Cell(row, 1).Value = "II. Opinión del nuevo sistema";
                ws.Cell(row, 2).Value = r.Xiii?.Valor;

                row++;
                ws.Cell(row, 1).Value = "III. Accesibilidad del sistema";
                ws.Cell(row, 2).Value = r.Xiv?.Valor;

                row++;
                ws.Cell(row, 1).Value = "IV. Cómo le ha ayudado";
                ws.Cell(row, 2).Value = r.Xv?.Valor;

                row++;
                ws.Cell(row, 1).Value = "V. Ahorro de dinero";
                ws.Cell(row, 2).Value = r.Xvi?.Valor;

                row++;
                ws.Cell(row, 1).Value = "VI. Recomendaciones de mejora";
                ws.Cell(row, 2).Value = r.Xvii?.Valor;

                // Ajuste automático de columnas
                ws.Columns().AdjustToContents();

                // Espacio entre encuestas
                currentRow += 2;
        }

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);

        return File(ms.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "Todas_Encuestas_Formato_Individual.xlsx");
    }



    #endregion
}
}


