using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    public interface IRolesAplicacion
    {
        Roles? PorId(int Id);
        List<Roles>? PorNombre(string nombre);
        List<Roles> Listar();
        Roles? Guardar(Roles? entidad);
        Roles? Modificar(Roles? entidad);
        Roles? Borrar(int Id);
    }
}
