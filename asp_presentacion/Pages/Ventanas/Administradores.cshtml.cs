using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class AdministradoresModel : PageModel
    {
        private readonly IAdministradoresPresentacion? iPresentacion;
        private readonly IRolesPresentacion? rolesPresentacion;

        public List<Administradores>? Administradores { get; set; }
        public List<Roles>? Roles { get; set; }

        [BindProperty] public Administradores? Administrador { get; set; } = null;
        public Enumerables.Ventanas VentanaActual { get; set; } = Enumerables.Ventanas.Listas;

        public Dictionary<string, int> opcionesBuscador { get; } = new Dictionary<string, int>
        {
            { "PorId", 1 },
            { "PorNombre", 2 },            
        };

        public AdministradoresModel(IAdministradoresPresentacion? administradoresPresentacion, IRolesPresentacion? rolesPresentacion)
        {
            this.iPresentacion = administradoresPresentacion;
            this.rolesPresentacion = rolesPresentacion;
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
            this.rolesPresentacion?.ponerToken(token);
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
                    var administradorTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                    administradorTask!.Wait();
                

                    if (administradorTask.Result == null)
                    {
                        this.Administradores = null;
                        return;
                    }
                    
                    this.Administradores = new List<Administradores> { administradorTask.Result };
                }
                else if (opcion == this.opcionesBuscador["PorNombre"])
                {
                    var administradoresTask = this.iPresentacion?.PorNombre(data);
                    administradoresTask!.Wait();

                    this.Administradores = administradoresTask.Result!;
                }                
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al buscar administradores.";
            }
        }

        public void OnPostListar()
        {
            try
            {
                VentanaActual = Enumerables.Ventanas.Listas;
                var AdministradoresTask = this.iPresentacion?.Listar();
                AdministradoresTask!.Wait();

                this.Administradores = AdministradoresTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los administradores.";
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
                ViewData["Error"] = "Ocurrió un error al borrar el administrador.";
            }
        }

        public void OnPostVistaModificar(string data)
        {
            try
            {
                comprobarToken();
                this.VentanaActual = Enumerables.Ventanas.Editar;

                var administradorTask = this.iPresentacion?.PorId(Convert.ToInt32(data));
                administradorTask!.Wait();

                obtenerRoles();

                this.Administrador = administradorTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el administrador para editar.";
            }
        }

        public void obtenerRoles()
        {
            try
            {
                var rolesTask = this.rolesPresentacion?.Listar();
                rolesTask!.Wait();

                this.Roles = rolesTask.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar los roles para editar.";
            }
        }

        public void OnPostModificar()
        {
            try
            {
                comprobarToken();
                var modificarTask = this.iPresentacion?.Modificar(this.Administrador!);
                modificarTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Editar;
                this.Administrador = modificarTask.Result;
                obtenerRoles();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar el administrador para editar.";
            }
        }

        public void OnPostVistaCrear()
        {
            try
            {
                this.VentanaActual = Enumerables.Ventanas.Crear;
                comprobarToken();
                obtenerRoles();

                this.Administrador = new Administradores();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al cargar la vista para crear un administrador.";
            }
        }

        public void OnPostCrear()
        {
            try
            {
                comprobarToken();
                var crearTask = this.iPresentacion?.Guardar(this.Administrador!);
                crearTask!.Wait();

                this.VentanaActual = Enumerables.Ventanas.Listas;
                OnPostListar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ViewData["Error"] = "Ocurrió un error al crear el administrador.";
            }
        }
    }
}
