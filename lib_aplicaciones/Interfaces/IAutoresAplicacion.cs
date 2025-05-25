using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IAutoresAplicacion
    {
        Autores? PorId(int Id);
        List<Autores>? PorNombre(string nombre);
        List<Autores> Listar();
        Autores? Guardar(Autores? entidad);
        Autores? Modificar(Autores? entidad);
        Autores? Borrar(int Id);
    }
}
