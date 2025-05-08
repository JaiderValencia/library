using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using lib_repositorios.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        public Conexion()
        {
            StringConexion = Configuracion.ObtenerValor("StringConexion");
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Autores>? Autores { get; set; }
        public DbSet<Categorias>? Categorias { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Estanterias>? Estanterias { get; set; }
        public DbSet<Libros>? Libros { get; set; }
        public DbSet<Niveles>? Niveles { get; set; }
        public DbSet<NumerosDeSerie>? NumerosDeSerie { get; set; }
        public DbSet<Niveles_tiene_Libros>? Niveles_tiene_Libros { get; set; }
        public DbSet<Prestamos>? Prestamos { get; set; }
        public DbSet<TiposDocumentos>? TiposDocumentos { get; set; }
    }
}
