using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.API.Models;

namespace Weelo.API.DTOs
{
    /// <summary>
    /// Modelo de la informacion que se muestra cuando se obtiene un owner de la BD.
    /// </summary>
    public class GetOwnersDTO
    {
        public int IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
