using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class AdministradoresPresentacion : IAdministradoresPresentacion
    {
        private readonly Comunicaciones Comunicaciones = new Comunicaciones();
        private string? token;

        public void ponerToken(string token)
        {
            this.token = token;
        }

        public async Task<string> ObtenerToken(string nombre, string password)
        {
            var respuesta = await Comunicaciones.Autenticar(nombre, password);

            if (respuesta.ContainsKey("Error"))
                throw new Exception("Credenciales incorrectas");

            return respuesta["Token"].ToString()!;
        }

        public async Task<Administradores?> PorId(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Administradores/PorId");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Administradores>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<List<Administradores>?> PorNombre(string nombre)
        {
            var datos = new Dictionary<string, object>
            {
                { "nombre", nombre },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Administradores/PorNombre");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Administradores>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Administradores>> Listar()
        {
            var datos = new Dictionary<string, object>()
            {
                { "Bearer", token! }
            };
            datos = Comunicaciones.ConstruirUrl(datos, "Administradores/Listar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Administradores>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Administradores?> Guardar(Administradores entidad)
        {
            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Administradores/Guardar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Administradores>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Administradores?> Modificar(Administradores entidad)
        {
            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Administradores/Modificar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Administradores>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Administradores?> Borrar(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Administradores/Borrar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Administradores>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}
