using lib_aplicaciones.Interfaces;
using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditoriasController : ControllerBase
    {
        private readonly IAuditoriasAplicacion? auditoriasAplicacion;
        private readonly TokenController? tokenController;

        public AuditoriasController(IAuditoriasAplicacion? auditoriasAplicacion, TokenController? tokenController)
        {
            this.auditoriasAplicacion = auditoriasAplicacion;
            this.tokenController = tokenController;
            tokenController!.ponerAccesoId(13);
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
                if (!tokenController!.Validate(datos, Request.Path.Value!))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                respuesta["Entidades"] = this.auditoriasAplicacion!.Listar()!;

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

        public string PorAdministrador()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos, Request.Path.Value!))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                respuesta["Entidades"] = this.auditoriasAplicacion!.PorAdministrador(datos["Administrador"].ToString()!)!;

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

        public string PorFecha()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos, Request.Path.Value!))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                DateTime fechaDesde = DateTime.Parse(datos["FechaDesde"].ToString()!);
                DateTime fechaHasta = DateTime.Parse(datos["FechaHasta"].ToString()!);

                respuesta["Entidades"] = this.auditoriasAplicacion!.PorFecha(fechaDesde, fechaHasta)!;

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
