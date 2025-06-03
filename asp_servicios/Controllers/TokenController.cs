using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using lib_dominio.Nucleo;
using lib_aplicaciones.Interfaces;
using Bcrypt = BCrypt.Net.BCrypt;
using lib_dominio.Entidades;



namespace asp_servicios.Controllers
{
    public class TokenController : ControllerBase
    {
        private readonly IAdministradoresAplicacion administradoresAplicacion;
        private readonly IAccesosAplicacion? accesosAplicacion;
        private readonly IAuditoriasAplicacion? auditoriasAplicacion;

        private int? AccesoId { get; set; }

        public TokenController(IAdministradoresAplicacion administradoresAplicacion, IAccesosAplicacion accesosAplicacion, IAuditoriasAplicacion? auditoriasAplicacion = null)
        {
            this.administradoresAplicacion = administradoresAplicacion;
            this.accesosAplicacion = accesosAplicacion;
            this.auditoriasAplicacion = auditoriasAplicacion;
        }

        public void ponerAccesoId(int? accesoId)
        {
            this.AccesoId = accesoId;
        }

        private Dictionary<string, object> ObtenerDatos()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = new StreamReader(Request.Body).ReadToEnd().ToString();
                if (string.IsNullOrEmpty(datos))
                    datos = "{}";
                return JsonConversor.ConvertirAObjeto(datos);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message.ToString();
                return respuesta;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Token/Autenticar")]
        public string Autenticar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();

                string nombre = datos["Nombre"].ToString()!;
                string password = datos["Password"].ToString()!;

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(password))
                {
                    respuesta["Error"] = "lb Falta usuario o contraseña";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var administrador = administradoresAplicacion.ObtenerUnoNombre(nombre);

                if (administrador == null || !Bcrypt.Verify(password, administrador!.Password))
                {
                    respuesta["Error"] = "lb Nombre o contraseña incorrectos";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("administrador_id", administrador.Id.ToString()!)}),

                    Expires = DateTime.UtcNow.AddHours(1),

                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DatosGenerales.clave)), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                respuesta["Token"] = tokenHandler.WriteToken(token);
                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.ToString();
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        public bool Validate(Dictionary<string, object> data, string ruta)
        {
            try
            {
                var authorizationHeader = data["Bearer"].ToString();

                authorizationHeader = authorizationHeader!.Replace("Bearer ", "");

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.ReadToken(authorizationHeader) as JwtSecurityToken;

                // Extraer el ID del administrador desde el claim
                var idClaim = token!.Claims.FirstOrDefault(claim => claim.Type == "administrador_id");

                var administradorId = int.Parse(idClaim!.Value);

                bool validarTiempo = DateTime.UtcNow < token.ValidTo;

                bool validarId = this.administradoresAplicacion.PorId(administradorId) != null;

                bool validarAcceso = this.accesosAplicacion!.validarAcceso(administradorId, AccesoId ?? 0, ruta);

                if (validarTiempo && validarId && validarAcceso && !ruta.ToLower().Contains("auditorias"))
                {
                    string Administrador = this.administradoresAplicacion.PorId(administradorId)!.Nombre!;

                    this.auditoriasAplicacion?.Guardar(new Auditorias
                    {
                        Administrador = Administrador,
                        Accion = ruta,
                        Fecha = DateTime.Now
                    });
                }

                return validarTiempo && validarId && validarAcceso;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating token: {ex.Message}");
                return false;
            }
        }
    }
}