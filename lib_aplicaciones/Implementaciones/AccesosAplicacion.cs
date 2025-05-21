using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class AccesosAplicacion : IAccesosAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Accesos>? PorNombre(string nombre)
        {
            return this.conexion!.Accesos!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public Accesos? Borrar(int id)
        {
            var entidad = this.conexion.Accesos!.FirstOrDefault(Autor => Autor.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó ese autor");

            this.conexion!.Accesos!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Accesos? Guardar(Accesos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Accesos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Accesos> Listar()
        {
            return this.conexion!.Accesos!.Take(20).ToList();
        }

        public Accesos? PorId(int Id)
        {
            return this.conexion!.Accesos!.FirstOrDefault(x => x.Id == Id);
        }

        public Accesos? Modificar(Accesos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad.Apellido = "Apellido cambiado";

            var entry = this.conexion!.Entry<Accesos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
