using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IAutoresPresentacion
    {
        void ponerToken(string token);

        Task<Autores?> PorId(int Id);
        Task<List<Autores>?> PorNombre(string nombre);
        Task<List<Autores>> Listar();
        Task<Autores?> Guardar(Autores entidad);
        Task<Autores?> Modificar(Autores entidad);
        Task<Autores?> Borrar(int Id);
    }
}
