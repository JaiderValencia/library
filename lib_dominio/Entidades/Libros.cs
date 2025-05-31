using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Libros
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public int Categoria { get; set; }
        public int Autor { get; set; }

        [NotMapped] public string estanterias { get; set; } = string.Empty;

        [NotMapped] public string niveles { get; set; } = string.Empty;

        [ForeignKey("Categoria")] public Categorias? _Categoria { get; set; }
        [ForeignKey("Autor")] public Autores? _Autor { get; set; }
    }
}
