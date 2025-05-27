using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class AutoresModel : PageModel
    {
        private readonly IAutoresPresentacion? iPresentacion;

        public List<Autores>? Autores { get; set; }
        [BindProperty] public Autores? Autor { get; set; } = null;

        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNombre", 2 },
        };

        public AutoresModel(IAutoresPresentacion? autoresPresentacion)
        {
            this.iPresentacion = autoresPresentacion;
        }

        public void OnGet()
        {
            comprobarToken();
            if (VentanaActual == Enumerables.Ventanas.Listas)
            {
                OnPostListar();
                return;
            }
        }

        private void comprobarToken()
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                HttpContext.Response.Redirect("/Index");
                return;
            }

            this.iPresentacion?.ponerToken(token);
        }
                
        public void OnPostBuscar(string data, int opcion)
        {
            try
            {
                comprobarToken();
                VentanaActual = Enumerables.Ventanas.Listas;

                if (opcion == this.opcionesBuscador["PorId"])
                {
                    var administradorTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                    administradorTask!.Wait();
                

                    if (administradorTask.Result == null)
                    {
                        this.Autores = null;
                        return;
                    }
                    
                    this.Autores = new List<Autores> { administradorTask.Result };
                }
                else if (opcion == this.opcionesBuscador["PorNombre"])
                {
                    var AutoresTask = this.iPresentacion?.PorNombre(data);
                    AutoresTask!.Wait();

                    this.Autores = AutoresTask.Result!;
                }                
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar administradores.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Listas;
                var AutoresTask = this.iPresentacion?.Listar();
                AutoresTask?.Wait();

                Autores = AutoresTask?.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, $"Error al listar autores: {ex.Message}");
            }
        }

        public void OnPostBorrar(string data)
        {
            try
            {
                comprobarToken();
                var borrarTask = this.iPresentacion?.Borrar(Convert.ToInt32(data));
                borrarTask!.Wait();

                OnPostListar();
                return;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al borrar el autor.";
            }
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                this.VentanaActual = Enumerables.Ventanas.Editar;

                var autorTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                autorTask!.Wait();

                this.Autor = autorTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el autor para editar.";
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = this.iPresentacion?.Modificar(this.Autor!);
                modificarTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.Autor = modificarTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el autor para editar.";
            }
        }

        public void OnPostVistaCrear()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Crear;
                comprobarToken();                

                this.Autor = new Autores();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la vista para crear un administrador.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = this.iPresentacion?.Guardar(this.Autor!);
                crearTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Listas;
                OnPostListar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear el administrador.";
            }
        }
    }
}
