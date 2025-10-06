
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebFinal.Models;
public class Usuario
{
    [Key]
    public int idusuario { get; set; }

    public string nombre { get; set; } = string.Empty;
    public string login { get; set; } = string.Empty;
    public string clave { get; set; } = string.Empty;

    [ForeignKey("Rol")]
    public int idrol { get; set; }

    public Rol? Rol { get; set; } = null!; // Propiedad de navegación
}


