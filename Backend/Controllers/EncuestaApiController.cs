using Microsoft.AspNetCore.Mvc;
using ProyectoWebFinal.Models;
using ProyectoWebFinal.DATA;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ProyectoWebFinal.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestaApiController : ControllerBase
    {
        private readonly ApplicationDbContextEncuesta _context;

        public EncuestaApiController(ApplicationDbContextEncuesta context)
        {
            _context = context;
        }

        // =========================
        // GET: api/Encuesta/{numero}
        // =========================
        [HttpGet("{numero}")]
        public async Task<IActionResult> GetEncuesta(int numero)
        {
            var encuesta = await _context.respuestas.FindAsync(numero);
            if (encuesta == null)
                return NotFound("Encuesta no encontrada.");

            return Ok(encuesta);
        }

        // =========================
        // GET: api/Encuesta
        // =========================
        [HttpGet]
        public async Task<ActionResult<List<Respuesta>>> GetEncuestas()
        {
            var encuestas = await _context.respuestas.ToListAsync();
            return Ok(encuestas);
        }

        // =========================
        // POST: api/Encuesta
        // =========================
        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostEncuesta([FromBody] Respuesta respuesta)
        {
            if (respuesta == null)
                return BadRequest("No se recibió ninguna respuesta.");

            try
            {
                _context.respuestas.Add(respuesta);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEncuesta), new { numero = respuesta.Numero }, respuesta);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error guardando la encuesta: " + ex.Message);
            }
        }

        // =========================
        // PUT: api/Encuesta/{numero}
        // =========================
       [HttpPut("{numero}")]
public async Task<IActionResult> ActualizarEncuesta(int numero, [FromBody] Respuesta respuesta)
{
    if (respuesta == null || numero != respuesta.Numero)
        return BadRequest("Datos inválidos o número de encuesta no coincide.");

    var encuestaExistente = await _context.respuestas.FindAsync(numero);
    if (encuestaExistente == null)
        return NotFound("Encuesta no encontrada.");

    // Actualizamos los campos manualmente
    encuestaExistente.Nombre = respuesta.Nombre;
    encuestaExistente.Apellido = respuesta.Apellido;
    encuestaExistente.SexoClave = respuesta.SexoClave;
    encuestaExistente.Identidad = respuesta.Identidad;
    encuestaExistente.DepartamentoClave = respuesta.DepartamentoClave;
    encuestaExistente.CiudadClave = respuesta.CiudadClave;
    encuestaExistente.FacultadClave = respuesta.FacultadClave;
    encuestaExistente.CarreraClave = respuesta.CarreraClave;
    encuestaExistente.PreguntaIX = respuesta.PreguntaIX;
    encuestaExistente.MatriculaClave = respuesta.MatriculaClave;
    encuestaExistente.BecadoClave = respuesta.BecadoClave;
    encuestaExistente.XiiClave = respuesta.XiiClave;
    encuestaExistente.XiiiClave = respuesta.XiiiClave;
    encuestaExistente.XivClave = respuesta.XivClave;
    encuestaExistente.XvClave = respuesta.XvClave;
    encuestaExistente.XviClave = respuesta.XviClave;
    encuestaExistente.XviiClave = respuesta.XviiClave;

    try
    {
        await _context.SaveChangesAsync();
        return Ok(encuestaExistente);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Error al actualizar la encuesta: " + ex.Message);
    }
}


        // =========================
        // COMBOS para select
        // =========================
        [HttpGet("combos/{tipo}")]
        public async Task<ActionResult<IEnumerable<object>>> GetCombo(string tipo)
        {
            switch (tipo.ToLower())
            {
                case "sexos":
                    return Ok(await _context.sexos.Select(s => new { s.Clave, s.Valor }).ToListAsync());
                case "departamentos":
                    return Ok(await _context.departamentos.Select(d => new { d.Clave, d.Valor }).ToListAsync());
                case "ciudades":
                    return Ok(await _context.ciudades.Select(c => new { c.Clave, c.Valor }).ToListAsync());
                case "facultades":
                    return Ok(await _context.facultades.Select(f => new { f.Clave, f.Valor }).ToListAsync());
                case "carreras":
                    return Ok(await _context.carreras.Select(c => new { c.Clave, c.Valor }).ToListAsync());
                case "matriculas":
                    return Ok(await _context.matriculas.Select(m => new { m.Clave, m.Valor }).ToListAsync());
                case "becados":
                    return Ok(await _context.becados.Select(b => new { b.Clave, b.Valor }).ToListAsync());
                case "xii":
                    return Ok(await _context.xiis.Select(x => new { x.Clave, x.Valor }).ToListAsync());
                case "xiii":
                    return Ok(await _context.xiiis.Select(x => new { x.Clave, x.Valor }).ToListAsync());
                case "xiv":
                    return Ok(await _context.xivs.Select(x => new { x.Clave, x.Valor }).ToListAsync());
                case "xv":
                    return Ok(await _context.xvs.Select(x => new { x.Clave, x.Valor }).ToListAsync());
                case "xvi":
                    return Ok(await _context.xvis.Select(x => new { x.Clave, x.Valor }).ToListAsync());
                case "xvii":
                    return Ok(await _context.xviis.Select(x => new { x.Clave, x.Valor }).ToListAsync());
                default:
                    return NotFound("Combo no encontrado.");
            }
        }
    }
}
