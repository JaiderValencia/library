using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class AdministradoresAplicacion : IAdministradoresAplicacion
    {
        private Conexion conexion = new Conexion();

        public Administradores? ObtenerUnoNombre(string nombre)
        {
            var entidad = this.conexion.Administradores!.FirstOrDefault(Administrador => Administrador.Nombre!.Equals(nombre));
            
            return entidad;
        }

        public List<Administradores>? PorNombre(string nombre)
        {
            return this.conexion!.Administradores!
                .Select(Administradores => new Administradores
                {
                    Id = Administradores.Id,
                    Nombre = Administradores.Nombre,
                    Role = Administradores.Role,
                    _Role = new Roles
                    {
                        Id = Administradores._Role!.Id,
                        Nombre = Administradores._Role.Nombre,
                    }
                })
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public Administradores? Borrar(int id)
        {
            var entidad = this.conexion.Administradores!.Select(Admin => new Administradores
            {
                Id = Admin.Id,
                Nombre = Admin.Nombre,
                Role = Admin.Role,                
            }).FirstOrDefault(Autor => Autor.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó ese administrador");

            this.conexion!.Administradores!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Administradores? Guardar(Administradores? entidad)
        {
            bool existe = this.conexion!.Administradores!.Any(Administrador => Administrador.Nombre!.Equals(entidad!.Nombre));

            if (existe)
                throw new Exception("Ya existe un administrador con ese nombre");

            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.Administradores!.Add(entidad);
            this.conexion.SaveChanges();

            entidad.Password = null;

            return entidad;
        }

        public List<Administradores> Listar()
        {
            return this.conexion!.Administradores!.Select(admin => new Administradores
            {
                Id = admin.Id,
                Nombre = admin.Nombre,
                Role = admin.Role,
            }).ToList();
        }

        public Administradores? PorId(int Id)
        {
            return this.conexion!.Administradores!.Select(admin => new Administradores
            {
                Id = admin.Id,
                Nombre = admin.Nombre,
                Role = admin.Role,
                _Role = new Roles
                {
                    Id = admin._Role!.Id,
                    Nombre = admin._Role.Nombre,                    
                }
            }).FirstOrDefault(Administrador => Administrador.Id == Id);
        }

        public string ObtenerPassword(int id)
        {
            var entidad = this.conexion.Administradores!.FirstOrDefault(admin => admin.Id == id) ?? throw new Exception("No se encontró el administrador");

            return entidad.Password!;
        }

        public Administradores? Modificar(Administradores? entidad)
        {
            bool existe = this.conexion!.Administradores!.Any(Administrador => Administrador.Nombre!.Equals(entidad!.Nombre) && Administrador.Id != entidad.Id);
            
            if (existe)
                throw new Exception("Ya existe un administrador con ese nombre");

            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");
        
            var entry = this.conexion!.Entry<Administradores>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
        public Administradores CrearOIngresarInvitado()
        {
            var usuario = this.conexion.Administradores!.FirstOrDefault(a => a.Nombre == "invitado");

            if (usuario != null)
                return usuario;

            var rol = this.conexion.Roles!.FirstOrDefault(r => r.Nombre == "Invitado");
            if (rol == null)
                throw new Exception("No existe el rol 'Invitado'");

            var nuevo = new Administradores
            {
                Nombre = "invitado",
                Password = "invitado",
                Role = rol.Id
            };

            this.conexion.Administradores!.Add(nuevo);
            this.conexion.SaveChanges();

            nuevo.Password = null;
            return nuevo;
        }


    }
}
