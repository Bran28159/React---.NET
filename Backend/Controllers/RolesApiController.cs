using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoWebFinal.Models;
using ProyectoWebFinal.DATA;
using Microsoft.EntityFrameworkCore;

namespace ProyectoWebFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RolesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetRoles()
        {
            if (_context?.rol == null)
                return NotFound("No se encontró la entidad 'rol'.");

            // Solo devolver el nombre del rol
            var roles = await _context.rol
    .Select(r => new { r.idrol, r.nombre_rol })
    .ToListAsync();


            return Ok(roles);
        }

        // POST: api/RolesApi
        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            _context.rol.Add(rol);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoles), new { id = rol.idrol }, rol);
        }
    }
}
