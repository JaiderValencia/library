using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;

namespace lib_aplicaciones.Implementaciones
{
    public class AuditoriasAplicacion : IAuditoriasAplicacion
    {
        private readonly Conexion conexion = new Conexion();

        public List<Auditorias> Listar()
        {
            return this.conexion.Auditorias!.ToList();
        }

        public void Guardar(Auditorias auditoria)
        {
            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();
        }

        public List<Auditorias> PorAdministrador(string Administrador)
        {
            return this.conexion.Auditorias!
                .Where(Auditoria => Auditoria.Administrador!.ToLower().Contains(Administrador.ToLower()))
                .ToList();
        }

        public List<Auditorias> PorFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            return this.conexion.Auditorias!
                .Where(Auditoria => Auditoria.Fecha >= fechaDesde && Auditoria.Fecha <= fechaHasta)
                .ToList();
        }
    }
}
