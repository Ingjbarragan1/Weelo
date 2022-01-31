using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.API.Common;
using Weelo.API.DTOs;
using Weelo.API.Models;

namespace Weelo.API.Service
{
    /// <summary>
    /// Interface utilizada para implentar los metodos de busuqeda, creacion y eliminacion de un Owner
    /// </summary>
    public interface IOwnerService
    {
        Task<Result> GetOwnersAsync(string ownerName);
        Task<Result> AddOwnerAsync(AddOwnerDTO ownerDto);
        Task<Result> UpdateOwnerAsync(UpdateOwnerDTO ownerDto);
        Task<Result> DeleteOwnerAsync(int idOwner);
    }
}
