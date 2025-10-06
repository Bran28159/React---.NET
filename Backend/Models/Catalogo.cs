using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebFinal.Models
{
    [Table("iii_sexo")]
    public class Sexo
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("v_departamento")]
    public class Departamento
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("vi_ciudad")]
    public class Ciudad
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("vii_facultad")]
    public class Facultad
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("viii_carrera")]
    public class Carrera
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("x_matricula")]
    public class Matricula
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("xi_becado")]
    public class Becado
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("xii")]
    public class Xii
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("xiii")]
    public class Xiii
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("xiv")]
    public class Xiv
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("xv")]
    public class Xv
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }

    [Table("xvi")]
    public class Xvi
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }


    [Table("xvii")]
    public class Xvii
    {
        [Key]
        [Column("clave")]
        public int Clave { get; set; }

        [Column("valor")]
        public string? Valor { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();

    }
}