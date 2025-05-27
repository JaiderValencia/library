using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones.Interfaces;

namespace asp_presentacion.Pages.Ventanas
{
    public class PrestamosModel : PageModel
    {
        private readonly IPrestamosPresentacion? iPresentacion;
        private readonly IClientesPresentacion? ClientesPresentacion;
        private readonly INumerosDeSeriePresentacion? NumerosDeSeriePresentacion;

        public List<Prestamos>? Prestamos { get; set; }
        public List<Clientes>? Clientes { get; set; }
        public List<NumerosDeSerie>? NumerosDeSerie { get; set; }

        [BindProperty] public Prestamos? Prestamo { get; set; } = null;
        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorFechaInicio", 2 }
        };

        public PrestamosModel(IPrestamosPresentacion? PrestamosPresentacion, IClientesPresentacion? ClientesPresentacion, INumerosDeSeriePresentacion? NumerosDeSeriePresentacion)
        {
            this.iPresentacion = PrestamosPresentacion;
            this.ClientesPresentacion = ClientesPresentacion;
            this.NumerosDeSeriePresentacion = NumerosDeSeriePresentacion;
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
            this.ClientesPresentacion?.ponerToken(token);
            this.NumerosDeSeriePresentacion?.ponerToken(token);
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
                    var PrestamoTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                    PrestamoTask!.Wait();


                    if (PrestamoTask.Result == null)
                    {
                        this.Prestamos = null;
                        return;
                    }

                    this.Prestamos = new List<Prestamos> { PrestamoTask.Result };
                }
                else if (opcion == this.opcionesBuscador["PorFechaInicio"])
                {
                    var PrestamosTask = this.iPresentacion?.PorFechaInicio(Convert.ToDateTime(data));
                    PrestamosTask!.Wait();

                    this.Prestamos = PrestamosTask.Result!;
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar Prestamo.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                VentanaActual = Enumerables.Ventanas.Listas;
                var PrestamosTask = this.iPresentacion?.Listar();
                PrestamosTask!.Wait();

                this.Prestamos = PrestamosTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los Prestamos.";
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
                ViewData["Error"] = "Ocurrió un error al borrar el Prestamo.";
            }
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                this.VentanaActual = Enumerables.Ventanas.Editar;

                var PrestamoTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                PrestamoTask!.Wait();

                obtenerNumerosDeSerie();
                obtenerClientes();

                this.Prestamo = PrestamoTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el Prestamo para editar.";
            }
        }

        public void obtenerClientes()
        {
            try
            {
                var ClientesTask = this.ClientesPresentacion?.Listar(1);
                ClientesTask!.Wait();

                this.Clientes = ClientesTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los Clientes para editar.";
            }
        }

        public void obtenerNumerosDeSerie()
        {
            try
            {
                var NumerosDeSerieTask = this.NumerosDeSeriePresentacion?.Listar();
                NumerosDeSerieTask!.Wait();

                this.NumerosDeSerie = NumerosDeSerieTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los Numeros de serie para editar.";
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = this.iPresentacion?.Modificar(this.Prestamo!);
                modificarTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.Prestamo = modificarTask.Result;
                obtenerClientes();
                obtenerNumerosDeSerie();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el Prestamo para editar.";
            }
        }

        public void OnPostVistaCrear()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Crear;
                comprobarToken();
                obtenerClientes();
                obtenerNumerosDeSerie();

                this.Prestamo = new Prestamos();
                this.Prestamo.FechaInicio = DateTime.Now;
                this.Prestamo.FechaFinal = DateTime.Now.AddDays(7);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la vista para crear un Prestamo.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = this.iPresentacion?.Guardar(this.Prestamo!);
                crearTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Listas;
                OnPostListar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear el Prestamo.";
            }
        }
    }
}
