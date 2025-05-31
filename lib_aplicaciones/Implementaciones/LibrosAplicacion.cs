using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class LibrosAplicacion : ILibrosAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Libros> Listar(int pagina = 1, int tamañoPagina = 20)
        {
            var libros = this.conexion.Libros!
            .Skip((pagina - 1) * tamañoPagina)
            .Take(tamañoPagina)
            .Include(libro => libro._Autor)
            .Include(libro => libro._Categoria)
            .ToList();

            libros.ForEach(libro =>
            {
                var nivel_tiene_libros = this.conexion.Niveles_tiene_Libros!
                    .Include(n => n._Nivel!._Estanteria)
                    .Where(n => n.Libro == libro.Id)
                    .ToList();

                libro.estanterias = string.Join(", ", nivel_tiene_libros
                    .Select(n => n._Nivel!._Estanteria!.Nombre));

                libro.niveles = string.Join(", ", nivel_tiene_libros
                    .Select(n => n._Nivel!.Nombre));
            });

            return libros;
        }

        public Libros? PorId(int Id)
        {
            var libro = this.conexion.Libros!
            .Include(Libro => Libro._Autor)
            .Include(Libro => Libro._Categoria)
            .FirstOrDefault(libro => libro.Id == Id);

            if (libro == null)
                throw new Exception("No existe este libro");

            var nivel_tiene_libros = this.conexion.Niveles_tiene_Libros!
                    .Include(n => n._Nivel!._Estanteria)
                    .Where(n => n.Libro == libro.Id)
                    .ToList();

            libro.niveles = string.Join(", ", nivel_tiene_libros.
                Select(n => n._Nivel!.Nombre));

            libro.estanterias = string.Join(", ", nivel_tiene_libros.
                Select(n => n._Nivel!._Estanteria!.Nombre));

            return libro;
        }

        public List<Libros> PorNombre(string nombre)
        {
            var libros = this.conexion.Libros!
            .Include(Libro => Libro._Autor)
            .Include(Libro => Libro._Categoria)
            .Where(libro => libro.Nombre!.Contains(nombre)).ToList();

            libros.ForEach(libro =>
            {
                var nivel_tiene_libros = this.conexion.Niveles_tiene_Libros!
                    .Include(n => n._Nivel!._Estanteria)
                    .Where(n => n.Libro == libro.Id)
                    .ToList();

                libro.estanterias = string.Join(", ", nivel_tiene_libros
                    .Select(n => n._Nivel!._Estanteria!.Nombre));

                libro.niveles = string.Join(", ", nivel_tiene_libros
                    .Select(n => n._Nivel!.Nombre));
            });

            return libros;
        }

        public Libros Guardar(Libros entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion.Libros!.Add(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }

        public Libros Modificar(Libros entidad)
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

        public Libros Borrar(int id)
        {
            var entidad = this.conexion.Libros!.FirstOrDefault(libro => libro.Id == id);

            if (entidad == null)
                throw new Exception("No existe esta libro");

            this.conexion.Libros!.Remove(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
