using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Automovil
    {
        public int IdAutomovil { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Generacion { get; set; }
        public string? Color { get; set; }
        public string FechaLanzamiento { get; set; }
        public Agencia Agencia { get; set; }

        public List<object> Autos { get; set; } // lista para guradar desde mi capa PL ya con web service integrado
    }
}
