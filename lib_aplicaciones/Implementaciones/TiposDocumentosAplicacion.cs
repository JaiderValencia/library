using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class TiposDocumentosAplicacion : ITiposDocumentosAplicacion
    {
        private Conexion conexion = new Conexion();

        public List<TiposDocumentos>? PorNombre(string nombre)
        {
            return this.conexion!.TiposDocumentos!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public TiposDocumentos? Borrar(int id)
        {
            var entidad = this.conexion.TiposDocumentos!.FirstOrDefault(Prestamos => Prestamos.Id == id);

            if (entidad == null)
                throw new Exception("No se guardó este nivel");

            this.conexion!.TiposDocumentos!.Remove(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public TiposDocumentos? Guardar(TiposDocumentos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            this.conexion!.TiposDocumentos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<TiposDocumentos> Listar()
        {
            return this.conexion!.TiposDocumentos!.Take(20).ToList();
        }

        public TiposDocumentos? PorId(int Id)
        {
            return this.conexion!.TiposDocumentos!.FirstOrDefault(x => x.Id == Id);
        }

        public TiposDocumentos? Modificar(TiposDocumentos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (entidad!.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.conexion!.Entry<TiposDocumentos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
