using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.DTOs
{
    /// <summary>
    /// Modelo de la informacion que se pide cuando se va actualizar una Propiedad.
    /// </summary>
    public class UpdatePropertyDTO
    {
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
        public int IdOwner { get; set; }
    }
}
