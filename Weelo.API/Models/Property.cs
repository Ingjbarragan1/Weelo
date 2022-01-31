using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.Models
{
    /// <summary>
    /// Modelo real de la tabla Property en base de datos.
    /// </summary>
    public class Property
    {
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
        public Owner Owner { get; set; }
        public IList<PropertyImage> PropertyImages { get; set; }
        public IList<PropertyTrace> PropertyTraces { get; set; }

    }
}
