using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Accesos
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
    }
}
