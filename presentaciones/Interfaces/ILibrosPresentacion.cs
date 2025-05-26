using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ILibrosPresentacion
    {
        void ponerToken(string token);

        Task<List<Libros>> Listar(int pagina = 1);

        Task<Libros?> PorId(int Id);

        Task<List<Libros>> PorNombre(string nombre);
        
        Task<Libros> Guardar(Libros entidad);

        Task<Libros> Modificar(Libros entidad);

        Task<Libros> Borrar(int Id);
    }
}
