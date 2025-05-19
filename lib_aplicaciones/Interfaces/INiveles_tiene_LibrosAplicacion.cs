using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    public interface INiveles_tiene_LibrosAplicacion
    {       
        Niveles_tiene_Libros? PorId(int Id);
        List<Niveles_tiene_Libros> Listar();
        Niveles_tiene_Libros? Guardar(Niveles_tiene_Libros? entidad);
        Niveles_tiene_Libros? Modificar(Niveles_tiene_Libros? entidad);
        Niveles_tiene_Libros? Borrar(int Id);
        
    }
}
