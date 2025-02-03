using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using apiHorus.Custom;
using apiHorus.Models;
using apiHorus.Models.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace apiHorus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly HorusContext _dbHoruscontext;
        public ProductoController(HorusContext dbHoruscontext)
        {
            _dbHoruscontext = dbHoruscontext;
        }

        [HttpGet]
        [Route("Productos")]
        // [Authorize(Roles = "admin, usuario")]
        public async Task<IActionResult> Productos()
        {
            var lista = await _dbHoruscontext.Productos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new {value = lista});
        }

        [HttpPost]
        [Route("AgregarProducto")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AgregarProducto([FromBody] Producto producto)
        {
            _dbHoruscontext.Productos.Add(producto);
            await _dbHoruscontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, producto);
        }

        [HttpPut]
        [Route("ActualizarProducto/{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto producto)
        {
            var existingProducto = await _dbHoruscontext.Productos.FindAsync(id);
            if (existingProducto == null)
                return NotFound(new { message = "Producto no encontrado" });

            existingProducto.Title = producto.Title;
            existingProducto.Description = producto.Description;
            existingProducto.Price = producto.Price;
            existingProducto.PriceAnterior = producto.PriceAnterior;
            existingProducto.ImageUrl = producto.ImageUrl;
            existingProducto.Categoria = producto.Categoria;

            await _dbHoruscontext.SaveChangesAsync();
            return Ok(new { message = "Producto actualizado correctamente" });
        }

        [HttpDelete]
        [Route("EliminarProducto/{id}")]
        // [Authorize(Roles = "usuario")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _dbHoruscontext.Productos.FindAsync(id);
            if (producto == null)
                return NotFound(new { message = "Producto no encontrado" });

            _dbHoruscontext.Productos.Remove(producto);
            await _dbHoruscontext.SaveChangesAsync();
            return Ok(new { message = "Producto eliminado correctamente" });
        }
        
    }
}
