using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    interface ICategoriasAplicacion
    {
        Categorias? PorId(int Id);
        List<Categorias>? PorNombre(string nombre);
        List<Categorias> Listar();
        Categorias? Guardar(Categorias? entidad);
        Categorias? Modificar(Categorias? entidad);
        Categorias? Borrar(Categorias entidad);
    }
}
