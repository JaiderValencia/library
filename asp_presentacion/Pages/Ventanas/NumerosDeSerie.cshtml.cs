using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones.Interfaces;

namespace asp_presentacion.Pages.Ventanas
{
    public class NumerosDeSerieModel : PageModel
    {
        private readonly INumerosDeSeriePresentacion? iPresentacion;
        private readonly ILibrosPresentacion? LibrosPresentacion;

        public List<NumerosDeSerie>? NumerosDeSerie { get; set; }
        public List<Libros>? Libros { get; set; }

        [BindProperty] public NumerosDeSerie? NumeroDeSerie { get; set; } = null;
        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNumeroSerie", 2 },
        };

        public NumerosDeSerieModel(INumerosDeSeriePresentacion? NumerosDeSeriePresentacion, ILibrosPresentacion? LibrosPresentacion)
        {
            this.iPresentacion = NumerosDeSeriePresentacion;
            this.LibrosPresentacion = LibrosPresentacion;
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
            this.LibrosPresentacion?.ponerToken(token);
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
                    var NumeroDeSerieTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                    NumeroDeSerieTask!.Wait();


                    if (NumeroDeSerieTask.Result == null)
                    {
                        this.NumerosDeSerie = null;
                        return;
                    }

                    this.NumerosDeSerie = new List<NumerosDeSerie> { NumeroDeSerieTask.Result };
                }
                else if (opcion == this.opcionesBuscador["PorNumeroSerie"])
                {
                    var NumerosDeSerieTask = this.iPresentacion?.PorNumeroSerie(data);
                    NumerosDeSerieTask!.Wait();

                    this.NumerosDeSerie = NumerosDeSerieTask.Result!;
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar Numero De Serie.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                VentanaActual = Enumerables.Ventanas.Listas;
                var NumerosDeSerieTask = this.iPresentacion?.Listar();
                NumerosDeSerieTask!.Wait();

                this.NumerosDeSerie = NumerosDeSerieTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los Numeros De Serie.";
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
                ViewData["Error"] = "Ocurrió un error al borrar el Numero De Serie.";
            }
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                this.VentanaActual = Enumerables.Ventanas.Editar;

                var NumeroDeSerieTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                NumeroDeSerieTask!.Wait();

                obtenerLibros();

                this.NumeroDeSerie = NumeroDeSerieTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el Numero De Serie para editar.";
            }
        }

        public void obtenerLibros()
        {
            try
            {
                var LibrosTask = this.LibrosPresentacion?.Listar();
                LibrosTask!.Wait();

                this.Libros = LibrosTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los Libros para editar.";
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = this.iPresentacion?.Modificar(this.NumeroDeSerie!);
                modificarTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.NumeroDeSerie = modificarTask.Result;
                obtenerLibros();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el Numero De Serie para editar.";
            }
        }

        public void OnPostVistaCrear()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Crear;
                comprobarToken();
                obtenerLibros();

                this.NumeroDeSerie = new NumerosDeSerie();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la vista para crear un Numero De Serie.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = this.iPresentacion?.Guardar(this.NumeroDeSerie!);
                crearTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Listas;
                OnPostListar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear el Numero De Serie.";
            }
        }
    }
}
