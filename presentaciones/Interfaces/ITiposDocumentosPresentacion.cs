using lib_dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_presentaciones.Interfaces
{
    public interface ITiposDocumentosPresentacion
    {
        Task<TiposDocumentos?> PorId(int Id);
        Task<List<TiposDocumentos>?> PorNombre(string nombre);
        Task<List<TiposDocumentos>> Listar();
        Task<TiposDocumentos?> Guardar(TiposDocumentos? entidad);
        Task<TiposDocumentos?> Modificar(TiposDocumentos? entidad);
        Task<TiposDocumentos?> Borrar(int Id);
    }
}
