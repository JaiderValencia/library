using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class LibrosPresentacion : ILibrosPresentacion
    {
        private readonly Comunicaciones Comunicaciones = new Comunicaciones();
        private string? token;

        public void ponerToken(string token)
        {
            this.token = token;
        }

        public async Task<List<Libros>> Listar(int pagina = 1)
        {
            var datos = new Dictionary<string, object>
            {
                { "pagina", pagina },
                { "tamañoPagina", 20 },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Libros/Listar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Libros>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Libros?> PorId(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Libros/PorId");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Libros>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<List<Libros>> PorNombre(string nombre)
        {
            var datos = new Dictionary<string, object>
            {
                { "nombre", nombre },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Libros/PorNombre");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Libros>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Libros> Guardar(Libros entidad)
        {
            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Libros/Guardar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Libros>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Libros> Modificar(Libros entidad)
        {
            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Libros/Modificar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Libros>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Libros> Borrar(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Libros/Borrar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Libros>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}
