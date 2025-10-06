using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebFinal.Models
{
    [Table("rol")]
    public class Rol
    {
        [Key]
        public int idrol { get; set; }

        [Required]
        [MaxLength(45)]
        public string nombre_rol { get; set; } = string.Empty;

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }

}
