using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Niveles
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Estanteria { get; set; }

        [ForeignKey("Estanteria")] public Estanterias? _Estanteria { get; set; }
    }
}
