using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class AutoresPresentacion : IAutoresPresentacion
    {
        private readonly Comunicaciones Comunicaciones = new Comunicaciones();
        private string? token;

        public void ponerToken(string token)
        {
            this.token = token;
        }

        public async Task<List<Autores>> Listar()
        {
            var datos = new Dictionary<string, object>()
            {
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Autores/Listar");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Autores>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Autores?> PorId(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Autores/PorId");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Autores>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<List<Autores>?> PorNombre(string nombre)
        {
            var datos = new Dictionary<string, object>
            {
                { "nombre", nombre },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Autores/PorNombre");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Autores>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Autores?> Guardar(Autores entidad)

        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Autores/Guardar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Autores>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Autores?> Modificar(Autores entidad)
        {
            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Autores/Modificar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Autores>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Autores?> Borrar(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Autores/Borrar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Autores>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}
