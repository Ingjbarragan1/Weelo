using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.API.Common;
using Weelo.API.DTOs;

namespace Weelo.API.Service
{
    /// <summary>
    /// Interface utilizada para implentar los metodos administracion de una propiedad como la creacion,
    /// actualizacion, busqueda, eliminacion de la propiedad, tambien cuenta con el metodo de agregar imagenes de la propiedad.
    /// </summary>
    public interface IPropertyService
    {
        Task<Result> AddPropertyAsync(AddPropertyDTO propertyDto);
        Task<Result> DeletePropertyAsync(int idOwner);
        Task<Result> GetPropertiesAsync(string propertyName);
        Task<Result> UpdatePropertiesAsync(UpdatePropertyDTO ownerDto);
        Task<Result> AddImageFromPropertiesAsync(AddPropertyImageDTO propertyDto);
    }
}
