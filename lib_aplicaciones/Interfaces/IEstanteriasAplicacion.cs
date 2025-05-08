using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    internal interface IEstanteriasAplicacion
    {
        public List<Estanterias> Listar();

        public Estanterias? PorId(int Id);

        public Estanterias? PorNombre(string nombre);

        public Estanterias? Guardar(Estanterias? entidad);

        public Estanterias? Modificar(Estanterias? entidad);

        public Estanterias? Borrar(Estanterias entidad);
    }
}
