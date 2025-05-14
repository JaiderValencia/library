using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class AutoresAplicacion : IAutoresAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Autores>? PorNombre(string nombre)
        {
            return this.conexion!.Autores!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public Autores? Borrar(Autores entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.conexion!.Autores!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Autores? Guardar(Autores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Autores!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Autores> Listar()
        {
            return this.conexion!.Autores!.Take(20).ToList();
        }

        public Autores? PorId(int Id)
        {
            return this.conexion!.Autores!.FirstOrDefault(x => x.Id == Id);
        }

        public Autores? Modificar(Autores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad.Apellido = "Apellido cambiado";

            var entry = this.conexion!.Entry<Autores>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
