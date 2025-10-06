using System;
using System.Collections.Generic;

namespace BlazorAppGianniCV.API.Models;

public partial class Libro
{
    public int Iidlibro { get; set; }

    public string? Titulo { get; set; }

    public string? Resumen { get; set; }

    public int? Numpaginas { get; set; }

    public int? Stock { get; set; }

    public int? Bhabilitado { get; set; }

    public int? Iidautor { get; set; }

    public int? Iidtipolibro { get; set; }

    public byte[]? Fotocaratula { get; set; }

    public byte[]? Libropdf { get; set; }

    public string? Nombrearchivo { get; set; }

    public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();

    public virtual Autor? IidautorNavigation { get; set; }

    public virtual TipoLibro? IidtipolibroNavigation { get; set; }
}
