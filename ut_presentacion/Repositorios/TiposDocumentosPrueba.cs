using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class TiposDocumentosPrueba
    {
        private readonly IConexion? iConexion = new Conexion();
        private List<TiposDocumentos>? lista;
        private TiposDocumentos? entidad;

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
            this.lista = this.iConexion!.TiposDocumentos!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.TiposDocumentos()!;
            this.iConexion!.TiposDocumentos!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.NombreCorto = "IDK";

            var entry = this.iConexion!.Entry<TiposDocumentos>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.TiposDocumentos!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }

    }
}
