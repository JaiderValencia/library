using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IClientesAplicaciones
    {
        Clientes? PorId(int Id);
        List<Clientes> PorNombre(string nombre);
        List<Clientes> PorDocumento(string documento);
        List<Clientes> Listar(int pagina, int tamañoPagina);
        Clientes? Guardar(Clientes entidad);
        Clientes? Modificar(Clientes entidad);
        Clientes? Borrar(int id);
    }
}
