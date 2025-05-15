using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Administradores
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Password { get; set; }
        public int Role { get; set; }

        [ForeignKey("Role")] public Roles? _Role { get; set; }
    }
}
