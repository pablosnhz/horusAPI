using AutoMapper;
using HorusAPI.Datos;
using HorusAPI.Models;
using HorusAPI.Models.Dto;
using HorusAPI.Repositorios;
using HorusAPI.Repositorios.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace HorusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroHorusController : ControllerBase
    {
        private readonly ILogger<HorusController> _logger;
        // private readonly ApplicationDbContext _db;
        private readonly IHorusRepositorio _horusRepo;
        private readonly INumeroHorusRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroHorusController(ILogger<HorusController> logger, IHorusRepositorio horusRepo, INumeroHorusRepositorio numeroRepo, IMapper mapper)
        {
            _logger = logger;
            _horusRepo = horusRepo;
            _numeroRepo = numeroRepo;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet("GetHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroHorus()
        {
            try
            {
                _logger.LogInformation("Obtener cantidad productos");

                IEnumerable<NumeroHorus> numeroHorusList = await _numeroRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<NumeroHorusDto>>(numeroHorusList);
                _response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(_response);
            } catch (Exception ex) 
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("GetHorusPorId", Name ="GetNumeroProductoHorus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroHorusPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer el numero producto por id" + id);
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                //var horus = HorusStore.horusList.FirstOrDefault(v => v.Id == id);
                var numeroHorus = await _numeroRepo.Obtener(x => x.HorusNo == id);

                if (numeroHorus == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    _response.IsExitoso =false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<NumeroHorusDto>(numeroHorus);
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
        public async Task<ActionResult<APIResponse>> CrearNumeroProducto([FromBody] NumeroHorusCreateDto horusCreateProducto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numeroRepo.Obtener(v => v.HorusNo ==
                horusCreateProducto.VillaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero producto con ese nombre ya existe");
                    return BadRequest(ModelState);
                }

                if(await _numeroRepo.Obtener(v=>v.HorusId==horusCreateProducto.VillaNo)==null)
                {
                    ModelState.AddModelError("ClaveForanea", "El id numero producto con ese nombre no existe");
                    return BadRequest(ModelState);
                }

                if (horusCreateProducto == null)
                {
                    return BadRequest(horusCreateProducto);
                }
                //horusProducto.Id = HorusStore.horusList.OrderByDescending(v => //v.Id).FirstOrDefault().Id + 1;
                //HorusStore.horusList.Add(horusProducto);
                NumeroHorus modelo = _mapper.Map<NumeroHorus>(horusCreateProducto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActual = DateTime.Now;
                await _numeroRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode = System.Net.HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroProductoHorus", new { id = modelo.HorusNo }, _response);
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
        public async Task<IActionResult> DeleteNumeroProducto(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso =false;
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var numeroproducto = await _numeroRepo.Obtener(v => v.HorusNo == id);
                if (numeroproducto == null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _numeroRepo.Remover(numeroproducto);

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
        public async Task<IActionResult> UpdateNumeroProducto(int id, [FromBody] NumeroHorusUpdateDto horusUpdateProducto)
        {
            if (horusUpdateProducto == null || id != horusUpdateProducto.HorusNo)
            {
                _response.IsExitoso=false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if(await _horusRepo.Obtener(v => v.Id == horusUpdateProducto.HorusId) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El id del producto no existe");
                return BadRequest(ModelState);
            }

            //var producto = HorusStore.horusList.FirstOrDefault(v => v.Id == id);
            //producto.Name = horusProducto.Name;
            //producto.Description = horusProducto.Description;
            NumeroHorus modelo = _mapper.Map<NumeroHorus>(horusUpdateProducto);

            await _numeroRepo.Actualizar(modelo);
            _response.StatusCode=System.Net.HttpStatusCode.NoContent;

            return Ok(_response);
        }
    }
}
