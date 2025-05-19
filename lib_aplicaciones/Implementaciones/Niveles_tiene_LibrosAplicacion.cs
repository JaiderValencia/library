using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class Niveles_tiene_LibrosAplicacion: INiveles_tiene_LibrosAplicacion
    {
        private Conexion conexion = new Conexion();

        public Niveles_tiene_Libros? Borrar(int id)
        {
            var entidad = this.conexion.Niveles_tiene_Libros!.FirstOrDefault(Niveles_tiene_Libros => Niveles_tiene_Libros.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó");

            this.conexion!.Niveles_tiene_Libros!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Niveles_tiene_Libros? Guardar(Niveles_tiene_Libros? entidad)
        {            
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Niveles_tiene_Libros!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Niveles_tiene_Libros> Listar()
        {
            return this.conexion!.Niveles_tiene_Libros!.Take(20).ToList();
        }

        public Niveles_tiene_Libros? PorId(int Id)
        {
            return this.conexion!.Niveles_tiene_Libros!.FirstOrDefault(x => x.Id==Id);
        }

        public Niveles_tiene_Libros? Modificar(Niveles_tiene_Libros? entidad)


        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad.Libro = 2;

            var entry = this.conexion!.Entry<Niveles_tiene_Libros>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
