using System;
using System.Collections.Generic;

namespace BlazorAppGianniCV.API.Models;

public partial class TipoLibro
{
    public int Iidtipolibro { get; set; }

    public string? Nombretipolibro { get; set; }

    public string? Descripcion { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
