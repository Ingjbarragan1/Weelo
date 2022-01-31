using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.DTOs
{
    /// <summary>
    /// Modelo de la informacion de la imagen de la propiedad cuando se consulta una propiedad.
    /// </summary>
    public class PropertyImageDTO
    {
        public int IdPropertyImage { get; set; }
        public byte File { get; set; }
        public bool Enable { get; set; }
    }
}
