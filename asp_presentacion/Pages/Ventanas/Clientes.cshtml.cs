using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ClientesModel : PageModel
    {
        private readonly IClientesPresentacion? iPresentacion;
        private readonly ITiposDocumentosPresentacion? iTiposDocumentosPresentacion;

        public List<Clientes>? Clientes { get; set; }
        public List<TiposDocumentos>? TiposDocumentos { get; set; }
        [BindProperty] public Clientes? Cliente { get; set; } = null;

        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNombre", 2 },
            { "PorDocumento", 3 }
        };

        public ClientesModel(IClientesPresentacion? clientesPresentacion, ITiposDocumentosPresentacion tiposDocumentosPresentacion)
        {
            this.iPresentacion = clientesPresentacion;
            this.iTiposDocumentosPresentacion = tiposDocumentosPresentacion;
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
            this.iTiposDocumentosPresentacion?.ponerToken(token);
        }

        public void OnPostBuscar(string data, int opcion)
        {
            try
            {
                comprobarToken();
                VentanaActual = Enumerables.Ventanas.Listas;

                if (opcion == opcionesBuscador["PorId"])
                {
                    var clienteTask = iPresentacion?.PorId(Convert.ToInt32(data));
                    clienteTask!.Wait();
                    if (clienteTask.Result == null)
                    {
                        Clientes = null;
                        return;
                    }
                    Clientes = new List<Clientes> { clienteTask.Result };
                }
                else if (opcion == opcionesBuscador["PorNombre"])
                {
                    var clientesTask = iPresentacion?.PorNombre(data);
                    clientesTask!.Wait();
                    Clientes = clientesTask.Result!;
                }
                else if (opcion == opcionesBuscador["PorDocumento"])
                {
                    var clientesTask = iPresentacion?.PorDocumento(data);
                    clientesTask!.Wait();
                    Clientes = clientesTask.Result!;
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar clientes.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                VentanaActual = Enumerables.Ventanas.Listas;
                var clientesTask = iPresentacion?.Listar(1);
                clientesTask!.Wait();
                Clientes = clientesTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los clientes.";
            }
        }

        public void ObtnerTiposDocumentos()
        {
            try
            {
                var tiposDocumentosTask = iTiposDocumentosPresentacion?.Listar();
                tiposDocumentosTask!.Wait();

                this.TiposDocumentos = tiposDocumentosTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los tipos de documentos.";
            }
        }

        public void OnPostVistaCrear()
        {
            comprobarToken();
            VentanaActual = Enumerables.Ventanas.Crear;
            Cliente = new Clientes();
            ObtnerTiposDocumentos();
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                var clienteTask = iPresentacion?.PorId(Convert.ToInt32(data));
                clienteTask!.Wait();
                Cliente = clienteTask.Result;
                ObtnerTiposDocumentos();
                VentanaActual = Enumerables.Ventanas.Editar;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el cliente para editar.";
            }
        }

        public IActionResult OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = iPresentacion?.Guardar(Cliente!);
                crearTask!.Wait();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear el cliente.";
                return Page();
            }
        }

        public IActionResult OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = iPresentacion?.Modificar(Cliente!);
                modificarTask!.Wait();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al modificar el cliente.";
                return Page();
            }
        }

        public IActionResult OnPostBorrar(string data)
        {
            try
            {
                comprobarToken();
                var borrarTask = iPresentacion?.Borrar(Convert.ToInt32(data));
                borrarTask!.Wait();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al borrar el cliente.";
                return Page();
            }
        }
    }
}