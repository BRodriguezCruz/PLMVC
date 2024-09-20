using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Agencium
    {
        public Agencium()
        {
            Automovils = new HashSet<Automovil>();
        }

        public int IdAgencia { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Automovil> Automovils { get; set; }
    }
}
