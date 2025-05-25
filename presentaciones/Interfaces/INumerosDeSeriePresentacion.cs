using lib_dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_presentaciones.Interfaces
{
    public interface INumerosDeSeriePresentacion
    {
        Task<NumerosDeSerie?> PorId(int Id);
        Task<List<NumerosDeSerie>?> PorNumeroSerie(string NumeroSerie);
        Task<List<NumerosDeSerie>> Listar();
        Task<NumerosDeSerie?> Guardar(NumerosDeSerie? entidad);
        Task<NumerosDeSerie?> Modificar(NumerosDeSerie? entidad);
        Task<NumerosDeSerie?> Borrar(int Id);
    }
}
