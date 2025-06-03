using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    public interface IAccesosAplicacion
    {

        public bool validarAcceso(int AdministradorId, int AccesoId, string ruta);
        Accesos? PorId(int Id);
        List<Accesos>? PorNombre(string nombre);
        List<Accesos> Listar();
        Accesos? Guardar(Accesos? entidad);
        Accesos? Modificar(Accesos? entidad);
        Accesos? Borrar(int Id);
    }
}
