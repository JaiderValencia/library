using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace lib_aplicaciones.Implementaciones
{
    public class NumerosDeSerieAplicacion : INumerosDeSerieAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<NumerosDeSerie>? PorNumeroSerie(string NumeroSerie)
        {

            var numerosSerie = this.conexion!.NumerosDeSerie!
                .Include(NumeroSerie => NumeroSerie._Libro)
                .Where(x => x.NumeroSerie!.Contains(NumeroSerie))
                .ToList();

            numerosSerie.ForEach(x =>
            {
                var nivel_tiene_libros = this.conexion!.Niveles_tiene_Libros!.Include(n => n._Nivel!._Estanteria).Where(n => n.Libro == x._Libro!.Id).ToList();

                x.niveles = string.Join(", ", nivel_tiene_libros.
                    Select(n => n._Nivel!.Nombre));

                x.estanterias = string.Join(", ", nivel_tiene_libros.
                    Select(n => n._Nivel!._Estanteria!.Nombre));
            });

            return numerosSerie;
        }

        public List<NumerosDeSerie>? DisponiblePrestar()
        {
            return this.conexion!.NumerosDeSerie!.
                Include(NumeroSerie => NumeroSerie._Libro)
                .Where(x => this.conexion!.Prestamos!.OrderByDescending(p => p.Id).FirstOrDefault(p => p.NumeroSerie == x.Id) == null || this.conexion!.Prestamos!.OrderByDescending(p => p.Id).FirstOrDefault(p => p.NumeroSerie == x.Id)!.FechaEntregado != null)
                .ToList();
        }

        public NumerosDeSerie? Borrar(int id)
        {
            var entidad = this.conexion.NumerosDeSerie!.FirstOrDefault(NumerosDeSerie => NumerosDeSerie.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó este nivel");

            this.conexion!.NumerosDeSerie!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public NumerosDeSerie? Guardar(NumerosDeSerie? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.NumerosDeSerie!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<NumerosDeSerie> Listar()
        {
            var numerosSerie = this.conexion!.NumerosDeSerie!.Include(NumeroSerie => NumeroSerie._Libro).Take(20).ToList();

            numerosSerie.ForEach(x =>
            {
                var nivel_tiene_libros = this.conexion!.Niveles_tiene_Libros!.Include(n => n._Nivel!._Estanteria).Where(n => n.Libro == x._Libro!.Id).ToList();

                x.niveles = string.Join(", ", nivel_tiene_libros.
                    Select(n => n._Nivel!.Nombre));

                x.estanterias = string.Join(", ", nivel_tiene_libros.
                    Select(n => n._Nivel!._Estanteria!.Nombre));
            });

            return numerosSerie;
        }

        public NumerosDeSerie? PorId(int Id)
        {
            var numeroSerie = this.conexion!.NumerosDeSerie!.Include(n=> n._Libro).FirstOrDefault(x => x.Id == Id);

            if (numeroSerie == null)
                return null;

            var nivel_tiene_libros = this.conexion!.Niveles_tiene_Libros!.Include(n => n._Nivel!._Estanteria).Where(n => n.Libro == numeroSerie._Libro!.Id).ToList();

            numeroSerie.niveles = string.Join(", ", nivel_tiene_libros.
                Select(n => n._Nivel!.Nombre));

            numeroSerie.estanterias = string.Join(", ", nivel_tiene_libros.
                Select(n => n._Nivel!._Estanteria!.Nombre));

            return numeroSerie;
        }

        public NumerosDeSerie? Modificar(NumerosDeSerie? entidad)


        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad.NumeroSerie = "Numero de serie cambiado";

            var entry = this.conexion!.Entry<NumerosDeSerie>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
