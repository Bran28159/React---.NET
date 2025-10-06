using Microsoft.EntityFrameworkCore;
using ProyectoWebFinal.Models; // Add this if Usuario is in the Models namespace

namespace ProyectoWebFinal.DATA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puedes agregar configuraciones adicionales si necesitas
        }

        // Tablas principales
        
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Rol> rol { get; set; }

    }
}
