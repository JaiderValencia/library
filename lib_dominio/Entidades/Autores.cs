﻿using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Autores
    {
        [Key] public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}
