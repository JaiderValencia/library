using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    class EntidadesNucleo
    {
        public static Estanterias? Estanterias()
        {
            var entidad = new Estanterias();
            entidad.Nombre = "Prueba";
            entidad.Activo = true;
            return entidad;
        }
        public static Niveles? Niveles()
        {
            var entidad = new Niveles();
            entidad.Nombre = "Prueba";
            entidad.Estanteria = 2;
            return entidad;
        }
        public static Categorias? Categorias()
        {
            var entidad = new Categorias();
            entidad.Nombre = "Prueba";
            entidad.Descripcion = "Esto es una prueba";
            return entidad;
        }
        public static Autores? Autores()
        {
            var entidad = new Autores();
            entidad.Nombre = "Prueba";
            entidad.Apellido = "Unitaria";
            entidad.FechaNacimiento = DateTime.Now.Date;
            return entidad;
        }
        public static Libros? Libros()
        {
            var entidad = new Libros();
            entidad.Nombre = "Prueba";
            entidad.Fecha = DateTime.Now;
            entidad.Categoria = 1;
            entidad.Autor = 1;
            return entidad;
        }
        public static NumerosDeSerie? NumerosDeSerie()
        {
            var entidad = new NumerosDeSerie();
            entidad.NumeroSerie = "Prueba";
            entidad.Libro = 1;
            return entidad;
        }
        public static Niveles_tiene_Libros? Niveles_tiene_Libros()
        {
            var entidad = new Niveles_tiene_Libros();
            entidad.Nivel = 1;
            entidad.Libro = 1;
            return entidad;
        }
        public static TiposDocumentos? TiposDocumentos()
        {
            var entidad = new TiposDocumentos();
            entidad.Nombre = "Prueba";
            entidad.NombreCorto = "PB";
            return entidad;
        }
        public static Clientes? Clientes()
        {
            var entidad = new Clientes();
            entidad.Nombre = "Prueba";
            entidad.Apellido = "Unitaria";
            entidad.Documento = "PB123";
            entidad.Direccion = "calle de prueba";
            entidad.Correo = "correo@Prueba.com";
            entidad.Telefono = "123abc";
            entidad.TipoDocumento = 1;
            return entidad;
        }
        public static Prestamos? Prestamos()
        {
            var entidad = new Prestamos();
            entidad.FechaInicio = DateTime.Now;
            entidad.FechaFinal = DateTime.Now;
            entidad.FechaEntregado = DateTime.Now;
            entidad.Cliente = 1;
            entidad.NumeroSerie = 1;
            return entidad;
        }
        public static Accesos? Accesos()
        {
            var entidad = new Accesos();
            entidad.Nombre = "Prueba";
            return entidad;
        }

        public static Administradores? Administradores()
        {
            var entidad = new Administradores();
            entidad.Nombre = "Prueba";
            entidad.Password = "PB123";
            entidad.Role = 1;
            return entidad;
        }

        public static Auditorias? Auditorias()
        {
            var entidad = new Auditorias();
            entidad.Accion = "Prueba";
            entidad.Fecha = DateTime.Now;
            entidad.Administrador = "Prueba";
            return entidad;
        }

        public static Roles? Roles()
        {
            var entidad = new Roles();
            entidad.Nombre = "Prueba";
            entidad.Accesos = [1,2,3];
            entidad.CantidadPermisos = 2;
            return entidad;
        }

        public static Roles_tiene_Accesos? Roles_tiene_Accesos()
        {
            var entidad = new Roles_tiene_Accesos();
            entidad.Acceso = 1;
            entidad.Role = 1;
            return entidad;
        }
    }
}
