using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Estanterias
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
