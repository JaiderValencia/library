﻿using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class NumerosDeSerie
    {
        public int Id { get; set; }
        public string? NumeroSerie { get; set; }
        public int Libro { get; set; }
                
        [NotMapped] public string estanterias { get; set; } = string.Empty;

        [NotMapped] public string niveles { get; set; } = string.Empty;

        [ForeignKey("Libro")] public Libros? _Libro { get; set; }
    }
}
