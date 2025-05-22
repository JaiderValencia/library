using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class AdministradoresAplicacion : IAdministradoresAplicacion
    {
        private Conexion conexion = new Conexion();

        public Administradores ObtenerUnoNombre(string usuario)
        {
            var entidad = this.conexion.Administradores!.FirstOrDefault(Administrador => Administrador.Nombre == usuario);

            if (entidad == null)
                throw new Exception("Usuario o contraseña incorrectos");

            return entidad;
        }

        public List<Administradores>? PorNombre(string nombre)
        {
            return this.conexion!.Administradores!
                .Include(Administradores => Administradores._Role)
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public Administradores? Borrar(int id)
        {
            var entidad = this.conexion.Administradores!.Include(Administradores => Administradores._Role).FirstOrDefault(Autor => Autor.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó ese autor");

            this.conexion!.Administradores!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Administradores? Guardar(Administradores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Administradores!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Administradores> Listar()
        {
            return this.conexion!.Administradores!.Take(20).ToList();
        }

        public Administradores? PorId(int Id)
        {
            return this.conexion!.Administradores!.FirstOrDefault(Administrador => Administrador.Id == Id);
        }

        public Administradores? Modificar(Administradores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.conexion!.Entry<Administradores>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
