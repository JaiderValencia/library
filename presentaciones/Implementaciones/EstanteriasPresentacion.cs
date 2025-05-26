using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class EstanteriasPresentacion : IEstanteriasPresentacion
    {
        private readonly Comunicaciones Comunicaciones = new Comunicaciones();
        private string? token;

        public void ponerToken(string token)
        {
            this.token = token;
        }

        public async Task<List<Estanterias>> Listar(int pagina = 1)
        {
            var datos = new Dictionary<string, object>()
            {
                { "pagina", pagina },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Estanterias/Listar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Estanterias>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Estanterias?> PorId(int Id)
        {
            var datos = new Dictionary<string, object> { { "id", Id }, { "Bearer", token! } };
            datos = Comunicaciones.ConstruirUrl(datos, "Estanterias/PorId");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Estanterias>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<List<Estanterias>> PorNombre(string nombre)
        {
            var datos = new Dictionary<string, object> { { "nombre", nombre }, { "Bearer", token! } };
            datos = Comunicaciones.ConstruirUrl(datos, "Estanterias/PorNombre");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Estanterias>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Estanterias> Guardar(Estanterias entidad)
        {
            var datos = new Dictionary<string, object> { { "entidad", JsonConversor.ConvertirAString(entidad) }, { "Bearer", token! } };
            datos = Comunicaciones.ConstruirUrl(datos, "Estanterias/Guardar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Estanterias>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Estanterias> Modificar(Estanterias entidad)
        {
            var datos = new Dictionary<string, object> { { "entidad", JsonConversor.ConvertirAString(entidad) }, { "Bearer", token! } };

            datos = Comunicaciones.ConstruirUrl(datos, "Estanterias/Modificar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Estanterias>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Estanterias> Borrar(int Id)
        {
            var datos = new Dictionary<string, object> { { "id", Id }, { "Bearer", token! } };

            datos = Comunicaciones.ConstruirUrl(datos, "Estanterias/Borrar");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Estanterias>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}