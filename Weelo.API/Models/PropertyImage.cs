using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.Models
{
    /// <summary>
    /// Modelo real de la tabla PorpertyImage en base de datos.
    /// </summary>
    public class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public byte File { get; set; }
        public bool Enable { get; set; }
        public Property Property { get; set; }
    }
}
