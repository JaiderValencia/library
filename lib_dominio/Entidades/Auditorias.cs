using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Auditorias
    {
        [Key] public int Id { get; set; }
        public string? Administrador { get; set; }
        public string? Accion { get; set; }

        public DateTime? Fecha { get; set; }
    }
}
