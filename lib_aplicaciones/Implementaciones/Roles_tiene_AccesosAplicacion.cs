using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class Roles_tiene_AccesosAplicacion: IRoles_tiene_AccesosAplicacion
    {
        private Conexion conexion = new Conexion();

        public Roles_tiene_Accesos? Borrar(int id)
        {
            var entidad = this.conexion.Roles_tiene_Accesos!.FirstOrDefault(Roles_tiene_Accesos => Roles_tiene_Accesos.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó");

            this.conexion!.Roles_tiene_Accesos!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Roles_tiene_Accesos? Guardar(Roles_tiene_Accesos? entidad)
        {            
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Roles_tiene_Accesos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Roles_tiene_Accesos> Listar()
        {
            return this.conexion!.Roles_tiene_Accesos!.Take(20).ToList();
        }

        public Roles_tiene_Accesos? PorId(int Id)
        {
            return this.conexion!.Roles_tiene_Accesos!
                .Include(Roles_tiene_Accesos => Roles_tiene_Accesos._Acceso)
                .Include(Roles_tiene_Accesos => Roles_tiene_Accesos._Role)
                .FirstOrDefault(x => x.Id==Id);
        }

        public List<Roles_tiene_Accesos> PorRol(int rolId)
        {
            return this.conexion!.Roles_tiene_Accesos!                
                .Where(x => x.Role == rolId)
                .ToList();
        }

        public Roles_tiene_Accesos? Modificar(Roles_tiene_Accesos? entidad)


        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad.Acceso = 2;

            var entry = this.conexion!.Entry<Roles_tiene_Accesos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
