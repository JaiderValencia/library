using lib_dominio.Entidades;

namespace presentaciones.Interfaces
{
    public interface IPrestamosPresentacion
    {
        void ponerToken(string token);

        Task<Prestamos?> PorId(int Id);
        Task<List<Prestamos>?> PorFechaInicio(DateTime FechaInicio);
        Task<List<Prestamos>> Listar();
        Task<Prestamos?> Guardar(Prestamos? entidad);
        Task<Prestamos?> Modificar(Prestamos? entidad);
        Task<Prestamos?> Borrar(int Id);
    }
}
