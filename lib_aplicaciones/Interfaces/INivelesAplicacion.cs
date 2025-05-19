using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    public interface INivelesAplicacion
    {       
        Niveles? PorId(int Id);
        List<Niveles>? PorNombre(string nombre);
        List<Niveles> Listar();
        Niveles? Guardar(Niveles? entidad);
        Niveles? Modificar(Niveles? entidad);
        Niveles? Borrar(int Id);
    }
}
