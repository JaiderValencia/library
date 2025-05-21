using System;
using System.Collections.Generic;
using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_repositorios.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Autores>? Autores { get; set; }
        DbSet<Categorias>? Categorias { get; set; }
        DbSet<Clientes>? Clientes { get; set; }
        DbSet<Estanterias>? Estanterias { get; set; }
        DbSet<Libros>? Libros { get; set; }
        DbSet<Niveles>? Niveles { get; set; }
        DbSet<NumerosDeSerie>? NumerosDeSerie { get; set; }
        DbSet<Niveles_tiene_Libros>? Niveles_tiene_Libros { get; set; }
        DbSet<Prestamos>? Prestamos { get; set; }
        DbSet<TiposDocumentos>? TiposDocumentos { get; set; }
        DbSet<Accesos>? Accesos { get; set; }
        DbSet<Administradores>? Administradores { get; set; }
        DbSet<Roles>? Roles { get; set; }
        DbSet<Roles_tiene_Accesos>? Roles_tiene_Accesos { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}