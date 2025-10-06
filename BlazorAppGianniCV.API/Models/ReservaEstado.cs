using System;
using System.Collections.Generic;

namespace BlazorAppGianniCV.API.Models;

public partial class ReservaEstado
{
    public int Iidestadoreserva { get; set; }

    public string? Vnombre { get; set; }

    public string? Vdescripcion { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<ReservaHistorial> ReservaHistorials { get; set; } = new List<ReservaHistorial>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
