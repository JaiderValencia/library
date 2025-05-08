using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Documento { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public int TipoDocumento { get; set; }

        [ForeignKey("TipoDocumento")]public TiposDocumentos? _TipoDocumento { get; set; }
    }
}
