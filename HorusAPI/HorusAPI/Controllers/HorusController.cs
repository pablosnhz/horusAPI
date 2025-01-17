using AutoMapper;
using HorusAPI.Datos;
using HorusAPI.Models;
using HorusAPI.Models.Dto;
using HorusAPI.Repositorios.IRepositorio;
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
        // private readonly ApplicationDbContext _db;
        private readonly IHorusRepositorio _horusRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public HorusController(ILogger<HorusController> logger, IHorusRepositorio horusRepo, IMapper mapper)
        {
            _logger = logger;
            _horusRepo = horusRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet("GetHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetHorus()
        {
            try
            {
                _logger.LogInformation("Obtener los productos");

                IEnumerable<Horus> horusList = await _horusRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<HorusDto>>(horusList);
                _response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(_response);
            } catch (Exception ex) 
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("GetHorusPorId", Name ="GetProductoHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHorusPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el producto por id" + id);
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                //var horus = HorusStore.horusList.FirstOrDefault(v => v.Id == id);
                var horus = await _horusRepo.Obtener(x => x.Id == id);

                if (horus == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsExitoso =false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<HorusDto>(horus);
                _response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(_response);
            } catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() {  ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CrearProducto([FromBody] HorusCreateDto horusCreateProducto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _horusRepo.Obtener(v => v.Name.ToLower() ==
                horusCreateProducto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El producto con ese nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (horusCreateProducto == null)
                {
                    return BadRequest(horusCreateProducto);
                }
                //horusProducto.Id = HorusStore.horusList.OrderByDescending(v => //v.Id).FirstOrDefault().Id + 1;
                //HorusStore.horusList.Add(horusProducto);
                Horus modelo = _mapper.Map<Horus>(horusCreateProducto);

                await _horusRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode = System.Net.HttpStatusCode.Created;

                return CreatedAtRoute("GetProductoHorus", new { id = modelo.Id }, _response);
            } catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> {ex.ToString()};
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso =false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var producto = await _horusRepo.Obtener(v => v.Id == id);
                if (producto == null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _horusRepo.Remover(producto);

                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(_response);
            } catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() {ex.ToString()};
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] HorusUpdateDto horusUpdateProducto)
        {
            if (horusUpdateProducto == null || id != horusUpdateProducto.Id)
            {
                _response.IsExitoso=false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            //var producto = HorusStore.horusList.FirstOrDefault(v => v.Id == id);
            //producto.Name = horusProducto.Name;
            //producto.Description = horusProducto.Description;
            Horus modelo = _mapper.Map<Horus>(horusUpdateProducto);

            await _horusRepo.Actualizar(modelo);


            _response.StatusCode=System.Net.HttpStatusCode.NoContent;

            return Ok(_response);
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

            var horus = await _horusRepo.Obtener(h => h.Id == id, tracked: false);
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

            _horusRepo.Actualizar(horus);

            return NoContent();
        }

    }
}
