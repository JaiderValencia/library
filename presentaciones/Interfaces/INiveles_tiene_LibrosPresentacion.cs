using lib_dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_presentaciones.Interfaces
{
    public interface INiveles_tiene_LibrosPresentacion
    {
        Task<Niveles_tiene_Libros?> PorId(int Id);
        Task<List<Niveles_tiene_Libros>> Listar();
        Task<Niveles_tiene_Libros?> Guardar(Niveles_tiene_Libros? entidad);
        Task<Niveles_tiene_Libros?> Modificar(Niveles_tiene_Libros? entidad);
        Task<Niveles_tiene_Libros?> Borrar(int Id);
    }
}
