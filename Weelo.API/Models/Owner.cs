using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.Models
{
    /// <summary>
    /// Modelo real de la tabla Owner en base de datos.
    /// </summary>
    public class Owner
    {
        public int IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }
        public IList<Property> Properties { get; set; }
    }
}
