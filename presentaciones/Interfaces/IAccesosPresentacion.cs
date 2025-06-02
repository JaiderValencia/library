using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IAccesosPresentacion
    {
        void ponerToken(string token);

        Task<List<Accesos>> Listar();
    }
}
