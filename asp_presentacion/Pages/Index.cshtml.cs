using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAdministradoresPresentacion? IPresentacion = null;

        public IndexModel(IAdministradoresPresentacion? presentacion)
        {
            IPresentacion = presentacion;
        }

        public bool EstaLogueado = false;
        [BindProperty] public string? Nombre { get; set; }
        [BindProperty] public string? Password { get; set; }

        public bool MostrarAlerta { get; set; } = false;        
        public string? MensajeAlerta { get; set; } = null;

        public void OnGetMostrarAlerta(string titulo, string mensaje)
        {
            Nombre = string.Empty;
            Password = string.Empty;

            MostrarAlerta = true;            
            MensajeAlerta = mensaje;
        }

        public void OnGetCerrarAlerta()
        {
            MostrarAlerta = false;            
            MensajeAlerta = null;
        }

        public void OnPostCerrarSesion()
        {
            HttpContext.Session.Remove("Token");
            EstaLogueado = false;
            ViewData["Logged"] = false;
        }

        public void OnGet()
        {
            var variable_session = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true;

                ViewData["Logged"] = true;
                return;
            }
            ViewData["Logged"] = false;
        }

        public void OnPostBtClean()
        {
            try
            {
                Nombre = string.Empty;
                Password = string.Empty;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(Nombre) && string.IsNullOrEmpty(Password))
                {
                    OnPostBtClean();
                    return;
                }

                var token = this.IPresentacion!.ObtenerToken(Nombre!, Password!);
                token.Wait();
                
                HttpContext.Session.SetString("Token", token.Result.ToString()!);
                ViewData["Logged"] = true;
                
                EstaLogueado = true;    
                OnPostBtClean();
            }
            catch (Exception ex)
            {                
                OnGetMostrarAlerta("Error", ex.Message);
                LogConversor.Log(ex, ViewData!);
            }
        }
        public void OnPostBtInvitadoAsync()
        {
            try
            {                
                var token = this.IPresentacion!.ObtenerToken("Invitado", "ContraseñaInvitado");
                token.Wait();
                
                HttpContext.Session.SetString("Token", token.Result.ToString()!);
                ViewData["Logged"] = true;
                
                EstaLogueado = true;    
                OnPostBtClean();
            }
            catch (Exception ex)
            {                
                OnGetMostrarAlerta("Error", ex.Message);
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
