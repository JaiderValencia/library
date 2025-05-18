using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class EstanteriasAplicacion : IEstanteriasAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Estanterias> Listar(int pagina = 1, int tamañoPagina = 20)
        {
            return this.conexion.Estanterias!
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina).ToList();
        }        

        public Estanterias? PorId(int Id)
        {
            return this.conexion.Estanterias!.FirstOrDefault(estanteria => estanteria.Id == Id);
        }

        public List<Estanterias> PorNombre(string nombre)
        {
            return this.conexion.Estanterias!.Where(Estanteria => Estanteria.Nombre!.Contains(nombre)).ToList();
        }

        public Estanterias Guardar(Estanterias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion.Estanterias!.Add(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }

        public Estanterias Modificar(Estanterias? entidad)
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

        public Estanterias Borrar(int id)
        {
            var entidad = this.conexion.Estanterias!.FirstOrDefault(estanteria => estanteria.Id == id);

            if (entidad == null)
                throw new Exception("No existe esta estanteria");

            this.conexion.Estanterias!.Remove(entidad);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
