using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class ClientesPresentacion : IClientesPresentacion
    {
        private readonly Comunicaciones Comunicaciones = new Comunicaciones();
        private string? token;

        public void ponerToken(string token)
        {
            this.token = token;
        }

        public async Task<Clientes?> PorId(int id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Clientes/PorId");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Clientes>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<List<Clientes>> PorNombre(string nombre)
        {
            var datos = new Dictionary<string, object>
            {
                { "nombre", nombre },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Clientes/PorNombre");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Clientes>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Clientes>> PorDocumento(string documento)
        {
            var datos = new Dictionary<string, object>
            {
                { "documento", documento },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Clientes/PorDocumento");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Clientes>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Clientes>> Listar(int pagina)
        {
            var datos = new Dictionary<string, object>
            {
                { "pagina", pagina },
                { "tamañoPagina", 20 },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Clientes/Listar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Clientes>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Clientes?> Guardar(Clientes entidad)
        {
            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Clientes/Guardar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Clientes>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Clientes?> Modificar(Clientes entidad)
        {
            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Clientes/Modificar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Clientes>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Clientes?> Borrar(int id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Clientes/Borrar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Clientes>(JsonConversor.ConvertirAString(respuesta["Entidad"]));

        }
    }
}
