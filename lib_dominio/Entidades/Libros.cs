using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Libros
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public int Categoria { get; set; }
        public int Autor { get; set; }

        [ForeignKey("Categoria")] public Categorias? _Categoria { get; set; }
        [ForeignKey("Autor")] public Autores? _Autor { get; set; }
    }
}
