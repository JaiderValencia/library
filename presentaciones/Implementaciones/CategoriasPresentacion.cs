using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class CategoriasPresentacion : ICategoriasPresentacion
    {
        private readonly Comunicaciones Comunicaciones = new Comunicaciones();
        private string? token;

        public void ponerToken(string token)
        {
            this.token = token;
        }

        public async Task<Categorias?> PorId(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Categorias/PorId");
            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Categorias>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<List<Categorias>> Listar()
        {
            var datos = new Dictionary<string, object>()
            {
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Categorias/Listar");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Categorias>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Categorias>> PorNombre(string nombre)
        {
            var datos = new Dictionary<string, object>
            {
                { "nombre", nombre },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Categorias/PorNombre");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Categorias>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Categorias?> Guardar(Categorias? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Categorias/Guardar");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Categorias>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Categorias?> Modificar(Categorias? entidad)
        {
            if (entidad == null)
                throw new Exception("Falta información");

            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Categorias/Modificar");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Categorias>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Categorias?> Borrar(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            datos = Comunicaciones.ConstruirUrl(datos, "Categorias/Borrar");

            var respuesta = await Comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Categorias>(JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}