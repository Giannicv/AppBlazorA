using System;
using System.Collections.Generic;

namespace BlazorAppGianniCV.API.Models;

public partial class Pai
{
    public int Iidpais { get; set; }

    public string? Nombre { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<Autor> Autors { get; set; } = new List<Autor>();
}
