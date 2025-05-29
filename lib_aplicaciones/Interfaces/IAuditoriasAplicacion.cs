using lib_dominio.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IAuditoriasAplicacion
    {
        public List<Auditorias> Listar();
        public void Guardar(Auditorias auditoria);

        public List<Auditorias> PorAdministrador(string Administrador);
        
        public List<Auditorias> PorFecha(DateTime fechaDesde, DateTime fechaHasta);
    }
}
