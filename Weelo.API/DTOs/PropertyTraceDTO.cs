using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.DTOs
{
    /// <summary>
    /// Modelo de la informacion PropertyTrace cuando se consulta una propiedad.
    /// </summary>
    public class PropertyTraceDTO
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Tax { get; set; }
    }
}
