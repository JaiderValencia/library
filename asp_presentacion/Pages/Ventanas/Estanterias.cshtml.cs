using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class EstanteriasModel : PageModel
    {
        private readonly IEstanteriasPresentacion? iPresentacion;

        public List<Estanterias>? Estanterias { get; set; }
        [BindProperty] public Estanterias? Estanteria { get; set; } = null;

        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNombre", 2 }
        };

        public EstanteriasModel(IEstanteriasPresentacion? estanteriasPresentacion)
        {
            this.iPresentacion = estanteriasPresentacion;
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

                if (opcion == opcionesBuscador["PorId"])
                {
                    var estanteriaTask = iPresentacion?.PorId(Convert.ToInt32(data));
                    estanteriaTask!.Wait();
                    if (estanteriaTask.Result == null)
                    {
                        Estanterias = null;
                        return;
                    }
                    Estanterias = new List<Estanterias> { estanteriaTask.Result };
                }
                else if (opcion == opcionesBuscador["PorNombre"])
                {
                    var estanteriasTask = iPresentacion?.PorNombre(data);
                    estanteriasTask!.Wait();
                    Estanterias = estanteriasTask.Result!;
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar estanterías.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                VentanaActual = Enumerables.Ventanas.Listas;
                var estanteriasTask = iPresentacion?.Listar(1);
                estanteriasTask!.Wait();
                Estanterias = estanteriasTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar las estanterías.";
            }
        }

        public void OnPostVistaCrear()
        {
            VentanaActual = Enumerables.Ventanas.Crear;
            Estanteria = new Estanterias();
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                var estanteriaTask = iPresentacion?.PorId(Convert.ToInt32(data));
                estanteriaTask!.Wait();
                Estanteria = estanteriaTask.Result;
                VentanaActual = Enumerables.Ventanas.Editar;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la estantería para editar.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = iPresentacion?.Guardar(Estanteria!);
                crearTask!.Wait();
                
                OnPostListar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear la estantería.";
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = iPresentacion?.Modificar(Estanteria!);
                modificarTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.Estanteria = modificarTask.Result;                
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al modificar la estantería.";                
            }
        }

        public void OnPostBorrar(string data)
        {
            try
            {
                comprobarToken();
                var borrarTask = iPresentacion?.Borrar(Convert.ToInt32(data));
                borrarTask!.Wait();
                OnPostListar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al borrar la estantería.";                
            }
        }
    }
}