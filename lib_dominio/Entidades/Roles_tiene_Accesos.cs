using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Roles_tiene_Accesos
    {
        [Key] public int Id { get; set; }
        public int Role { get; set; }
        public int Acceso { get; set; }

        [ForeignKey("Role")] public Roles? _Role { get; set; }
        [ForeignKey("Acceso")] public Accesos? _Acceso { get; set; }
    }
}
