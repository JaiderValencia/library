using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ICategoriasPresentacion
    {
        Task<Categorias?> PorId(int Id);
        Task<List<Categorias>> PorNombre(string nombre);
        Task<List<Categorias>> Listar();
        Task<Categorias?> Guardar(Categorias? entidad);
        Task<Categorias?> Modificar(Categorias? entidad);
        Task<Categorias?> Borrar(int Id);
    }
}
