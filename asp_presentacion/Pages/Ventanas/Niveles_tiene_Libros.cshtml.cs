using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones.Interfaces;

namespace asp_presentacion.Pages.Ventanas
{
    public class Niveles_tiene_LibrosModel : PageModel
    {
        private readonly INiveles_tiene_LibrosPresentacion? iPresentacion;
        private readonly ILibrosPresentacion? LibrosPresentacion;
        private readonly INivelesPresentacion? NivelesPresentacion;

        public List<Niveles_tiene_Libros>? Niveles_tiene_Libros { get; set; }
        public List<Libros>? Libros { get; set; }
        public List<Niveles>? Niveles { get; set; }

        [BindProperty] public Niveles_tiene_Libros? Nivel_tiene_Libro { get; set; } = null;
        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
        };

        public Niveles_tiene_LibrosModel(INiveles_tiene_LibrosPresentacion? Niveles_tiene_LibrosPresentacion, ILibrosPresentacion? LibrosPresentacion, INivelesPresentacion? NivelesPresentacion)
        {
            this.iPresentacion = Niveles_tiene_LibrosPresentacion;
            this.LibrosPresentacion = LibrosPresentacion;
            this.NivelesPresentacion = NivelesPresentacion;
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
            this.NivelesPresentacion?.ponerToken(token);
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
                    var Nivel_tiene_LibroTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                    Nivel_tiene_LibroTask!.Wait();


                    if (Nivel_tiene_LibroTask.Result == null)
                    {
                        this.Niveles_tiene_Libros = null;
                        return;
                    }

                    this.Niveles_tiene_Libros = new List<Niveles_tiene_Libros> { Nivel_tiene_LibroTask.Result };
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
                var Niveles_tiene_LibrosTask = this.iPresentacion?.Listar();
                Niveles_tiene_LibrosTask!.Wait();

                this.Niveles_tiene_Libros = Niveles_tiene_LibrosTask.Result;
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

                var Nivel_tiene_LibroTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                Nivel_tiene_LibroTask!.Wait();

                obtenerNiveles();
                obtenerLibros();

                this.Nivel_tiene_Libro = Nivel_tiene_LibroTask.Result;
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

        public void obtenerNiveles()
        {
            try
            {
                var NivelesTask = this.NivelesPresentacion?.Listar();
                NivelesTask!.Wait();

                this.Niveles = NivelesTask.Result;
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
                var modificarTask = this.iPresentacion?.Modificar(this.Nivel_tiene_Libro!);
                modificarTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.Nivel_tiene_Libro = modificarTask.Result;
                obtenerLibros();
                obtenerNiveles();
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
                obtenerNiveles();

                this.Nivel_tiene_Libro = new Niveles_tiene_Libros();
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
                var crearTask = this.iPresentacion?.Guardar(this.Nivel_tiene_Libro!);
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
