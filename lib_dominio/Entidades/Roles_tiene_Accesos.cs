using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Roles_tiene_Accesos
    {
        [Key] public int Id { get; set; }
        public int Role { get; set; }
        public int Acceso { get; set; }

        public string? Acciones { get; set; }

        [ForeignKey("Role")] public Roles? _Role { get; set; }
        [ForeignKey("Acceso")] public Accesos? _Acceso { get; set; }

        public List<string>? AccionesALista()
        {
            if (string.IsNullOrEmpty(Acciones))
                return null;

            return new List<string>(Acciones.Split(", "));
        }

        public void ListaAacciones(List<string>? Acciones)
        {
            if (Acciones == null || Acciones.Count < 1)
            {
                this.Acciones = string.Empty;
                return;
            }

            this.Acciones = string.Join(", ", Acciones);
        }
    }
}
