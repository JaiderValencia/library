using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EstanteriasController : ControllerBase
    {
        private readonly IEstanteriasAplicacion? EstanteriasAplicacion;
        private readonly TokenController? tokenController;

        public EstanteriasController(IEstanteriasAplicacion? EstanteriasAplicacion, TokenController? tokenController)
        {
            this.EstanteriasAplicacion = EstanteriasAplicacion;
            this.tokenController = tokenController;
        }

        private Dictionary<string, object> ObtenerDatos()
        {
            var datos = new StreamReader(Request.Body).ReadToEnd().ToString();
            if (string.IsNullOrEmpty(datos))
                datos = "{}";
            return JsonConversor.ConvertirAObjeto(datos);
        }

        public string Listar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                int pagina = Convert.ToInt32(datos["pagina"]);

                respuesta["Entidades"] = this.EstanteriasAplicacion!.Listar(pagina, 20);

                respuesta["Respuesta"] = "OK";

                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        public string PorId()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                respuesta["Entidad"] = this.EstanteriasAplicacion!.PorId(Convert.ToInt32(datos["id"]))!;

                respuesta["Respuesta"] = "OK";

                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        public string PorNombre()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                respuesta["Entidades"] = this.EstanteriasAplicacion!.PorNombre(datos["nombre"].ToString()!);
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        public string Guardar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                Estanterias estanteria = JsonConversor.ConvertirAObjeto<Estanterias>(datos["Entidad"].ToString()!);

                respuesta["Entidad"] = this.EstanteriasAplicacion!.Guardar(estanteria);

                respuesta["Respuesta"] = "OK";

                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        public string Modificar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                Estanterias estanteria = JsonConversor.ConvertirAObjeto<Estanterias>(datos["Entidad"].ToString()!);

                respuesta["Entidad"] = this.EstanteriasAplicacion!.Modificar(estanteria);

                respuesta["Respuesta"] = "OK";

                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        public string Borrar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                respuesta["Entidad"] = this.EstanteriasAplicacion!.Borrar(Convert.ToInt32(datos["id"]));

                respuesta["Respuesta"] = "OK";

                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
        }
    }
}
