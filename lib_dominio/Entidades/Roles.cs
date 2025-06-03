using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Roles
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }

        [NotMapped] public int CantidadPermisos { get; set; }
        [NotMapped] public List<int>? Accesos { get; set; }

        [NotMapped] public List<string>? accionesRolAccesos { get; set; }
    }
}
