using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private Conexion conexion = new Conexion();
        private Roles_tiene_AccesosAplicacion rolesAccesosApp = new Roles_tiene_AccesosAplicacion();

        public List<Roles>? PorNombre(string nombre)
        {
            var Roles = this.conexion!.Roles!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();

            if (Roles != null)
            {
                foreach (var rol in Roles)
                {
                    rol.CantidadPermisos = this.conexion!.Roles_tiene_Accesos!.Count(x => x.Role == rol.Id);
                }
            }

            return Roles;
        }

        public Roles? Borrar(int id)
        {
            var entidad = this.conexion.Roles!.FirstOrDefault(Autor => Autor.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó ese autor");

            this.conexion!.Roles!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public void GuardarAccesoRol(List<int> accesos, int rolId)
        {
            var accesosRole = this.rolesAccesosApp.PorRol(rolId);

            if (accesos == null && accesosRole != null && accesosRole.Count > 0)
            {
                foreach (var accesoRole in accesosRole)
                {
                    this.conexion!.Roles_tiene_Accesos!.Remove(accesoRole);
                }
                this.conexion.SaveChanges();

                return;
            }

            if (accesosRole != null && accesos != null)
            {
                foreach (var accesoRole in accesosRole)
                {
                    if (!accesos.Contains(accesoRole.Acceso))
                    {
                        this.conexion!.Roles_tiene_Accesos!.Remove(accesoRole);
                    }
                }
            }

            if (accesos != null)
            {
                foreach (var acceso in accesos)
                {
                    if (accesosRole != null && accesosRole.Any(x => x.Acceso == acceso && x.Role == rolId))
                        continue;

                    this.conexion!.Roles_tiene_Accesos!.Add(new Roles_tiene_Accesos
                    {
                        Role = rolId,
                        Acceso = acceso
                    });
                }
            }

            this.conexion.SaveChanges();
        }

        public Roles? Guardar(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Roles!.Add(entidad);
            this.conexion.SaveChanges();

            if (entidad.Accesos != null)
                this.GuardarAccesoRol(entidad.Accesos, entidad.Id);

            return entidad;
        }

        public List<Roles> Listar()
        {
            var Roles = this.conexion!.Roles!.ToList();

            if (Roles != null)
            {
                foreach (var rol in Roles)
                {
                    rol.CantidadPermisos = this.conexion!.Roles_tiene_Accesos!.Count(x => x.Role == rol.Id);
                }
            }

            return Roles!;
        }

        public Roles? PorId(int Id)
        {
            var rol = this.conexion!.Roles!.FirstOrDefault(x => x.Id == Id);

            if (rol == null)
                throw new Exception("lbNoSeEncontro");


            rol.Accesos = this.conexion.Roles_tiene_Accesos!
            .Where(x => x.Role == rol.Id)
            .Select(x => x.Acceso)
            .ToList();

            rol.CantidadPermisos = this.conexion!.Roles_tiene_Accesos!.Count(x => x.Role == rol.Id);

            return rol;
        }

        public Roles? Modificar(Roles? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.conexion!.Entry<Roles>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();

            GuardarAccesoRol(entidad.Accesos!, entidad.Id);

            return entidad;
        }
    }
}
