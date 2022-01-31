using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weelo.API.Common
{
    /// <summary>
    /// clase que se utiliza para almacenar las respuestas de los metodos 
    /// </summary>
    public class Result
    {
        public int StatusResult { get; set; }
        public string StatusMessage { get; set; }
        public object Data { get; set; }
    }
}
