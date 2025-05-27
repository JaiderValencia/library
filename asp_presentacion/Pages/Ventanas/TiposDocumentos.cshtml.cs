using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class TiposDocumentosModel : PageModel
    {
        private readonly ITiposDocumentosPresentacion? iPresentacion;

        public List<TiposDocumentos>? TiposDocumentos { get; set; }

        [BindProperty] public TiposDocumentos? TipoDocumento { get; set; } = null;
        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNombre", 2 },
        };

        public TiposDocumentosModel(ITiposDocumentosPresentacion? TiposDocumentosPresentacion)
        {
            this.iPresentacion = TiposDocumentosPresentacion;
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

        public void OnGet()
        {
            comprobarToken();
            if (VentanaActual == Enumerables.Ventanas.Listas)
            {
                OnPostListar();
                return;
            }
        }

        public void OnPostBuscar(string data, int opcion)
        {
            try
            {
                comprobarToken();
                VentanaActual = Enumerables.Ventanas.Listas;

                if (opcion == this.opcionesBuscador["PorId"])
                {
                    var TipoDocumentoTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                    TipoDocumentoTask!.Wait();


                    if (TipoDocumentoTask.Result == null)
                    {
                        this.TiposDocumentos = null;
                        return;
                    }

                    this.TiposDocumentos = new List<TiposDocumentos> { TipoDocumentoTask.Result };
                }
                else if (opcion == this.opcionesBuscador["PorNombre"])
                {
                    var TiposDocumentosTask = this.iPresentacion?.PorNombre(data);
                    TiposDocumentosTask!.Wait();

                    this.TiposDocumentos = TiposDocumentosTask.Result!;
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar Tipo de Documento.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                VentanaActual = Enumerables.Ventanas.Listas;
                var TiposDocumentosTask = this.iPresentacion?.Listar();
                TiposDocumentosTask!.Wait();

                this.TiposDocumentos = TiposDocumentosTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los Tipos de Documentos.";
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
                ViewData["Error"] = "Ocurrió un error al borrar el tipo del documento.";
            }
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                this.VentanaActual = Enumerables.Ventanas.Editar;

                var TipoDocumentoTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                TipoDocumentoTask!.Wait();

                

                this.TipoDocumento = TipoDocumentoTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el TipoDocumento para editar.";
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = this.iPresentacion?.Modificar(this.TipoDocumento!);
                modificarTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.TipoDocumento = modificarTask.Result;
                
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el tipo de documento para editar.";
            }
        }

        public void OnPostVistaCrear()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Crear;
                comprobarToken();
                

                this.TipoDocumento = new TiposDocumentos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la vista para crear un tipo de documento.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = this.iPresentacion?.Guardar(this.TipoDocumento!);
                crearTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Listas;
                OnPostListar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear el tipo de documento.";
            }
        }
    }
}
