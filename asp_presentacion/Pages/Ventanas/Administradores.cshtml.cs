using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class AdministradoresModel : PageModel
    {
        private readonly IAdministradoresPresentacion? iPresentacion;
        public List<Administradores>? Administradores { get; set; }
        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public AdministradoresModel(IAdministradoresPresentacion? presentacion)
        {
            iPresentacion = presentacion;
        }

        public void OnGet()
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                HttpContext.Response.Redirect("/Index");
                return;
            }

            this.iPresentacion?.ponerToken(token);

            if (VentanaActual == Enumerables.Ventanas.Listas)
            {
                Listar();
                return;
            }            
        }

        public void Listar()
        {            
            try
            {
                var AdministradoresTask = this.iPresentacion?.Listar();
                AdministradoresTask!.Wait();

                this.Administradores = AdministradoresTask.Result;                
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los administradores.";
            }
        }
    }
}
