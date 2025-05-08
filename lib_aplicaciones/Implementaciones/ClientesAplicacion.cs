using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    class ClientesAplicacion:IClientesAplicaciones
    {
        private Conexion conexion = new Conexion();
       
        public Clientes? PorId(int Id)
        {
            return this.conexion.Clientes!.FirstOrDefault(cliente => cliente.Id == Id);
        }

        public Clientes? PorNombre(string nombre)
        {
            return this.conexion.Clientes!.FirstOrDefault(cliente => cliente.Nombre == nombre);
        }

        public Clientes? PorDocumento(string documento)
        {
            return this.conexion.Clientes!.FirstOrDefault(cliente => cliente.Documento == documento);
        }

        public List<Clientes> Listar()
        {            
            return this.conexion.Clientes!.Take(20).ToList();
        }

        public Clientes? Guardar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.conexion.Clientes!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Clientes? Modificar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad.Nombre = "Nombre cambiado";

            var entry = this.conexion.Clientes!.Entry(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }

        public Clientes? Borrar(Clientes entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.conexion.Clientes!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
