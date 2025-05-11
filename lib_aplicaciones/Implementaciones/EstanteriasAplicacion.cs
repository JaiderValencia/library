using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class EstanteriasAplicacion : IEstanteriasAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Estanterias> Listar()
        {
            return this.conexion.Estanterias!.Take(20).ToList();
        }

        public Estanterias? PorId(int Id)
        {
            return this.conexion.Estanterias!.FirstOrDefault(estanteria => estanteria.Id == Id);
        }

        public Estanterias? PorNombre(string nombre)
        {
            return this.conexion.Estanterias!.FirstOrDefault(estanteria => estanteria.Nombre == nombre);
        }

        public Estanterias? Guardar(Estanterias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.conexion.Estanterias!.Add(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }

        public Estanterias? Modificar(Estanterias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.conexion.Estanterias!.Entry(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }

        public Estanterias? Borrar(Estanterias entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.conexion.Estanterias!.Remove(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
