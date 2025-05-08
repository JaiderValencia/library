using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Implementaciones
{
    class ClientesAplicacion:IClientesAplicaciones
    {
        private Conexion conexion = new Conexion();

        public List<Clientes> Listar()
        {
            return this.conexion.Clientes!.Take(20).ToList();
        }

        public Clientes? PorId(int Id)
        {
            return this.conexion.Clientes!.FirstOrDefault(cliente => cliente.Id == Id);
        }

        public Clientes? PorNombre(string nombre)
        {
            return this.conexion.Clientes!.FirstOrDefault(cliente => cliente.Nombre == nombre);
        }
    }
}
