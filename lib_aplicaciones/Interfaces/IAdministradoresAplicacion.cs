using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    public interface IAdministradoresAplicacion
    {
        Administradores ObtenerUnoNombre(string nobmre);
        Administradores? PorId(int Id);
        List<Administradores>? PorNombre(string nombre);
        List<Administradores> Listar();
        Administradores? Guardar(Administradores? entidad);
        Administradores? Modificar(Administradores? entidad);
        Administradores? Borrar(int Id);
    }
}
