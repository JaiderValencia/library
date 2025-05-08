using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class Niveles_tiene_LibrosPrueba
    {
        private readonly IConexion? iConexion = new Conexion();
        private List<Niveles_tiene_Libros>? lista;
        private Niveles_tiene_Libros? entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Niveles_tiene_Libros!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Niveles_tiene_Libros()!;
            this.iConexion!.Niveles_tiene_Libros!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Libro = 2;

            var entry = this.iConexion!.Entry<Niveles_tiene_Libros>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Niveles_tiene_Libros!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }

    }
}
