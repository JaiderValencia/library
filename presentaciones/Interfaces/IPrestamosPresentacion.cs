using lib_dominio.Entidades;

namespace presentaciones.Interfaces
{
    public interface IPrestamosPresentacion
    {
        Task<Prestamos?> PorId(int Id);
        Task<List<Prestamos>?> PorFechaInicio(DateTime FechaInicio);
        Task<List<Prestamos>> Listar();
        Task<Prestamos?> Guardar(Prestamos? entidad);
        Task<Prestamos?> Modificar(Prestamos? entidad);
        Task<Prestamos?> Borrar(int Id);
    }
}
