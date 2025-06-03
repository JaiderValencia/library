using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class RolesModel : PageModel
    {
        private readonly IRolesPresentacion? iPresentacion;
        private readonly IAccesosPresentacion? accesosPresentacion;

        public List<Roles>? Roles { get; set; }
        public List<Accesos>? Accesos { get; set; }

        [BindProperty] public Roles? Rol { get; set; } = null;
        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public List<string> opcionesAcciones = new List<string>
        {
            "Listar",
            "Borrar",
            "Crear",
            "Guardar"
        };

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNombre", 2 }
        };
        public RolesModel(IRolesPresentacion? RolesPresentacion, IAccesosPresentacion? accesosPresentacion)
        {
            this.iPresentacion = RolesPresentacion;
            this.accesosPresentacion = accesosPresentacion;
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
                    var RolTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                    RolTask!.Wait();


                    if (RolTask.Result == null)
                    {
                        this.Rol = null;
                        return;
                    }

                    this.Roles = new List<Roles> { RolTask.Result };
                }
                else if (opcion == this.opcionesBuscador["PorNombre"])
                {
                    var RolesTask = this.iPresentacion?.PorNombre(data);
                    RolesTask!.Wait();

                    this.Roles = RolesTask.Result!;
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Al parecer no se encuentra :(.";
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
            this.accesosPresentacion?.ponerToken(token);
        }

        private void obtenerAccesos()
        {
            try
            {
                var accesosTask = this.accesosPresentacion!.Listar();
                accesosTask.Wait();

                this.Accesos = accesosTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los Accesos.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                VentanaActual = Enumerables.Ventanas.Listas;
                var RolesTask = iPresentacion?.Listar();
                RolesTask!.Wait();

                Roles = RolesTask.Result;
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
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al borrar el Rol.";
            }
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                this.VentanaActual = Enumerables.Ventanas.Editar;

                obtenerAccesos();
                var RolTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                RolTask!.Wait();

                this.Rol = RolTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el rol para editar.";
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();

                var modificarTask = this.iPresentacion?.Modificar(this.Rol!);
                modificarTask!.Wait();

                obtenerAccesos();
                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.Rol = modificarTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el Rol para editar.";
            }
        }

        public void OnPostVistaCrear()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Crear;
                comprobarToken();
                obtenerAccesos();
                this.Rol = new Roles();
            }
            catch (Exception ex)
            {

                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la vista para crear un Rol.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = this.iPresentacion?.Guardar(this.Rol!);
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
