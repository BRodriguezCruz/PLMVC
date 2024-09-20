using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Agencia
    {
        public int IdAgencia { get; set; }
        public string Nombre { get; set; } = null!;

        public List<object> Agencias { get; set; }
    }
}
