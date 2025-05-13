using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lib_repositorios.Migrations
{
    /// <inheritdoc />
    public partial class crear_todas_las_tablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estanterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estanterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreCorto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    Autor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libros_Autores_Autor",
                        column: x => x.Autor,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Libros_Categorias_Categoria",
                        column: x => x.Categoria,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Niveles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estanteria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Niveles_Estanterias_Estanteria",
                        column: x => x.Estanteria,
                        principalTable: "Estanterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDocumento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_TiposDocumentos_TipoDocumento",
                        column: x => x.TipoDocumento,
                        principalTable: "TiposDocumentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NumerosDeSerie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroSerie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Libro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumerosDeSerie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumerosDeSerie_Libros_Libro",
                        column: x => x.Libro,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Niveles_tiene_Libros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Libro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveles_tiene_Libros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Niveles_tiene_Libros_Libros_Libro",
                        column: x => x.Libro,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Niveles_tiene_Libros_Niveles_Nivel",
                        column: x => x.Nivel,
                        principalTable: "Niveles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntregado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cliente = table.Column<int>(type: "int", nullable: false),
                    NumeroSerie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestamos_Clientes_Cliente",
                        column: x => x.Cliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prestamos_NumerosDeSerie_NumeroSerie",
                        column: x => x.NumeroSerie,
                        principalTable: "NumerosDeSerie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TipoDocumento",
                table: "Clientes",
                column: "TipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_Autor",
                table: "Libros",
                column: "Autor");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_Categoria",
                table: "Libros",
                column: "Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Niveles_Estanteria",
                table: "Niveles",
                column: "Estanteria");

            migrationBuilder.CreateIndex(
                name: "IX_Niveles_tiene_Libros_Libro",
                table: "Niveles_tiene_Libros",
                column: "Libro");

            migrationBuilder.CreateIndex(
                name: "IX_Niveles_tiene_Libros_Nivel",
                table: "Niveles_tiene_Libros",
                column: "Nivel");

            migrationBuilder.CreateIndex(
                name: "IX_NumerosDeSerie_Libro",
                table: "NumerosDeSerie",
                column: "Libro");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_Cliente",
                table: "Prestamos",
                column: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_NumeroSerie",
                table: "Prestamos",
                column: "NumeroSerie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Niveles_tiene_Libros");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "Niveles");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "NumerosDeSerie");

            migrationBuilder.DropTable(
                name: "Estanterias");

            migrationBuilder.DropTable(
                name: "TiposDocumentos");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
