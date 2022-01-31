using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.DTOs
{
    /// <summary>
    /// Modelo de la informacion de la imagen de la propiedad que recibe el servicio para su cracion en la BD
    /// </summary>
    public class AddPropertyImageDTO
    {
        public byte File { get; set; }
        public bool Enable { get; set; }
        public int IdProperty { get; set; }
    }
}
