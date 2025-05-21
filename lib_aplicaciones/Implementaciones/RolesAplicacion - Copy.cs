using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Roles>? PorNombre(string nombre)
        {
            return this.conexion!.Roles!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public Roles? Borrar(int id)
        {
            var entidad = this.conexion.Roles!.FirstOrDefault(Autor => Autor.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó ese autor");

            this.conexion!.Roles!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Roles? Guardar(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Roles!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Roles> Listar()
        {
            return this.conexion!.Roles!.Take(20).ToList();
        }

        public Roles? PorId(int Id)
        {
            return this.conexion!.Roles!.FirstOrDefault(x => x.Id == Id);
        }

        public Roles? Modificar(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad.Apellido = "Apellido cambiado";

            var entry = this.conexion!.Entry<Roles>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
