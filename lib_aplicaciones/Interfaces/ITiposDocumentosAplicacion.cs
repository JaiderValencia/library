using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    public interface ITiposDocumentosAplicacion
    {       
        TiposDocumentos? PorId(int Id);
        List<TiposDocumentos>? PorNombre(string nombre);
        List<TiposDocumentos> Listar();
        TiposDocumentos? Guardar(TiposDocumentos? entidad);
        TiposDocumentos? Modificar(TiposDocumentos? entidad);
        TiposDocumentos? Borrar(int Id);
    }
}
