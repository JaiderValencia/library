using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class LibrosModel : PageModel
    {
        private readonly ILibrosPresentacion? iPresentacion;
        private readonly ICategoriasPresentacion? iCategoriasPresentacion;
        private readonly IAutoresPresentacion? iAutoresPresentacion;

        public List<Libros>? Libros { get; set; }
        public List<Categorias>? Categorias { get; set; }
        public List<Autores>? Autores { get; set; }
        [BindProperty] public Libros? Libro { get; set; } = null;

        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNombre", 2 }
        };

        public LibrosModel(
            ILibrosPresentacion? librosPresentacion,
            ICategoriasPresentacion? categoriasPresentacion,
            IAutoresPresentacion? autoresPresentacion)
        {
            this.iPresentacion = librosPresentacion;
            this.iCategoriasPresentacion = categoriasPresentacion;
            this.iAutoresPresentacion = autoresPresentacion;
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
            this.iCategoriasPresentacion?.ponerToken(token);
            this.iAutoresPresentacion?.ponerToken(token);
        }

        public void ObtenerCategorias()
        {
            try
            {                
                var categoriasTask = iCategoriasPresentacion?.Listar();
                categoriasTask!.Wait();
                this.Categorias = categoriasTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar las categorías.";
            }
        }

        public void ObtenerAutores()
        {
            try
            {
                var autoresTask = iAutoresPresentacion?.Listar();
                autoresTask!.Wait();
                this.Autores = autoresTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los autores.";
            }
        }

        public void OnPostBuscar(string data, int opcion)
        {
            try
            {
                comprobarToken();
                VentanaActual = Enumerables.Ventanas.Listas;

                if (opcion == opcionesBuscador["PorId"])
                {
                    var libroTask = iPresentacion?.PorId(Convert.ToInt32(data));
                    libroTask!.Wait();
                    if (libroTask.Result == null)
                    {
                        Libros = null;
                        return;
                    }
                    Libros = new List<Libros> { libroTask.Result };
                }
                else if (opcion == opcionesBuscador["PorNombre"])
                {
                    var librosTask = iPresentacion?.PorNombre(data);
                    librosTask!.Wait();
                    Libros = librosTask.Result!;
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar libros.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                VentanaActual = Enumerables.Ventanas.Listas;
                var librosTask = iPresentacion?.Listar();
                librosTask!.Wait();
                Libros = librosTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los libros.";
            }
        }

        public void OnPostVistaCrear()
        {                        
            comprobarToken();
            VentanaActual = Enumerables.Ventanas.Crear;
            Libro = new Libros();
            ObtenerCategorias();
            ObtenerAutores();
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                var libroTask = iPresentacion?.PorId(Convert.ToInt32(data));
                libroTask!.Wait();
                Libro = libroTask.Result;
                VentanaActual = Enumerables.Ventanas.Editar;
                ObtenerCategorias();
                ObtenerAutores();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el libro para editar.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = iPresentacion?.Guardar(Libro!);
                crearTask!.Wait();
                OnPostListar();                
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear el libro.";                                
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = iPresentacion?.Modificar(Libro!);
                modificarTask!.Wait();
                ObtenerAutores();
                ObtenerCategorias();
                
                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.Libro = modificarTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al modificar el libro.";
                ObtenerCategorias();
                ObtenerAutores();
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
                ViewData["Error"] = "Ocurrió un error al borrar el libro.";                
            }
        }
    }
}