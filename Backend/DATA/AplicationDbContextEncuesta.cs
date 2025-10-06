using ProyectoWebFinal.Models;
using ProyectoWebFinal.DATA;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContextEncuesta : DbContext
{
    public ApplicationDbContextEncuesta(DbContextOptions<ApplicationDbContextEncuesta> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Sexo)
            .WithMany(s => s.Respuestas)
            .HasForeignKey(r => r.SexoClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Departamento)
            .WithMany(d => d.Respuestas)
            .HasForeignKey(r => r.DepartamentoClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Ciudad)
            .WithMany(c => c.Respuestas)
            .HasForeignKey(r => r.CiudadClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Facultad)
            .WithMany(f => f.Respuestas)
            .HasForeignKey(r => r.FacultadClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Carrera)
            .WithMany(c => c.Respuestas)
            .HasForeignKey(r => r.CarreraClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Matricula)
            .WithMany(m => m.Respuestas)
            .HasForeignKey(r => r.MatriculaClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Becado)
            .WithMany(b => b.Respuestas)
            .HasForeignKey(r => r.BecadoClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Xii)
            .WithMany(x => x.Respuestas)
            .HasForeignKey(r => r.XiiClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Xiii)
            .WithMany(x => x.Respuestas)
            .HasForeignKey(r => r.XiiiClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Xiv)
            .WithMany(x => x.Respuestas)
            .HasForeignKey(r => r.XivClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Xv)
            .WithMany(x => x.Respuestas)
            .HasForeignKey(r => r.XvClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Xvi)
            .WithMany(x => x.Respuestas)
            .HasForeignKey(r => r.XviClave)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Xvii)
            .WithMany(x => x.Respuestas)
            .HasForeignKey(r => r.XviiClave)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Respuesta> respuestas { get; set; }
    public DbSet<iii_sexo> sexos { get; set; }
    public DbSet<v_departamento> departamentos { get; set; }
    public DbSet<vi_ciudad> ciudades { get; set; }
    public DbSet<vii_facultad> facultades { get; set; }
    public DbSet<viii_carrera> carreras { get; set; }
    public DbSet<x_matricula> matriculas { get; set; }
    public DbSet<xi_becado> becados { get; set; }
    public DbSet<XII> xiis { get; set; }
    public DbSet<XIII> xiiis { get; set; }
    public DbSet<XIV> xivs { get; set; }
    public DbSet<XV> xvs { get; set; }
    public DbSet<XVI> xvis { get; set; }
    public DbSet<XVII> xviis { get; set; }
}
