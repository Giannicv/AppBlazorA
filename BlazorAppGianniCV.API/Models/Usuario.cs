using System;
using System.Collections.Generic;

namespace BlazorAppGianniCV.API.Models;

public partial class Usuario
{
    public int Iidusuario { get; set; }

    public string? Nombreusuario { get; set; }

    public string? Contra { get; set; }

    public int? Iidpersona { get; set; }

    public int? Bhabilitado { get; set; }

    public int? Iidtipousuario { get; set; }

    public virtual Persona? IidpersonaNavigation { get; set; }

    public virtual TipoUsuario? IidtipousuarioNavigation { get; set; }

    public virtual ICollection<ReservaHistorial> ReservaHistorials { get; set; } = new List<ReservaHistorial>();
}
