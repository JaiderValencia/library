using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Prestamos
    { 
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime? FechaEntregado { get; set; }
        public int Cliente { get; set; }
        public int NumeroSerie { get; set; }

        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("NumeroSerie")] public NumerosDeSerie? _NumeroSerie { get; set; }
    }
}
