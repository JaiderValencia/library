using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class ClientesAplicacion : IClientesAplicaciones
    {
        private Conexion conexion = new Conexion();

        public Clientes? PorId(int Id)
        {
            return this.conexion.Clientes!.Include(Cliente => Cliente._TipoDocumento).FirstOrDefault(cliente => cliente.Id == Id);
        }

        public List<Clientes> PorNombre(string nombre)
        {
            return this.conexion.Clientes!.Include(Cliente => Cliente._TipoDocumento).Where(cliente => cliente.Nombre!.Contains(nombre)).ToList();
        }

        public List<Clientes> PorDocumento(string documento)
        {
            return this.conexion.Clientes!.Include(Cliente => Cliente._TipoDocumento).Where(cliente => cliente.Documento!.Contains(documento)).ToList();;
        }

        public List<Clientes> Listar(int pagina = 1, int tamañoPagina = 20)
        {
            return this.conexion.Clientes!
                .Include(Cliente => Cliente._TipoDocumento)
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina).ToList();
        }

        public Clientes? Guardar(Clientes entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion.Clientes!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Clientes? Modificar(Clientes entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            
            var entry = this.conexion.Clientes!.Entry(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }

        public Clientes? Borrar(int id)
        {
            var entidad = this.conexion.Clientes!.FirstOrDefault(Cliente => Cliente.Id == id);

            if (entidad == null)
            {
                throw new Exception("No existe ese cliente");
            }

            this.conexion.Clientes!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
