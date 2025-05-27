using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IEstanteriasPresentacion
    {
        void ponerToken(string token);

        Task<List<Estanterias>> Listar(int pagina = 1);

        Task<Estanterias?> PorId(int Id);

        Task<List<Estanterias>> PorNombre(string nombre);

        Task<Estanterias> Guardar(Estanterias entidad);

        Task<Estanterias> Modificar(Estanterias entidad);

        Task<Estanterias> Borrar(int Id);
    }
}
