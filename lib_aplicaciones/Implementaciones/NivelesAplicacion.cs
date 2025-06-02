using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class NivelesAplicacion : INivelesAplicacion
    {
        private Conexion conexion = new Conexion();
        private Niveles_tiene_LibrosAplicacion nivelesLibrosApp = new Niveles_tiene_LibrosAplicacion();

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

        private void guardarLibroEnNivel(List<int> libros, int nivelId)
        {
            var nivelLibros = this.nivelesLibrosApp.PorNivel(nivelId);

            if (libros == null && nivelLibros != null && nivelLibros.Count > 0)
            {
                foreach (var nivelLibro in nivelLibros)
                {
                    this.conexion!.Niveles_tiene_Libros!.Remove(nivelLibro);
                }
                this.conexion.SaveChanges();

                return;
            }

            if (nivelLibros != null && libros != null)
            {
                foreach (var nivelLibro in nivelLibros)
                {
                    if (!libros.Contains(nivelLibro.Libro))
                    {
                        this.conexion!.Niveles_tiene_Libros!.Remove(nivelLibro);
                    }
                }
            }

            if (libros != null)
            {
                foreach (var libroId in libros)
                {
                    if (nivelLibros != null && nivelLibros.Any(x => x.Libro == libroId && x.Nivel == nivelId))
                        continue;

                    var nivelTieneLibro = new Niveles_tiene_Libros
                    {
                        Libro = libroId,
                        Nivel = nivelId
                    };

                    this.conexion.Niveles_tiene_Libros!.Add(nivelTieneLibro);
                }
            }

            this.conexion.SaveChanges();
        }

        public Niveles? Guardar(Niveles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Niveles!.Add(entidad);
            this.conexion.SaveChanges();

            if (entidad.Libros != null && entidad.Libros.Count() != 0)
                this.guardarLibroEnNivel(entidad.Libros!, entidad.Id);

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

            nivel.Libros = this.conexion!.Niveles_tiene_Libros!
                .Where(x => x.Nivel == nivel.Id)
                .Select(x => x.Libro)
                .ToList();

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

            this.guardarLibroEnNivel(entidad.Libros!, entidad.Id);

            return entidad;
        }
    }
}
