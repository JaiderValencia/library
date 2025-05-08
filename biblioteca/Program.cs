// See https://aka.ms/new-console-template for more information

// var niveles = listaEstanterias.Where(estanteria => estanteria.id == 4).ToList();

List<Estanterias> listaEstanterias = new List<Estanterias>();
List<Niveles> listaNiveles = new List<Niveles>();
List<Categorias> listaCategorias = new List<Categorias>();
List<Autores> listaAutores = new List<Autores>();
List<Libros> listaLibros = new List<Libros>();
List<NumerosDeSeries> listaNumerosDeSeries = new List<NumerosDeSeries>();
List<Niveles_tiene_Libros> listaNiveles_tiene_Libros = new List<Niveles_tiene_Libros>();
List<TiposDocumentos> listaTiposDocumentos = new List<TiposDocumentos>();
List<Clientes> listaClientes = new List<Clientes>();
List<Prestamos> listaPrestamos = new List<Prestamos>();

listaEstanterias.Add(new Estanterias()
{
    id = 1,
    nombre = "5C",
    activo = true
});

listaEstanterias.Add(new Estanterias()
{
    id = 2,
    nombre = "4R",
    activo = true
});

listaEstanterias.Add(new Estanterias()
{
    id = 3,
    nombre = "3S",
    activo = true
});

listaEstanterias.Add(new Estanterias()
{
    id = 4,
    nombre = "45F",
    activo = true
});

listaNiveles.Add(new Niveles()
{
    id = 1,
    nombre = "5",
    Estanteria = 1,
    _Estanteria = listaEstanterias.First(estanteria => estanteria.id == 1)
});

listaNiveles.Add(new Niveles()
{
    id = 2,
    nombre = "2",
    Estanteria = 2,
    _Estanteria = listaEstanterias.First(estanteria => estanteria.id == 2)
});

listaNiveles.Add(new Niveles()
{
    id = 3,
    nombre = "3",
    Estanteria = 3,
    _Estanteria = listaEstanterias.First(estanteria => estanteria.id == 3)
});

listaNiveles.Add(new Niveles()
{
    id = 4,
    nombre = "6",
    Estanteria = 4,
    _Estanteria = listaEstanterias.First(estanteria => estanteria.id == 4)
});

listaCategorias.Add(new Categorias()
{
    id = 1,
    nombre = "Infantil",
    descripcion = "Libros para niños entre 5 y 12 años"
});

listaCategorias.Add(new Categorias()
{
    id = 2,
    nombre = "Educación",
    descripcion = "Libros para bachillerato y universidad",
});

listaCategorias.Add(new Categorias()
{
    id = 3,
    nombre = "Fantasia",
    descripcion = "Libros para todas las edades",
});

listaCategorias.Add(new Categorias()
{
    id = 4,
    nombre = "Novela",
    descripcion = "Libros de historias dramaticas",
});

listaAutores.Add(new Autores()
{
    id = 1,
    nombre = "Antonie de Saint-Exupéry",
    fechaNacimiento = new DateOnly(1900, 6, 29),
});

listaAutores.Add(new Autores()
{
    id = 2,
    nombre = "Aurelio Baldor",
    fechaNacimiento = new DateOnly(1906, 10, 22),
});

listaAutores.Add(new Autores()
{
    id = 3,
    nombre = "J.K Rowling",
    fechaNacimiento = new DateOnly(1965, 7, 31),
});

listaAutores.Add(new Autores()
{
    id = 4,
    nombre = "J.R.R Tolkien",
    fechaNacimiento = new DateOnly(1982, 1, 3),
});

listaLibros.Add(new Libros()
{
    id = 1,
    nombre = "El principito",
    fecha = new DateOnly(1943, 4, 6),
    Categoria = 1,
    Autor = 1,
    _Categoria = listaCategorias.First(categoria => categoria.id == 1),
    _Autor = listaAutores.First(autor => autor.id == 1)
});

listaLibros.Add(new Libros()
{
    id = 2,
    nombre = "Algebra",
    fecha = new DateOnly(1941, 6, 19),
    Categoria = 2,
    Autor = 2,
    _Categoria = listaCategorias.First(categoria => categoria.id == 2),
    _Autor = listaAutores.First(autor => autor.id == 2)
});

listaLibros.Add(new Libros()
{
    id = 3,
    nombre = "Harry Potter",
    fecha = new DateOnly(1997, 6, 19),
    Categoria = 3,
    Autor = 3,
    _Categoria = listaCategorias.First(categoria => categoria.id == 3),
    _Autor = listaAutores.First(autor => autor.id == 3)
});

listaLibros.Add(new Libros()
{
    id = 4,
    nombre = "El señor de los anillos",
    fecha = new DateOnly(1954, 6, 29),
    Categoria = 3,
    Autor = 4,
    _Categoria = listaCategorias.First(categoria => categoria.id == 3),
    _Autor = listaAutores.First(autor => autor.id == 4)
});

listaNumerosDeSeries.Add(new NumerosDeSeries()
{
    id = 1,
    numeroSeries = "P12",
    Libro = 1,
    _Libro = listaLibros.First(libro => libro.id == 1)
});

listaNumerosDeSeries.Add(new NumerosDeSeries()
{
    id = 2,
    numeroSeries = "A5",
    Libro = 2,
    _Libro = listaLibros.First(libro => libro.id == 2)
});

listaNumerosDeSeries.Add(new NumerosDeSeries()
{
    id = 3,
    numeroSeries = "H46",
    Libro = 4,
    _Libro = listaLibros.First(libro => libro.id == 4)
});

listaNumerosDeSeries.Add(new NumerosDeSeries()
{
    id = 4,
    numeroSeries = "E44",
    Libro = 3,
    _Libro = listaLibros.First(libro => libro.id == 3)
});

listaNiveles_tiene_Libros.Add(new Niveles_tiene_Libros()
{
    id = 1,
    Nivel = 1,
    Libro = 1,
    _Nivel = listaNiveles.First(nivel => nivel.id == 1),
    _Libro = listaLibros.First(libro => libro.id == 1)
});

listaNiveles_tiene_Libros.Add(new Niveles_tiene_Libros()
{
    id = 2,
    Nivel = 2,
    Libro = 2,
    _Nivel = listaNiveles.First(nivel => nivel.id == 2),
    _Libro = listaLibros.First(libro => libro.id == 2)
});

listaNiveles_tiene_Libros.Add(new Niveles_tiene_Libros()
{
    id = 3,
    Nivel = 3,
    Libro = 3,
    _Nivel = listaNiveles.First(nivel => nivel.id == 3),
    _Libro = listaLibros.First(libro => libro.id == 3)
});

listaNiveles_tiene_Libros.Add(new Niveles_tiene_Libros()
{
    id = 4,
    Nivel = 4,
    Libro = 4,
    _Nivel = listaNiveles.First(nivel => nivel.id == 4),
    _Libro = listaLibros.First(libro => libro.id == 4)
});

listaTiposDocumentos.Add(new TiposDocumentos()
{
    id = 1,
    nombre = "Cédula",
    nombreCorto = "CC"
});

listaTiposDocumentos.Add(new TiposDocumentos()
{
    id = 2,
    nombre = "Tarjeta de indentidad",
    nombreCorto = "T.I"
});

listaTiposDocumentos.Add(new TiposDocumentos()
{
    id = 3,
    nombre = "Pasaporte",
    nombreCorto = "P.P"
});

listaTiposDocumentos.Add(new TiposDocumentos()
{
    id = 4,
    nombre = "Cédula de extranjería",
    nombreCorto = "C.E"
});

listaClientes.Add(new Clientes()
{
    id = 1,
    nombre = "Jose",
    apellido = "Henao",
    documento = "123456789",
    direccion = "Calle 20 #40-21",
    correo = "correo@contacto.com",
    telefono = "32124132",
    TipoDocumento = 1,
    _TipoDocumento = listaTiposDocumentos.First(tipoDocumento => tipoDocumento.id == 1)
});

listaClientes.Add(new Clientes()
{
    id = 2,
    nombre = "Samantha",
    apellido = "Diaz",
    documento = "1234256789",
    direccion = "Carrera 19 #3-17",
    correo = "correo2@contacto.com",
    telefono = "12345",
    TipoDocumento = 1,
    _TipoDocumento = listaTiposDocumentos.First(tipoDocumento => tipoDocumento.id == 1)
});

listaClientes.Add(new Clientes()
{
    id = 3,
    nombre = "Miguel",
    apellido = "Yagual",
    documento = "31028412",
    direccion = "Calle 92 #20-20",
    correo = "correo3@contacto.com",
    telefono = "31982137",
    TipoDocumento = 4,
    _TipoDocumento = listaTiposDocumentos.First(tipoDocumento => tipoDocumento.id == 4)
});

listaClientes.Add(new Clientes()
{
    id = 4,
    nombre = "Sulay",
    apellido = "Zapata",
    documento = "1593753",
    direccion = "Calle 50 #2-10",
    correo = "correo4@contacto.com",
    telefono = "11312137",
    TipoDocumento = 2,
    _TipoDocumento = listaTiposDocumentos.First(tipoDocumento => tipoDocumento.id == 2)
});

listaPrestamos.Add(new Prestamos()
{
    id = 1,
    fechaInicio = new DateTime(2024, 05, 12, 13, 20, 20),
    fechaFinal = new DateTime(2024, 05, 12, 13, 20, 20).AddMonths(1),
    fechaEntregado = new DateTime(2024, 05, 18, 12, 20, 31),
    Libro = 1,
    Cliente = 1,
    _Libro = listaLibros.First(libro => libro.id == 1),
    _Cliente = listaClientes.First(cliente => cliente.id == 1)
});

listaPrestamos.Add(new Prestamos()
{
    id = 2,
    fechaInicio = new DateTime(2024, 06, 11, 13, 20, 20),
    fechaFinal = new DateTime(2024, 06, 11, 13, 20, 20).AddMonths(1),
    fechaEntregado = new DateTime(2024, 06, 11, 13, 20, 20).AddMonths(1),
    Libro = 2,
    Cliente = 2,
    _Libro = listaLibros.First(libro => libro.id == 2),
    _Cliente = listaClientes.First(cliente => cliente.id == 2)
});

listaPrestamos.Add(new Prestamos()
{
    id = 3,
    fechaInicio = new DateTime(2024, 06, 11, 13, 20, 20),
    fechaFinal = new DateTime(2025, 08, 1, 9, 40, 00),
    fechaEntregado = new DateTime(2024, 07, 30, 18, 30, 55),
    Libro = 3,
    Cliente = 3,
    _Libro = listaLibros.First(libro => libro.id == 3),
    _Cliente = listaClientes.First(cliente => cliente.id == 3)
});

listaPrestamos.Add(new Prestamos()
{
    id = 4,
    fechaInicio = new DateTime(2024, 04, 08, 7, 20, 20),
    fechaFinal = new DateTime(2024, 04, 08, 7, 20, 20).AddMonths(1),
    fechaEntregado = null,
    Libro = 4,
    Cliente = 4,
    _Libro = listaLibros.First(libro => libro.id == 4),
    _Cliente = listaClientes.First(cliente => cliente.id == 4)
});

foreach (var estanteria in listaEstanterias)
{
    estanteria._Niveles = listaNiveles.Where(nivel => nivel.id == estanteria.id).ToList();
}

foreach (var nivel in listaNiveles)
{
    nivel._Libros = listaNiveles_tiene_Libros.Where(nivelLibro => nivelLibro.Nivel == nivel.id).Select(nivelLibro => nivelLibro._Libro).ToList();
}

foreach (var libro in listaLibros)
{
    libro._NumerosDeSeries = listaNumerosDeSeries.Where(numeroSerie => numeroSerie.Libro == libro.id).ToList();
    libro._Niveles = listaNiveles_tiene_Libros.Where(nivelLibro => nivelLibro.Libro == libro.id).Select(nivelLibro => nivelLibro._Nivel).ToList();
}

foreach (var categoria in listaCategorias)
{
    categoria._Libros = listaLibros.Where(libro => libro.Categoria == categoria.id).ToList();
}

foreach (var autor in listaAutores)
{
    autor._Libros = listaLibros.Where(libro => libro.Autor == autor.id).ToList();
}

foreach (var NumeroDeSeries in listaNumerosDeSeries)
{
    NumeroDeSeries._Prestamos = listaPrestamos.Where(prestamo => prestamo.Libro == NumeroDeSeries.id).ToList();
}

foreach (var cliente in listaClientes)
{
    cliente._Prestamos = listaPrestamos.Where(prestamo => prestamo.Cliente == cliente.id).ToList();
}

foreach (var tipoDocumento in listaTiposDocumentos)
{
    tipoDocumento._Clientes = listaClientes.Where(cliente => cliente.TipoDocumento == tipoDocumento.id).ToList();
}

public class Estanterias
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public Boolean activo { get; set; }

    public List<Niveles>? _Niveles { get; set; }
}

public class Niveles
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public int Estanteria { get; set; }

    public Estanterias? _Estanteria { get; set; }
    public List<Libros>? _Libros { get; set; }
}

public class Categorias
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public string? descripcion { get; set; }

    public List<Libros>? _Libros { get; set; }
}

public class Autores
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public string? apellido { get; set; }

    public DateOnly fechaNacimiento { get; set; }

    public List<Libros>? _Libros { get; set; }
}

public class Libros
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public DateOnly fecha { get; set; }
    public int Categoria { get; set; }
    public int Autor { get; set; }

    public Categorias? _Categoria { get; set; }
    public Autores? _Autor { get; set; }

    public List<NumerosDeSeries>? _NumerosDeSeries { get; set; }
    public List<Niveles>? _Niveles { get; set; }
}

public class NumerosDeSeries
{
    public int id { get; set; }
    public string? numeroSeries { get; set; }
    public int Libro { get; set; }

    public Libros? _Libro { get; set; }
    public List<Prestamos>? _Prestamos { get; set; }
}

public class Niveles_tiene_Libros
{
    public int id { get; set; }
    public int Nivel { get; set; }
    public int Libro { get; set; }

    public Niveles? _Nivel { get; set; }
    public Libros? _Libro { get; set; }
}

public class TiposDocumentos
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public string? nombreCorto { get; set; }

    public List<Clientes>? _Clientes { get; set; }
}

public class Clientes
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public string? apellido { get; set; }
    public string? documento { get; set; }
    public string? direccion { get; set; }
    public string? telefono { get; set; }
    public string? correo { get; set; }
    public int TipoDocumento { get; set; }

    public TiposDocumentos? _TipoDocumento { get; set; }
    public List<Prestamos>? _Prestamos { get; set; }
}

public class Prestamos
{
    public int id { get; set; }
    public DateTime fechaInicio { get; set; }
    public DateTime fechaFinal { get; set; }
    public DateTime? fechaEntregado { get; set; }
    public int Cliente { get; set; }
    public int Libro { get; set; }

    public Clientes? _Cliente { get; set; }
    public Libros? _Libro { get; set; }
}