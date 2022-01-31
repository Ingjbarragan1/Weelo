using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.Models
{
    /// <summary>
    /// Modelo real de la tabla PorpertyTrace en base de datos.
    /// </summary>
    public class PropertyTrace
    {
        public int IdPropertyTrace { get; set; }        
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Tax { get; set; }
        public Property Property { get; set; }
    }
}
