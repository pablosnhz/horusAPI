using AutoMapper;
using HorusAPI.Datos;
using HorusAPI.Models;
using HorusAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HorusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorusController : ControllerBase
    {
        private readonly ILogger<HorusController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HorusController(ILogger<HorusController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }


        [HttpGet("GetHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HorusDto>>> GetHorus()
        {
            _logger.LogInformation("Obtener los productos");

            IEnumerable<Horus> horusList = await _db.HorusDB.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<HorusDto>>(horusList));
        }


        [HttpGet("GetHorusPorId", Name ="GetProductoHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HorusDto>> GetHorusPorId(int id)
        {
            if(id == 0)
            {
                _logger.LogError("Error al traer el producto por id" + id);
                return BadRequest();
            }
            //var horus = HorusStore.horusList.FirstOrDefault(v => v.Id == id);
            var horus = await _db.HorusDB.FirstOrDefaultAsync(x => x.Id == id);

            if(horus == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HorusDto>(horus));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HorusDto>> CrearProducto([FromBody] HorusCreateDto horusCreateProducto)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            if(await _db.HorusDB.FirstOrDefaultAsync(v => v.Name.ToLower() ==
            horusCreateProducto.Name.ToLower()) != null)
                {
                ModelState.AddModelError("NombreExiste", "El producto con ese nombre ya existe");
                return BadRequest(ModelState);
                }

            if(horusCreateProducto == null)
            {
                return BadRequest(horusCreateProducto);
            }
            //horusProducto.Id = HorusStore.horusList.OrderByDescending(v => //v.Id).FirstOrDefault().Id + 1;
            //HorusStore.horusList.Add(horusProducto);
            Horus modelo = _mapper.Map<Horus>(horusCreateProducto);
            
            await _db.HorusDB.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetProductoHorus", new { id = modelo.Id }, modelo);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var producto = await _db.HorusDB.FirstOrDefaultAsync(v => v.Id == id);
            if(producto == null)
            {
                return NotFound();
            }
            _db.HorusDB.Remove(producto);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] HorusUpdateDto horusUpdateProducto)
        {
            if (horusUpdateProducto == null || id != horusUpdateProducto.Id)
            {
                return BadRequest(new { message = "El ID de la URL y el ID del producto no coinciden o el cuerpo está vacío." });
            }

            //var producto = HorusStore.horusList.FirstOrDefault(v => v.Id == id);
            //producto.Name = horusProducto.Name;
            //producto.Description = horusProducto.Description;
            Horus modelo = _mapper.Map<Horus>(horusUpdateProducto);

            _db.HorusDB.Update(modelo);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialProducto(int id, [FromBody] JsonPatchDocument<HorusUpdateDto> horusProductoPatch)
        {
            if (horusProductoPatch == null || id == 0)
            {
                return BadRequest();
            }

            var horus = await _db.HorusDB.FirstOrDefaultAsync(h => h.Id == id);
            if (horus == null)
            {
                return NotFound();
            }

            HorusUpdateDto horusDto = _mapper.Map<HorusUpdateDto>(horus);


            if(horus == null) return BadRequest();

            horusProductoPatch.ApplyTo(horusDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Horus modelo = _mapper.Map<Horus>(horusDto);

            _db.HorusDB.Update(horus);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
