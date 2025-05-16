using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class CategoriasAplicacion : ICategoriasAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Categorias>? PorNombre(string nombre)
        {
            return this.conexion!.Categorias!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public Categorias? Borrar(int id)
        {
            var entidad = this.conexion.Categorias!.FirstOrDefault(Categoria => Categoria.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó esa categoría");
            
            this.conexion!.Categorias!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Categorias? PorId(int Id)
        {
            return this.conexion!.Categorias!.FirstOrDefault(x => x.Id == Id);
        }

        public Categorias? Modificar(Categorias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.conexion!.Entry<Categorias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Categorias> Listar()
        {
            return this.conexion!.Categorias!.Take(20).ToList();
        }

        public Categorias? Guardar(Categorias? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Categorias!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
