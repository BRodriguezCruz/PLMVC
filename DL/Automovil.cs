using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Automovil
    {
        public int IdAutomovil { get; set; }
        public string Modelo { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Generacion { get; set; } = null!;
        public string? Color { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int? IdAgencia { get; set; }

        public virtual Agencium? IdAgenciaNavigation { get; set; }
    }
}
