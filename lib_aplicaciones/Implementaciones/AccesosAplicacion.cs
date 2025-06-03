using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class AccesosAplicacion : IAccesosAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<Accesos>? PorNombre(string nombre)
        {
            return this.conexion!.Accesos!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public Accesos? Borrar(int id)
        {
            var entidad = this.conexion.Accesos!.FirstOrDefault(Autor => Autor.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó ese acceso");

            this.conexion!.Accesos!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Accesos? Guardar(Accesos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Accesos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Accesos> Listar()
        {
            return this.conexion!.Accesos!.Take(20).ToList();
        }

        public Accesos? PorId(int Id)
        {
            return this.conexion!.Accesos!.FirstOrDefault(x => x.Id == Id);
        }

        public bool validarAcceso(int AdministradorId, int AccesoId, string ruta)
        {
            var acceso = this.conexion!.Accesos!
                .FirstOrDefault(x => x.Id == AccesoId);

            if (acceso == null)
                throw new Exception("lbNoExisteAcceso");

            var roleAdministrador = this.conexion!.Administradores!
                .FirstOrDefault(Administrador => Administrador.Id == AdministradorId)!.Role;

            var RolAcceso = this.conexion!.Roles_tiene_Accesos!.FirstOrDefault(x => x.Acceso == AccesoId && x.Role == roleAdministrador);

            if (RolAcceso == null || string.IsNullOrEmpty(RolAcceso.Acciones))
                return false;

            if (ruta.Split("/")[2].Contains("Por"))
            {
                return RolAcceso.Acciones.Contains("Listar");
            }

            return RolAcceso.Acciones.Contains(ruta.Split("/")[2]);
        }

        public Accesos? Modificar(Accesos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.conexion!.Entry<Accesos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
