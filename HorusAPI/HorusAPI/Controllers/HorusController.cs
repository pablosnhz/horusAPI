using HorusAPI.Datos;
using HorusAPI.Models;
using HorusAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HorusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorusController : ControllerBase
    {
        private readonly ILogger<HorusController> _logger;
        private readonly ApplicationDbContext _db;
        public HorusController(ILogger<HorusController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        [HttpGet("GetHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HorusDto>> GetHorus()
        {
            _logger.LogInformation("Obtener los productos");
            return Ok(_db.HorusDB.ToList());
        }


        [HttpGet("GetHorusPorId", Name ="GetProductoHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HorusDto> GetHorusPorId(int id)
        {
            if(id == 0)
            {
                _logger.LogError("Error al traer el producto por id" + id);
                return BadRequest();
            }
            //var horus = HorusStore.horusList.FirstOrDefault(v => v.Id == id);
            var horus = _db.HorusDB.FirstOrDefault(x => x.Id == id);

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

            if(_db.HorusDB.FirstOrDefault(v => v.Name.ToLower() == 
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
            //horusProducto.Id = HorusStore.horusList.OrderByDescending(v => //v.Id).FirstOrDefault().Id + 1;
            //HorusStore.horusList.Add(horusProducto);
            Horus modelo = new()
            {
                Name = horusProducto.Name,
                Detalle = horusProducto.Detalle,
                ImagenUrl = horusProducto.ImagenUrl,
                Tarifa = horusProducto.Tarifa,
                FechaActual = horusProducto.FechaActual,
                FechaCreacion = horusProducto.DateCreacion
            };
            _db.HorusDB.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetProductoHorus", new { id = horusProducto.Id }, horusProducto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProducto(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var producto = _db.HorusDB.FirstOrDefault(v => v.Id == id);
            if(producto == null)
            {
                return NotFound();
            }
            _db.HorusDB.Remove(producto);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateProducto(int id, [FromBody] HorusDto horusProducto)
        {
            if (horusProducto == null || id != horusProducto.Id)
            {
                return BadRequest(new { message = "El ID de la URL y el ID del producto no coinciden o el cuerpo está vacío." });
            }

            //var producto = HorusStore.horusList.FirstOrDefault(v => v.Id == id);
            //producto.Name = horusProducto.Name;
            //producto.Description = horusProducto.Description;
            Horus modelo = new()
            {
                Id = horusProducto.Id,
                Name = horusProducto.Name,
                Detalle = horusProducto.Detalle,
                Tarifa = horusProducto.Tarifa,
                ImagenUrl = horusProducto.ImagenUrl,
                FechaActual = horusProducto.FechaActual,
                FechaCreacion = horusProducto.DateCreacion
            };
            _db.HorusDB.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePartialProducto(int id, JsonPatchDocument<HorusDto> horusProductoPatch)
        {
            if (horusProductoPatch == null || id == 0)
            {
                return BadRequest();
            }

            //var producto = HorusStore.horusList.FirstOrDefault(v => v.Id == id);
            var horus = _db.HorusDB.FirstOrDefault(h => h.Id == id);
            HorusDto horusDto = new()
            {
                Id = horus.Id,
                Name = horus.Name,
                Detalle = horus.Detalle,
                ImagenUrl = horus.ImagenUrl,
                Tarifa = horus.Tarifa,
                FechaActual = horus.FechaActual,
                DateCreacion = horus.FechaCreacion
            };
            if(horus == null) return BadRequest();

            horusProductoPatch.ApplyTo(horusDto, ModelState);
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            Horus modelo = new()
            {
                Id = horus.Id,
                Name = horus.Name,
                Detalle = horus.Detalle,
                ImagenUrl = horus.ImagenUrl,
                Tarifa = horus.Tarifa,
                FechaActual = horus.FechaActual,
                FechaCreacion = horus.FechaCreacion
            };
            _db.HorusDB.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
