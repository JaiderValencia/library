using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IRolesPresentacion
    {
        void ponerToken(string token);
        
        Task<Roles?> PorId(int Id);
        Task<List<Roles>?> PorNombre(string nombre);
        Task<List<Roles>> Listar();
        Task<Roles?> Guardar(Roles entidad);
        Task<Roles?> Modificar(Roles entidad);
        Task<Roles?> Borrar(int Id);
    }
}
