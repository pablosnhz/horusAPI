using HorusAPI.Datos;
using HorusAPI.Models;
using HorusAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorusController : ControllerBase
    {
        [HttpGet("GetHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HorusDto>> GetHorus()
        {
            return Ok(HorusStore.horusList);
        }


        [HttpGet("GetHorusPorId", Name ="GetProductoHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HorusDto> GetHorusPorId(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var horus = HorusStore.horusList.FirstOrDefault(v => v.Id == id);

            if(horus == null)
            {
                return NotFound();
            }

            return Ok(horus);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public ActionResult<HorusDto> CrearProducto([FromBody] HorusDto horusProducto)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            if(HorusStore.horusList.FirstOrDefault(v => v.Name.ToLower() == 
            horusProducto.Name.ToLower()) != null)
                {
                ModelState.AddModelError("NombreExiste", "El producto con ese nombre ya existe");
                return BadRequest(ModelState);
                }

            if(horusProducto == null)
            {
                return BadRequest(horusProducto);
            }
            if(horusProducto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            horusProducto.Id = HorusStore.horusList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            HorusStore.horusList.Add(horusProducto);

            return CreatedAtRoute("GetProductoHorus", new { id = horusProducto.Id }, horusProducto);
        }
    }
}
