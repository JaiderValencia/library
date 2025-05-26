using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IClientesPresentacion
    {
        void ponerToken(string token);

        Task<Clientes?> PorId(int id);
        Task<List<Clientes>> PorNombre(string nombre);
        Task<List<Clientes>> PorDocumento(string documento);
        Task<List<Clientes>> Listar(int pagina);
        Task<Clientes?> Guardar(Clientes entidad);
        Task<Clientes?> Modificar(Clientes entidad);
        Task<Clientes?> Borrar(int id);
    }
}
