using lib_dominio.Entidades;

namespace presentaciones.Interfaces
{
    public interface INivelesPresentacion
    {
        void ponerToken(string token);

        Task<Niveles?> PorId(int Id);
        Task<List<Niveles>> PorNombre(string nombre);
        Task<List<Niveles>> Listar();
        Task<Niveles?> Guardar(Niveles? entidad);
        Task<Niveles?> Modificar(Niveles? entidad);
        Task<Niveles?> Borrar(int Id);
    }
}
