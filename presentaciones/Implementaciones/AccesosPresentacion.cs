using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class AccesosPresentacion : IAccesosPresentacion
    {
        private readonly Comunicaciones comunicaciones = new Comunicaciones();
        private string? token;

        public void ponerToken(string token)
        {
            this.token = token;
        }

        public async Task<List<Accesos>> Listar()
        {
            var datos = new Dictionary<string, object>()
            {
                { "Bearer", token! }
            };

            datos = comunicaciones.ConstruirUrl(datos, "Accesos/Listar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Accesos>>(JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }
    }
}
