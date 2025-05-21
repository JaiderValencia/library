using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    public interface IRoles_tiene_AccesosAplicacion
    {
        Roles_tiene_Accesos? PorId(int Id);
        List<Roles_tiene_Accesos> Listar();
        Roles_tiene_Accesos? Guardar(Roles_tiene_Accesos? entidad);
        Roles_tiene_Accesos? Modificar(Roles_tiene_Accesos? entidad);
        Roles_tiene_Accesos? Borrar(int Id);
    }
}
