using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.DTOs
{
    /// <summary>
    /// Modelo de la informacion que se muestra cuando se obtiene un owner de la BD.
    /// </summary>
    public class GetPropertiesDTO
    {
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
        public PropertyOwnerDTO Owner { get; set; }
        public IList<PropertyImageDTO> PropertyImages { get; set; }
        public IList<PropertyTraceDTO> PropertyTraces { get; set; }
    }
}
