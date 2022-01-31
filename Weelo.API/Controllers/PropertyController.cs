using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.API.DTOs;
using Weelo.API.Models;
using Weelo.API.Service;

namespace Weelo.API.Controllers
{
    /// <summary>
    /// Contorlador para exponer y consumir los metodos del servicio del Property
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        /// <summary>
        /// para acceder  alos metodos de la interface donde se encuentran los metodos de la propiedad
        /// </summary>
        private readonly IPropertyService _service;
        public PropertyController(IPropertyService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        /// <summary>
        /// Metodo para agregar una propiedad 
        /// </summary>
        /// <param name="property">recibe un objeto de tipo AddPropertyDTO con toda la informacion requerida para crear una propiedad</param>
        /// <returns>
        /// muestra el mensaje de respuesta que nos da el servicio ya se satifactorio o si ocurrio algun error
        /// </returns>
        [HttpPost("create")]
        public async Task<IActionResult> AddPropertyAsync(AddPropertyDTO property)
        {
            var result =  await _service.AddPropertyAsync(property);

            if (result.StatusResult == 400)
            {
                return this.BadRequest("Invalid parameters");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok("Property created");
        }
        /// <summary>
        /// Metodo para buscar un propiedad por el nombre 
        /// </summary>
        /// <param name="propertyName">nombre con que resgitra la propiedad en la BD</param>
        /// <returns>
        /// Objeto con la informacion de la propiedad que esta en la BD
        /// </returns>
        [HttpGet("get")]
        public async Task<IActionResult> GetPropertiesAsync(string propertyName)
        {
            var result = await _service.GetPropertiesAsync(propertyName);

            if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok(result.Data);
        }
        /// <summary>
        /// Metodo para actualizar la informacion de la propiedad en la BD
        /// </summary>
        /// <param name="property">objeto de tipo UpdatePropertyDTO que contiene la toda la informacion a actualizar en la BD</param>
        /// <returns>
        /// muestra el mensaje de respuesta que nos da el servicio ya se satifactorio o si ocurrio algun errormo
        /// </returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePropertyAsync(UpdatePropertyDTO property)
        {
            var result = await _service.UpdatePropertiesAsync(property);

            if (result.StatusResult == 400)
            {
                return this.BadRequest("Invalid parameters");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok("Property updated");
        }
        /// <summary>
        /// Metodo para eliminar una  propiedad
        /// </summary>
        /// <param name="idProperty">Identificador de l apropiedad que se va a aleiminar</param>
        /// <returns>
        /// muestra el mensaje de respuesta que nos da el servicio ya se satifactorio o si ocurrio algun error
        /// </returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePropertyAsync(int idProperty)
        {
            var result = await _service.DeletePropertyAsync(idProperty);

            if (result.StatusResult == 404)
            {
                return NotFound("Property does not exist");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok("Property deleted");
        }
        /// <summary>
        /// Metodo para cambiar el precio de propiedad
        /// </summary>
        /// <param name="idProperty">identificador de la propiedad a la que se le va a cambiar el precio</param>
        /// <param name="newPrice">nuevo preio de la propiedad que se va a modificar</param>
        /// <returns>
        /// muestra el mensaje de respuesta que nos da el servicio ya se satifactorio o si ocurrio algun error
        /// </returns>
        [HttpPut("change-price")]
        public async Task<IActionResult> ChangePricePropertyAsync(int idProperty, int newPrice)
        {       

            var changePriceModel = new UpdatePropertyDTO()
            {
                IdProperty = idProperty,
                Price = newPrice
            };

            var result = await _service.UpdatePropertiesAsync(changePriceModel);

            if (result.StatusResult == 404)
            {
                return NotFound("Property does not exist");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok("Successful Price Change");
        }
        /// <summary>
        /// metodo para gragar una imagen relacionada con la propiedad
        /// </summary>
        /// <param name="propertyImageDto">objeto de tipo AddPropertyImageDTO que contiene toda la informacion requerida por la bd</param>
        /// <returns>
        /// muestra el mensaje de respuesta que nos da el servicio ya se satifactorio o si ocurrio algun errormo
        /// </returns>
        [HttpPost("add-image")]
        public async Task<IActionResult> AddImageFromPropertyAsync(AddPropertyImageDTO propertyImageDto)
        {
            var result = await _service.AddImageFromPropertiesAsync(propertyImageDto);

            if (result.StatusResult == 400)
            {
                return this.BadRequest("Invalid parameters");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok("Property image added");
        }
    }
}
