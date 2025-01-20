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
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : Controller
    {
        private readonly HorusContext _dbHoruscontext;
        private readonly Utilidades _utilidades;
        public AccesoController(HorusContext dbHoruscontext, Utilidades utilidades)
        {
            _dbHoruscontext = dbHoruscontext;
            _utilidades = utilidades;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioDTO objeto)
        {
            var modeloUsuario = new Usuario
            {
                Name = objeto.Nombre,
                Email = objeto.Email,
                Password = _utilidades.encriptarSHA256(objeto.Password)
            };
            await _dbHoruscontext.Usuarios.AddAsync(modeloUsuario);
            await _dbHoruscontext.SaveChangesAsync();

            if(modeloUsuario.UserId != 0) 
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
              else
                return StatusCode(StatusCodes.Status200OK, new {isSuccess = false});
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _dbHoruscontext.Usuarios
                                           .Where(u => 
                                            u.Email == objeto.Email &&
                                            u.Password == _utilidades.encriptarSHA256(objeto.Password)).FirstOrDefaultAsync();

            if(usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new {isSuccess=false, token =""});
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado) });

        }

        //public IActionResult Index()
        //{
        //  return View();
        //}
    }
}
