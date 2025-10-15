using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlazor.Entities
{
    public class LibroListCLS
    {
        public int idlibro { get; set; }
        public string titulo { get; set; } = null!;

        public string nombretipolibro { get; set; } = string.Empty;
      
        public byte[]? imagen { get; set; }

        public byte[]? archivo { get; set; }

        public string nombrearchivo { get; set; } = string.Empty;

        public string nombreautor { get; set; } = string.Empty;
    }
}
