using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    interface IPrestamosAplicacion
    {       
        Prestamos? PorId(int Id);
        List<Prestamos>? PorFechaInicio(DateTime FechaInicio);
        List<Prestamos> Listar();
        Prestamos? Guardar(Prestamos? entidad);
        Prestamos? Modificar(Prestamos? entidad);
        Prestamos? Borrar(Prestamos entidad);
    }
}
