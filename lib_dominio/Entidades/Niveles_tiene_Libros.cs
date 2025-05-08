using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Niveles_tiene_Libros
    {
        public int Id { get; set; }
        public int Nivel { get; set; }
        public int Libro { get; set; }

        [ForeignKey("Nivel")] public Niveles? _Nivel { get; set; }
        [ForeignKey("Libro")] public Libros? _Libro { get; set; }
    }
}
