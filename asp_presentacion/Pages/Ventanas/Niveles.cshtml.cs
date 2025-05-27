using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones.Interfaces;

namespace asp_presentacion.Pages.Ventanas
{
    public class NivelesModel : PageModel
    {
        private readonly INivelesPresentacion? iPresentacion;
        private readonly IEstanteriasPresentacion? EstanteriasPresentacion;

        public List<Niveles>? Niveles { get; set; }
        public List<Estanterias>? Estanterias { get; set; }

        [BindProperty] public Niveles? Nivel { get; set; } = null;
        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNombre", 2 },
        };

        public NivelesModel(INivelesPresentacion? NivelesPresentacion, IEstanteriasPresentacion? EstanteriasPresentacion)
        {
            this.iPresentacion = NivelesPresentacion;
            this.EstanteriasPresentacion = EstanteriasPresentacion;
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
            this.EstanteriasPresentacion?.ponerToken(token);
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
                    var NivelTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                    NivelTask!.Wait();


                    if (NivelTask.Result == null)
                    {
                        this.Niveles = null;
                        return;
                    }

                    this.Niveles = new List<Niveles> { NivelTask.Result };
                }
                else if (opcion == this.opcionesBuscador["PorNombre"])
                {
                    var NivelesTask = this.iPresentacion?.PorNombre(data);
                    NivelesTask!.Wait();

                    this.Niveles = NivelesTask.Result!;
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar Niveles.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                VentanaActual = Enumerables.Ventanas.Listas;
                var NivelesTask = this.iPresentacion?.Listar();
                NivelesTask!.Wait();

                this.Niveles = NivelesTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los Niveles.";
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
                ViewData["Error"] = "Ocurrió un error al borrar el Nivel.";
            }
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                this.VentanaActual = Enumerables.Ventanas.Editar;

                var NivelTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                NivelTask!.Wait();

                obtenerEstanterias();

                this.Nivel = NivelTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el Nivel para editar.";
            }
        }

        public void obtenerEstanterias()
        {
            try
            {
                var EstanteriasTask = this.EstanteriasPresentacion?.Listar();
                EstanteriasTask!.Wait();

                this.Estanterias = EstanteriasTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los Estanterias para editar.";
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = this.iPresentacion?.Modificar(this.Nivel!);
                modificarTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.Nivel = modificarTask.Result;
                obtenerEstanterias();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el Nivel para editar.";
            }
        }

        public void OnPostVistaCrear()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Crear;
                comprobarToken();
                obtenerEstanterias();

                this.Nivel = new Niveles();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la vista para crear un Nivel.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = this.iPresentacion?.Guardar(this.Nivel!);
                crearTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Listas;
                OnPostListar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear el Nivel.";
            }
        }
    }
}
