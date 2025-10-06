using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebFinal.Models
{
    [Table("iii_sexo")]
    public class iii_sexo
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("v_departamento")]
    public class v_departamento
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("vi_ciudad")]
    public class vi_ciudad
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("vii_facultad")]
    public class vii_facultad
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("viii_carrera")]
    public class viii_carrera
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("x_matricula")]
    public class x_matricula
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("xi_becado")]
    public class xi_becado
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("xii")]
    public class XII
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("xiii")]
    public class XIII
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("xiv")]
    public class XIV
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("xv")]
    public class XV
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("xvi")]
    public class XVI
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }

    [Table("xvii")]
    public class XVII
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string Valor { get; set; } = string.Empty;

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new HashSet<Respuesta>();
    }
}
