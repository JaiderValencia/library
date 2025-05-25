using lib_dominio.Entidades;

namespace presentaciones.Interfaces
{
    public interface IRoles_tiene_AccesosPresentacion
    {
        Task<Roles_tiene_Accesos?> PorId(int Id);
        Task<List<Roles_tiene_Accesos>> Listar();
        Task<Roles_tiene_Accesos?> Guardar(Roles_tiene_Accesos? entidad);
        Task<Roles_tiene_Accesos?> Modificar(Roles_tiene_Accesos? entidad);
        Task<Roles_tiene_Accesos?> Borrar(int Id);
    }
}
