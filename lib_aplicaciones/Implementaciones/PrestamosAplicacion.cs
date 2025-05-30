﻿using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class PrestamosAplicacion: IPrestamosAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Prestamos>? PorFechaInicio(DateTime FechaInicio)
        {
            return this.conexion!.Prestamos!
                .Include(Prestamos => Prestamos._Cliente)
                .Include(Prestamos => Prestamos._NumeroSerie._Libro)
                .Where(x => x.FechaInicio.Date == FechaInicio.Date)
                .ToList();
        }


        public Prestamos? Borrar(int id)
        {
            var entidad = this.conexion.Prestamos!.FirstOrDefault(Prestamos => Prestamos.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó este nivel");

            this.conexion!.Prestamos!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Prestamos? Guardar(Prestamos? entidad)
        {            
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Prestamos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Prestamos> Listar()
        {
            return this.conexion!.Prestamos!
                .Include(Prestamo => Prestamo._Cliente!)
                .Include(Prestamo => Prestamo._NumeroSerie!._Libro)
                .Take(20).ToList();
        }

        public Prestamos? PorId(int Id)
        {
            return this.conexion!.Prestamos!.FirstOrDefault(x => x.Id==Id);
        }

        public Prestamos? Modificar(Prestamos? entidad)


        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
            
            var entry = this.conexion!.Entry<Prestamos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
