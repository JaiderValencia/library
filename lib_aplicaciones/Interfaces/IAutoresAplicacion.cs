using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    public interface IAutoresAplicacion
    {
        Autores? PorId(int Id);
        List<Autores>? PorNombre(string nombre);
        List<Autores> Listar();
        Autores? Guardar(Autores? entidad);
        Autores? Modificar(Autores? entidad);
        Autores? Borrar(Autores entidad);
    }
}
