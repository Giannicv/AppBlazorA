using System;
using System.Collections.Generic;

namespace BlazorAppGianniCV.API.Models;

public partial class Button
{
    public int Iidbutton { get; set; }

    public string? Nombrebutton { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<PaginaTipoUsuButton> PaginaTipoUsuButtons { get; set; } = new List<PaginaTipoUsuButton>();
}
