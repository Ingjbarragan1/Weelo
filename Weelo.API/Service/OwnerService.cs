using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.API.Common;
using Weelo.API.Database;
using Weelo.API.DTOs;
using Weelo.API.Models;

namespace Weelo.API.Service
{
    /// <summary>
    /// Servicio que se encarga de realizar la busuqeda, creacion y eliminacion de un Owner
    /// </summary>
    public class OwnerService : IOwnerService
    {
        private readonly DatabaseContext _context;

        public OwnerService(DatabaseContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// Metodo que se encarga de crear un Owner
        /// </summary>
        /// <param name="ownerDto">Recibe un objeto de tipo AddOwnerDTO que son las caracteristicas del Owner a guardar en la BD.</param>
        /// <returns>
        /// Retorna un objeto de tipo Result, el codigo y el mensaje que se produce al insertar en la base de datos.
        /// Code:400 = Es que algunos de los parametros esta mal diligenciado o va nulll algun campo reuqrido.
        /// Code:500 = Es un error inesperado;
        /// Code:200 = Es que la insercion del registro exitosamente en la BD
        /// </returns>
        public async Task<Result> AddOwnerAsync(AddOwnerDTO ownerDto)
        {
            try
            {
                var isInvalidOwner = string.IsNullOrWhiteSpace(ownerDto.Name) ||
                                     string.IsNullOrWhiteSpace(ownerDto.Address) ||
                                     string.IsNullOrWhiteSpace(ownerDto.Photo) ||
                                     (ownerDto.Birthday == default);
                if (isInvalidOwner)
                {
                    return new Result() { StatusResult = 400, StatusMessage = "Invalid Owner parameters" };
                }

                var owner = new Owner()
                {
                    Name = ownerDto.Name,
                    Address = ownerDto.Address,
                    Photo = ownerDto.Photo,
                    Birthday = ownerDto.Birthday
                };
                _context.Owners.Add(owner);
                await _context.SaveChangesAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                // Codigo para loguear error
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
        }
        /// <summary>
        /// Metodo que se encarga de eliminar un Owner por medio del ID
        /// </summary>
        /// <param name="idOwner">Recibe el Identificador del Owner que se va a eliminar.</param>
        /// <returns>
        /// Retorna un objeto de tipo Result, el codigo y el mensaje que se produce al eliminar en la base de datos.
        /// Code:404 = Cuando el Owner que se va a eliminar no se encuenta en BD.
        /// Code:500 = Es un error inesperado;
        /// Code:200 = Es que se elimino el registro exitosamente en la BD
        /// </returns>
        public async Task<Result> DeleteOwnerAsync(int idOwner)
        {
            try
            {
                var existingOwner = await this._context.Owners.FindAsync(idOwner);

                if (existingOwner is null)
                {
                    return new Result() { StatusResult = 404, StatusMessage = "Owner does not exist" };
                }

                var deletedOwner = _context.Owners.Find(idOwner);
                _context.Owners.Remove(deletedOwner);
                await _context.SaveChangesAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                // Codigo para loguear error
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
        }
        /// <summary>
        /// Metodo que se encarga de realizar la busqueda de un Owner por medio del nombre
        /// </summary>
        /// <param name="ownerName">Nombre del Ownwe por el que se va a realizar la busqueda</param>
        /// <returns>
        /// Retorna un objeto de tipo Result, el codigo, el mensaje y el objeto con la info que esta guardada en la BD.
        /// Code:500 = Es un error inesperado;
        /// Code:200 = La busqueda se realizo exitosamente y en la data viene toda la informacion de la BD
        /// </returns>
        public async Task<Result> GetOwnersAsync(string ownerName)
        {
            try
            {
                var querable = this._context.Owners.AsQueryable();

                if (!string.IsNullOrWhiteSpace(ownerName))
                {
                    querable = querable.Where(x => x.Name.Contains(ownerName));
                }

                var owners = await querable.Select(x => new GetOwnersDTO()
                {
                    IdOwner = x.IdOwner,
                    Name = x.Name,
                    Address = x.Address,
                    Photo = x.Photo,
                    Birthday = x.Birthday
                }).ToListAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok", Data = owners };
            }
            catch (Exception ex)
            {
                // Codigo para loguear error
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
        }
        /// <summary>
        /// Metodo que se encarga de actualizar la informacion un Owner en BD
        /// </summary>
        /// <param name="ownerDto">Recibe un objeto de tipo UpdateOwnerDTO que son las caracteristicas del Owner que se va a actualizar en la BD</param>
        /// <returns>
        /// Retorna un objeto de tipo Result, el codigo y el mensaje que se produce al Actualizar en la base de datos.
        /// Code:404 = Cuando el Owner que se va a actualizar no se encuenta en BD.
        /// Code:500 = Es un error inesperado;
        /// Code:200 = Es que se actualizo el registro exitosamente en la BD
        /// </returns>
        public async Task<Result> UpdateOwnerAsync(UpdateOwnerDTO ownerDto)
        {
            try
            {
                var owner = await this._context.Owners.FindAsync(ownerDto.IdOwner);

                if (owner is null)
                {
                    return new Result() { StatusResult = 404, StatusMessage = "Owner does not exist" };
                }

                owner.Name = (!string.IsNullOrWhiteSpace(ownerDto.Name)) ? ownerDto.Name : owner.Name;
                owner.Address = (!string.IsNullOrWhiteSpace(ownerDto.Address)) ? ownerDto.Address : owner.Address;
                owner.Photo = (!string.IsNullOrWhiteSpace(ownerDto.Photo)) ? ownerDto.Photo : owner.Photo;
                owner.Birthday = (ownerDto.Birthday != default) ? ownerDto.Birthday : owner.Birthday;

                _context.Owners.Update(owner);
                await _context.SaveChangesAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok" };
            }
            catch (Exception ex)
            {
                // Codigo para loguear error
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
        }
    }
}
