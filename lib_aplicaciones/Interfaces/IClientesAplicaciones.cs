using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    interface IClientesAplicaciones
    {
        Clientes? PorId(int Id);
        Clientes? PorNombre(string nombre);
        Clientes? PorDocumento(string documento);
        List<Clientes> Listar();
        Clientes? Guardar(Clientes? entidad);
        Clientes? Modificar(Clientes? entidad);
        Clientes? Borrar(Clientes entidad);
    }
}
