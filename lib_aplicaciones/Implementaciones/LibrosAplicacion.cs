using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class LibrosAplicacion : ILibrosAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Libros> Listar()
        {
            return this.conexion.Libros!.Take(20).ToList();
        }

        public Libros? PorId(int Id)
        {
            return this.conexion.Libros!.FirstOrDefault(libro => libro.Id == Id);
        }

        public Libros? PorNombre(string nombre)
        {
            return this.conexion.Libros!.FirstOrDefault(libro => libro.Nombre == nombre);
        }

        public Libros? Guardar(Libros? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.conexion.Libros!.Add(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }

        public Libros? Modificar(Libros? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.conexion.Libros!.Entry(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public Libros? Borrar(Libros entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.conexion.Libros!.Remove(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
