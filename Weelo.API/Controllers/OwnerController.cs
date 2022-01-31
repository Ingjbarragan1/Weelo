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
    /// Contorlador para exponer y consumir los metodos del servicio del Owner
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        /// <summary>
        /// para acceder  alos metodos de la interface donde se encuentran los metodos del Owner
        /// </summary>
        private readonly IOwnerService _service;
        public OwnerController(IOwnerService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        /// <summary>
        /// Medtodo post para realizar la insercion del owner en la BD
        /// </summary>
        /// <param name="owner">recibe el objeto de tipo AddOwnerDTO con la informacion que obtuvo.</param>
        /// <returns>
        /// retorna un mensaje con lo que le genero el servicio.
        /// </returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddOwnerAsync(AddOwnerDTO owner)
        {
            var result = await _service.AddOwnerAsync(owner);

            if (result.StatusResult == 400)
            {
                return this.BadRequest("Invalid parameters");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok("Owner created");
        }
        /// <summary>
        /// Metodo para consultar el Owner por medio del nombre
        /// </summary>
        /// <param name="ownerName">Recibe el nombre de Owner poe el cual se va a realizar la busqueda</param>
        /// <returns>devuelve el objeto con toda la informacion de que encuentra en la base de datos si es exitoso</returns>
        [HttpGet("get")]
        public async Task<IActionResult> GetOwnersAsync(string ownerName)
        {
            var result = await _service.GetOwnersAsync(ownerName);

            if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok(result.Data);
        }
        /// <summary>
        /// Metodo para actualizar la informacion del Owner
        /// </summary>
        /// <param name="owner">objeto tipo UpdateOwnerDTO que contiene toda la infromacion del owner que se va actualizar</param>
        /// <returns>
        /// devuelve un mensaje con el resulado de la actualizacion.
        /// </returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateOwnerAsync(UpdateOwnerDTO owner)
        {
            var result = await _service.UpdateOwnerAsync(owner);

            if (result.StatusResult == 400) 
            {
                return this.BadRequest("Invalid parameters");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok("Owner updated");
        }
        /// <summary>
        /// Metodo para el elim,inar un owner por medio de identificador
        /// </summary>
        /// <param name="idOwner">idententificador unico de cada Owner</param>
        /// <returns>
        /// devuelve mesaje con respuesta del servicio ya exitoso o de que haya presentado algun error
        /// </returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteOwnerAsync(int idOwner)
        {
            var result = await _service.DeleteOwnerAsync(idOwner);

            if (result.StatusResult == 404)
            {
                return NotFound("Owner does not exist");
            }
            else if (result.StatusResult != 200)
            {
                return StatusCode(500, "Server Error");
            }

            return Ok("Owner deleted");
        }
    }
}
