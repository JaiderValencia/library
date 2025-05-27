using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class CategoriasModel : PageModel
    {
        private readonly ICategoriasPresentacion? iPresentacion;

        public List<Categorias>? Categorias { get; set; }

        [BindProperty] public Categorias? Categoria { get; set; } = null;

        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNombre", 2 },
        };

        public CategoriasModel(ICategoriasPresentacion? categoriasPresentacion)
        {
            this.iPresentacion = categoriasPresentacion;
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
                    var CategoriaTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                    CategoriaTask!.Wait();


                    if (CategoriaTask.Result == null)
                    {
                        this.Categorias = null;
                        return;
                    }

                    this.Categorias = new List<Categorias> { CategoriaTask.Result };
                }
                else if (opcion == this.opcionesBuscador["PorNombre"])
                {
                    var CategoriasTask = this.iPresentacion?.PorNombre(data);
                    CategoriasTask!.Wait();

                    this.Categorias = CategoriasTask.Result!;
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar categorias.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Listas;
                var CategoriasTask = this.iPresentacion?.Listar();
                CategoriasTask?.Wait();

                this.Categorias = CategoriasTask?.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, $"Error al listar categorias: {ex.Message}");
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
                ViewData["Error"] = "Ocurrió un error al borrar la categoria.";
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

                this.Categoria = autorTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la categoria para editar.";
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = this.iPresentacion?.Modificar(this.Categoria!);
                modificarTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.Categoria = modificarTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la categoria para editar.";
            }
        }

        public void OnPostVistaCrear()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Crear;
                comprobarToken();

                this.Categoria = new Categorias();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la vista para crear una categoria.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = this.iPresentacion?.Guardar(this.Categoria!);
                crearTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Listas;
                OnPostListar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear la categoria.";
            }
        }
    }
}
