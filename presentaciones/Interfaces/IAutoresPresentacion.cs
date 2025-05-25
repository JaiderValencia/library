using lib_dominio.Entidades;

namespace presentaciones.Interfaces
{
    public interface IAutoresPresentacion
    {
        Task<Autores?> PorId(int Id);
        Task<List<Autores>?> PorNombre(string nombre);
        Task<List<Autores>> Listar();
        Task<Autores?> Guardar(Autores? entidad);
        Task<Autores?> Modificar(Autores? entidad);
        Task<Autores?> Borrar(int Id);
    }
}
