using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Interfaces
{
    interface INumerosDeSerieAplicacion
    {       
        NumerosDeSerie? PorId(int Id);
        List<NumerosDeSerie>? PorNumeroSerie(string NumeroSerie);
        List<NumerosDeSerie> Listar();
        NumerosDeSerie? Guardar(NumerosDeSerie? entidad);
        NumerosDeSerie? Modificar(NumerosDeSerie? entidad);
        NumerosDeSerie? Borrar(NumerosDeSerie entidad);
    }
}
