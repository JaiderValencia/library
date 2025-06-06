﻿using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class NumerosDeSeriePrueba
    {
        private readonly IConexion? iConexion = new Conexion();
        private List<NumerosDeSerie>? lista;
        private NumerosDeSerie? entidad;

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
            this.lista = this.iConexion!.NumerosDeSerie!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.NumerosDeSerie()!;
            this.iConexion!.NumerosDeSerie!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.NumeroSerie = "Z9";

            var entry = this.iConexion!.Entry<NumerosDeSerie>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.NumerosDeSerie!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }

    }
}
