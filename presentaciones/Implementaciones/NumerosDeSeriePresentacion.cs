using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using presentaciones.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lib_presentaciones.Implementaciones
{
    public class NumerosDeSeriePresentacion : INumerosDeSeriePresentacion
    {
        private Comunicaciones? comunicaciones = null;
        private string? token;

        public void ponerToken(string token)
        {
            this.token = token;
        }

        public async Task<List<NumerosDeSerie>> Listar()
        {
            var lista = new List<NumerosDeSerie>();
            var datos = new Dictionary<string, object>()
            {
                { "Bearer", token! }
            };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "NumerosDeSerie/Listar");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<NumerosDeSerie>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));

            return lista;
        }
        public async Task<List<NumerosDeSerie>?> PorNumeroSerie(string NumeroSerie)
        {
            var lista = new List<NumerosDeSerie>();
            var datos = new Dictionary<string, object>
            {
                { "NumeroSerie", NumeroSerie },
                { "Bearer", token! }
            };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Niveles/PorNumeroSerie");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<NumerosDeSerie>>(
               JsonConversor.ConvertirAString(respuesta["Entidades"]));

            return lista;
        }

        public async Task<NumerosDeSerie?> PorId(int Id)
        {
            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "NumerosDeSerie/PorId");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            NumerosDeSerie? entidad = JsonConversor.ConvertirAObjeto<NumerosDeSerie>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<NumerosDeSerie?> Guardar(NumerosDeSerie? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "NumerosDeSerie/Guardar");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<NumerosDeSerie>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<NumerosDeSerie?> Modificar(NumerosDeSerie? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>
            {
                { "Entidad", JsonConversor.ConvertirAString(entidad) },
                { "Bearer", token! }
            };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "NumerosDeSerie/Modificar");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<NumerosDeSerie>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<NumerosDeSerie?> Borrar(int Id)
        {
            if (Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>
            {
                { "id", Id },
                { "Bearer", token! }
            };

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "NumerosDeSerie/Borrar");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            NumerosDeSerie? entidad = JsonConversor.ConvertirAObjeto<NumerosDeSerie>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}