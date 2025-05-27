using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class RolesPresentacion : IRolesPresentacion
    {
        private readonly Comunicaciones Comunicaciones = new Comunicaciones();
        private string? token;
        public void ponerToken(string token)
        {
            this.token = token;
        }

        public async Task<Roles?> PorId(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Roles/PorId");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Roles>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<List<Roles>?> PorNombre(string nombre)
        {
            var datos = new Dictionary<string, object>
            {
                { "nombre", nombre },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Roles/PorNombre");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Roles>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Roles>> Listar()
        {
            var datos = new Dictionary<string, object>
            {
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Roles/Listar");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Roles>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Roles?> Guardar(Roles entidad)
        {
            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Roles/Guardar");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Roles>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Roles?> Modificar(Roles entidad)
        {
            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Roles/Modificar");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Roles>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Roles?> Borrar(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Roles/Borrar");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Roles>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}
