using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    class NivelesAplicacion: INivelesAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Niveles>? PorNombre(string nombre)
        {
            return this.conexion!.Niveles!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public Niveles? Borrar(Niveles entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

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
            return this.conexion!.Niveles!.Take(20).ToList();
        }

        public Niveles? PorId(int Id)
        {
            return this.conexion!.Niveles!.FirstOrDefault(x => x.Id==Id);
        }

        public Niveles? Modificar(Niveles? entidad)


        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad.Nombre = "Nombre cambiado";

            var entry = this.conexion!.Entry<Niveles>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
