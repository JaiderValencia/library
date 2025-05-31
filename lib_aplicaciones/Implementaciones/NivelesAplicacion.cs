using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class NivelesAplicacion : INivelesAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Niveles>? PorNombre(string nombre)
        {
            var niveles = this.conexion!.Niveles!
                .Include(Niveles => Niveles._Estanteria)
                .Where(x => x.Nombre!.Contains(nombre))
                .OrderBy(n => n.Estanteria)
                .ToList();

            foreach (var nivel in niveles)
            {
                nivel.CantidadLibros = this.conexion!.Niveles_tiene_Libros!.Count(x => x.Nivel == nivel.Id);
            }

            return niveles;
        }

        public Niveles? Borrar(int id)
        {
            var entidad = this.conexion.Niveles!.FirstOrDefault(Niveles => Niveles.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó este nivel");

            this.conexion!.Niveles!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Niveles? Guardar(Niveles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Niveles!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Niveles> Listar()
        {
            var niveles = this.conexion!.Niveles!
            .Include(Niveles => Niveles._Estanteria)
            .OrderBy(n => n.Estanteria)
            .Take(20)
            .ToList();

            foreach (var nivel in niveles)
            {
                nivel.CantidadLibros = this.conexion!.Niveles_tiene_Libros!.Count(x => x.Nivel == nivel.Id);
            }

            return niveles;
        }

        public Niveles? PorId(int Id)
        {
            var nivel = this.conexion!.Niveles!.FirstOrDefault(x => x.Id == Id);

            if (nivel == null)
                throw new Exception("lbNoSeEncontro");

            nivel.CantidadLibros = this.conexion!.Niveles_tiene_Libros!.Count(x => x.Nivel == nivel.Id);

            return nivel;
        }

        public Niveles? Modificar(Niveles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.conexion!.Entry<Niveles>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
