using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class NivelesPresentacion : INivelesPresentacion
    {
        private Comunicaciones? comunicaciones = null;

        public async Task<List<Niveles>> Listar()
        {
            var lista = new List<Niveles>();
            var datos = new Dictionary<string, object>();

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Niveles/Listar");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            lista = JsonConversor.ConvertirAObjeto<List<Niveles>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));

            return lista;
        }

        public async Task<List<Niveles>> PorNombre(string Nombre)
        {
            var datos = new Dictionary<string, object>();
            datos["Nombre"] = Nombre!;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Niveles/PorNombre");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            var lista = JsonConversor.ConvertirAObjeto<List<Niveles>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
            return lista;
        }
        public async Task<Niveles?> PorId(int Id)
        {
            var datos = new Dictionary<string, object>();
            datos["Id"] = Id!;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Niveles/PorId");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            Niveles? entidad = JsonConversor.ConvertirAObjeto<Niveles>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Niveles?> Guardar(Niveles? entidad)
        {
            if (entidad!.Id != 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Niveles/Guardar");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Niveles>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Niveles?> Modificar(Niveles? entidad)
        {
            if (entidad!.Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Entidad"] = entidad;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Niveles/Modificar");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            entidad = JsonConversor.ConvertirAObjeto<Niveles>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }

        public async Task<Niveles?> Borrar(int Id)
        {
            if (Id == 0)
            {
                throw new Exception("lbFaltaInformacion");
            }

            var datos = new Dictionary<string, object>();
            datos["Id"] = Id;

            comunicaciones = new Comunicaciones();
            datos = comunicaciones.ConstruirUrl(datos, "Niveles/Borrar");
            var respuesta = await comunicaciones!.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }
            Niveles? entidad = JsonConversor.ConvertirAObjeto<Niveles>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
            return entidad;
        }
    }
}