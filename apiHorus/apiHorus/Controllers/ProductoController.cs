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
    [Authorize]
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
        public async Task<IActionResult> Productos()
        {
            var lista = await _dbHoruscontext.Productos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new {value = lista});
        }
    }
}
