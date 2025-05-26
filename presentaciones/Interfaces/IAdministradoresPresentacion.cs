using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IAdministradoresPresentacion
    {        
        void ponerToken(string token);

        Task<string> ObtenerToken(string nombre, string password);

        Task<Administradores?> PorId(int Id);
        Task<List<Administradores>?> PorNombre(string nombre);
        Task<List<Administradores>> Listar();
        Task<Administradores?> Guardar(Administradores entidad);
        Task<Administradores?> Modificar(Administradores entidad);
        Task<Administradores?> Borrar(int Id);
    }
}
