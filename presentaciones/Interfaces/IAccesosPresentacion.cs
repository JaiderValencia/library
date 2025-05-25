using lib_dominio.Entidades;

namespace presentaciones.Interfaces
{
    public interface IAccesosPresentacion
    {
        Task<Accesos?> PorId(int Id);
        Task<List<Accesos>?> PorNombre(string nombre);
        Task<List<Accesos>> Listar();
        Task<Accesos?> Guardar(Accesos? entidad);
        Task<Accesos?> Modificar(Accesos? entidad);
        Task<Accesos?> Borrar(int Id);
    }
}
