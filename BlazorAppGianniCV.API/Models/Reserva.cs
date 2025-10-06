using System;
using System.Collections.Generic;

namespace BlazorAppGianniCV.API.Models;

public partial class Reserva
{
    public int Iidreserva { get; set; }

    public int? Iidusuario { get; set; }

    public int? Numlibros { get; set; }

    public int? Iidestadoreserva { get; set; }

    public DateTime? Dfechareserva { get; set; }

    public DateTime? Dfechafinreserva { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<DetalleReserva> DetalleReservas { get; set; } = new List<DetalleReserva>();

    public virtual ReservaEstado? IidestadoreservaNavigation { get; set; }

    public virtual ICollection<ReservaHistorial> ReservaHistorials { get; set; } = new List<ReservaHistorial>();
}
