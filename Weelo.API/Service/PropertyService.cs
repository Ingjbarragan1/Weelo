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
    /// servicio que se encarga administracion de una propiedad, suminstrando metodos como la creacion,
    /// actualizacion, busqueda, eliminacion de la propiedad, tambien cuenta con el metodo de agregar imagenes de la propiedad.
    /// </summary>
    public class PropertyService : IPropertyService
    {
        private readonly DatabaseContext _context;

        public PropertyService(DatabaseContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// Metodo para crar una propiedad en la BD.
        /// </summary>
        /// <param name="propertyDto">Recibe un objeto de tipo AddPropertyDTO con toda la informacion requerida para crarse en la BD.</param>
        /// <returns>
        /// Retorna un objeto de tipo Result, el codigo y el mensaje que se produce al insertar en la base de datos.
        /// Code:400 = Es que algunos de los parametros esta mal diligenciado o va nulll algun campo reuqrido.
        /// Code:404 = Cuando el IdOwner respensable de la propiedad no aparece en la BD.
        /// Code:500 = Es un error inesperado;
        /// Code:200 = Es que la insercion del registro exitosamente en la BD
        /// </returns>
        public async Task<Result> AddPropertyAsync(AddPropertyDTO propertyDto)
        {
            try
            {
                var isInvalidProperty = string.IsNullOrWhiteSpace(propertyDto.Name) ||
                                     string.IsNullOrWhiteSpace(propertyDto.Address) ||
                                     (propertyDto.Price == default) ||
                                     string.IsNullOrWhiteSpace(propertyDto.CodeInternal) ||
                                     string.IsNullOrWhiteSpace(propertyDto.Year) ||
                                     (propertyDto.IdOwner == default);
                if (isInvalidProperty)
                {
                    return new Result() { StatusResult = 400, StatusMessage = "Invalid Property parameters" };
                }

                var owner = await this._context.Owners.FindAsync(propertyDto.IdOwner);

                if (owner is null)
                {
                    return new Result() { StatusResult = 404, StatusMessage = "Owner does not exist" };
                }

                var property = new Property()
                {
                    Name = propertyDto.Name,
                    Address = propertyDto.Address,
                    Price = propertyDto.Price,
                    CodeInternal = propertyDto.CodeInternal,
                    Year = propertyDto.Year,
                    Owner = owner
                };

                _context.Properties.Add(property);
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
        /// Metodo que se encarga de eliminar una propiedad por medio del Id en la BD.
        /// </summary>
        /// <param name="idProperty">Recibe el Identificador de la propiedad que se va a eliminar.</param>
        /// <returns>
        /// Retorna un objeto de tipo Result, el codigo y el mensaje que se produce al eliminar en la base de datos.
        /// Code:404 = Cuando la propiedad que se va a eliminar no se encuenta en BD.
        /// Code:500 = Es un error inesperado;
        /// Code:200 = Es que se elimino el registro exitosamente en la BD
        /// </returns>
        public async Task<Result> DeletePropertyAsync(int idProperty)
        {
            try
            {
                var existingProperty = await this._context.Properties.FindAsync(idProperty);

                if (existingProperty is null)
                {
                    return new Result() { StatusResult = 404, StatusMessage = "Property does not exist" };
                }

                var query = _context.Properties.Include("PropertyImages").Include("PropertyTraces");
                var deletedProperty = await query.Where(x => x.IdProperty == idProperty).FirstOrDefaultAsync();

                if (deletedProperty.PropertyImages != null && deletedProperty.PropertyImages.Count > 0)
                {
                    foreach (var image in deletedProperty.PropertyImages)
                    {
                        _context.PropertyImages.Remove(image);
                    }
                }

                if (deletedProperty.PropertyTraces != null && deletedProperty.PropertyTraces.Count > 0)
                {
                    foreach (var trace in deletedProperty.PropertyTraces)
                    {
                        _context.PropertyTraces.Remove(trace);
                    }
                }

                _context.Properties.Remove(deletedProperty);
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
        /// Metodo que se encarga de realizar la busqueda de una propiedad por medio del nombre.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad por el que se va a realizar la busqueda</param>
        /// <returns>
        /// Retorna un objeto de tipo Result, el codigo, el mensaje y el objeto con la info que esta guardada en la BD.
        /// Code:500 = Es un error inesperado;
        /// Code:200 = La busqueda se realizo exitosamente y en la data viene toda la informacion de la BD
        /// </returns>
        public async Task<Result> GetPropertiesAsync(string propertyName)
        {
            try
            {
                var querable = this._context.Properties.AsQueryable();

                querable.Include("Owner");
                querable.Include("PropertyImages");
                querable.Include("PropertyTraces");

                if (!string.IsNullOrWhiteSpace(propertyName))
                {
                    querable = querable.Where(x => x.Name.Contains(propertyName));
                }

                var properties = await querable.Select(x => new GetPropertiesDTO()
                {
                    IdProperty = x.IdProperty,
                    Name = x.Name,
                    Address = x.Address,
                    Price = x.Price,
                    CodeInternal = x.CodeInternal,
                    Year = x.Year,
                    Owner = new PropertyOwnerDTO()
                    {
                        IdOwner = x.Owner.IdOwner,
                        Name = x.Owner.Name,
                        Address = x.Owner.Address,
                        Photo = x.Owner.Photo,
                        Birthday = x.Owner.Birthday
                    },
                    PropertyImages = x.PropertyImages.Select(i => new PropertyImageDTO()
                    {
                        IdPropertyImage = i.IdPropertyImage,
                        File = i.File,
                        Enable = i.Enable
                    }).ToList(),
                    PropertyTraces = x.PropertyTraces.Select(t => new PropertyTraceDTO()
                    {
                        IdPropertyTrace = t.IdPropertyTrace,
                        DateSale = t.DateSale,
                        Name = t.Name,
                        Value = t.Value,
                        Tax = t.Tax
                    }).ToList()
                }).ToListAsync();

                return new Result() { StatusResult = 200, StatusMessage = "Ok", Data = properties };
            }
            catch (Exception ex)
            {

                // Codigo para loguear error
                return new Result() { StatusResult = 500, StatusMessage = "Error inesperado" };
            }
           
        }
        /// <summary>
        /// Metodo que se encarga de actualizar la informacion una propiedad en la BD
        /// </summary>
        /// <param name="propertyDto">Recibe un objeto de tipo UpdatePropertyDTO que son las caracteristicas de la propiedad que se va a actualizar en la BD</param>
        /// <returns>
        /// Retorna un objeto de tipo Result, el codigo y el mensaje que se produce al Actualizar en la base de datos.
        /// Code:404 = Cuando la propiedad que se va a actualizar no se encuenta en BD.
        /// Code:500 = Es un error inesperado;
        /// Code:200 = Es que se actualizo el registro exitosamente en la BD
        /// </returns>
        public async Task<Result> UpdatePropertiesAsync(UpdatePropertyDTO propertyDto)
        {
            try
            {
                var property = await this._context.Properties.FindAsync(propertyDto.IdProperty);

                if (property is null)
                {
                    return new Result() { StatusResult = 404, StatusMessage = "Owner does not exist" };
                }

                property.Name = (!string.IsNullOrWhiteSpace(propertyDto.Name)) ? propertyDto.Name : property.Name;
                property.Address = (!string.IsNullOrWhiteSpace(propertyDto.Address)) ? propertyDto.Address : property.Address;
                property.Price = (propertyDto.Price != default) ? propertyDto.Price : property.Price;
                property.CodeInternal = (propertyDto.CodeInternal != default) ? propertyDto.CodeInternal : property.CodeInternal;
                property.Year = (propertyDto.Year != default) ? propertyDto.Year : property.Year;

                if (propertyDto.IdOwner != default)
                {
                    var newOwner = await this._context.Owners.FindAsync(propertyDto.IdOwner);
                    property.Owner = newOwner;
                }

                _context.Properties.Update(property);
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
        /// Metodo que se utiliza para agregar imagenes de la propiedad.
        /// </summary>
        /// <param name="propertyDto">Recibe un objeto de tipo AddPropertyImageDTO con toda la informacion requerida para crarse en la BD.</param>
        /// <returns>
        /// Retorna un objeto de tipo Result, el codigo y el mensaje que se produce al insertar en la base de datos.       
        /// Code:500 = Es un error inesperado;
        /// Code:200 = Es que la insercion del registro exitosamente en la BD
        /// </returns>
        public async Task<Result> AddImageFromPropertiesAsync(AddPropertyImageDTO propertyDto)
        {
            try
            {
                var property = await this._context.Properties.FindAsync(propertyDto.IdProperty);

                if (property.PropertyImages is null)
                {
                    property.PropertyImages = new List<PropertyImage>();
                }

                property.PropertyImages.Add(new PropertyImage()
                {
                    File = propertyDto.File,
                    Enable = propertyDto.Enable
                });

                _context.Properties.Update(property);
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
