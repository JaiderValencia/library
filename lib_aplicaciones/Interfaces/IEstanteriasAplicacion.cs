using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IEstanteriasAplicacion
    {
        public List<Estanterias> Listar(int pagina = 1, int tamañoPagina = 20);

        public Estanterias? PorId(int id);

        public List<Estanterias> PorNombre(string nombre);

        public Estanterias Guardar(Estanterias? entidad);

        public Estanterias Modificar(Estanterias? entidad);

        public Estanterias Borrar(int id);
    }
}
