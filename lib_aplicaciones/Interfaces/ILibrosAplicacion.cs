using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ILibrosAplicacion
    {
        List<Libros> Listar(int pagina = 1, int tamañoPagina = 20);
        Libros? PorId(int Id);
        List<Libros> PorNombre(string nombre);
        Libros Guardar(Libros entidad);
        Libros Modificar(Libros entidad);
        Libros Borrar(int id);
    }
}
