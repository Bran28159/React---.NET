using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebFinal.Models;
using ProyectoWebFinal.DATA; // Add this if ApplicationDbContext is in the Data namespace
using Microsoft.EntityFrameworkCore; // Add this for EF Core features like Include and ToListAsync

namespace ProyectoWebFinal.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuarioApiController(ApplicationDbContext context)
        {
            _context = context;
        }

  

[HttpGet]
public async Task<ActionResult<IEnumerable<object>>> GetUsuarios()
{
    if (_context?.usuario == null)
        return NotFound("No se encontró la entidad 'usuario'.");

    // Solo traer usuarios con rol válido para login
    var usuarios = await _context.usuario
        .Where(u => u.idrol == 2 || u.idrol == 3)
        .Select(u => new
        {
            u.idusuario,
            u.nombre,
            u.login,
            u.idrol,
            u.activo
        })
        .ToListAsync();

    return Ok(usuarios);
}

// POST: api/Usuario/login
[HttpPost("login")]
public async Task<ActionResult<object>> Login([FromBody] LoginModel model)
{
    var usuario = await _context.usuario
        .FirstOrDefaultAsync(u => u.login == model.Usuario && u.clave == model.Clave);

    if (usuario == null)
        return Unauthorized("Usuario o clave incorrectos");

    if (!usuario.activo)
        return Unauthorized("Este usuario está desactivado. Contacte al administrador.");

    return Ok(new
    {
        usuario.idusuario,
        usuario.nombre,
        usuario.login,
        usuario.idrol,
        usuario.activo
    });
}

// POST: api/Usuario/registrar
[HttpPost("registrar")]
public async Task<IActionResult> Registrar([FromBody] Usuario dto)
{


    // Verificar si el usuario ya existe
var usuarioExistente = await _context.usuario
    .FirstOrDefaultAsync(u => u.login == dto.login);

if (usuarioExistente != null)
{
    // Determinar el tipo de rol según idrol
    string tipoRol = usuarioExistente.idrol switch
    {
        1 => "Administrador",
        2 => "Profesor",
        3 => "Estudiante",
        _ => "Desconocido"
    };

    return BadRequest($"El usuario ya existe como {tipoRol}.");
}


    var usuario = new Usuario
    {
        nombre = dto.nombre,
        login = dto.login,
        clave = dto.clave,
        idrol = dto.idrol,
        activo = true

    };

    _context.usuario.Add(usuario);
    await _context.SaveChangesAsync();

    await _context.Entry(usuario).Reference(u => u.Rol).LoadAsync();

    return Ok(new
    {
        usuario.idusuario,
        usuario.nombre,
        usuario.login,
        usuario.idrol,
        usuario.activo
    });
}


        [HttpDelete("{id}")]
public async Task<IActionResult> UsuariosEliminar(int id)
{
    try
    {
        var usuario = await _context.usuario.FindAsync(id);
        if (usuario == null)
            return NotFound("Usuario no encontrado");

        _context.usuario.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent(); // 204
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error al eliminar: {ex.Message}");
    }
}


        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.idusuario)
            {
                return BadRequest("El id no coincide con el registro.");
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.usuario.Any(e => e.idusuario == id))
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

[HttpPut("activar/{id}")]
public async Task<IActionResult> ActivarUsuario(int id)
{
    var usuario = await _context.usuario.FindAsync(id);
    if (usuario == null)
        return NotFound("Usuario no encontrado.");

    usuario.activo = true;
    await _context.SaveChangesAsync();
    return Ok("Usuario activado correctamente");
}

[HttpPut("desactivar/{id}")]
public async Task<IActionResult> DesactivarUsuario(int id)
{
    var usuario = await _context.usuario.FindAsync(id);
    if (usuario == null)
        return NotFound("Usuario no encontrado.");

    usuario.activo = false;
    await _context.SaveChangesAsync();
    return Ok("Usuario desactivado correctamente");
}


        public class LoginModel
        {
            public required string Usuario { get; set; }
            public required string Clave { get; set; }
        }
    }
}