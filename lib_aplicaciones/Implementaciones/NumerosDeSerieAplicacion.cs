using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class NumerosDeSerieAplicacion: INumerosDeSerieAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<NumerosDeSerie>? PorNumeroSerie(string NumeroSerie)
        {
            return this.conexion!.NumerosDeSerie!
                .Include(NumeroSerie => NumeroSerie._Libro)
                .Where(x => x.NumeroSerie!.Contains(NumeroSerie))
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
            return this.conexion!.NumerosDeSerie!.Take(20).ToList();
        }

        public NumerosDeSerie? PorId(int Id)
        {
            return this.conexion!.NumerosDeSerie!.FirstOrDefault(x => x.Id==Id);
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
