using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    interface IClientesAplicaciones
    {
        List<Clientes> PorId(int Id);
        List<Clientes> PorNombre(string nombre);
        List<Clientes> PorDocumento(string documento);
        List<Clientes> Listar();
        Clientes? Guardar(Clientes? entidad);
        Clientes? Modificar(Clientes? entidad);
        Clientes? Borrar(Clientes entidad);
    }
}
