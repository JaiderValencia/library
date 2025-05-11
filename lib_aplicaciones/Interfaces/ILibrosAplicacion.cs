using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ILibrosAplicacion
    {
        List<Libros> Listar();
        Libros? PorId(int Id);
        Libros? PorNombre(string nombre);
        Libros? Guardar(Libros? entidad);
        Libros? Modificar(Libros? entidad);
        Libros? Borrar(Libros entidad);
    }
}
