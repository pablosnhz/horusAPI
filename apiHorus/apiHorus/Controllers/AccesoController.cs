using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using apiHorus.Custom;
using apiHorus.Models;
using apiHorus.Models.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        public async Task<IActionResult> Registrarse(UsuarioDTO usuarioDto)
        {
            if (_dbHoruscontext.Usuarios.Any(u => u.Email == usuarioDto.Email))
                return BadRequest(new { message = "El usuario ya existe." });

            var roleUsuario = await _dbHoruscontext.Roles.FirstOrDefaultAsync(r => r.RoleName == "usuario");
            if (roleUsuario == null)
                return BadRequest(new { message = "error" });

            var usuario = new Usuario
            {
                Name = usuarioDto.Nombre, 
                Email = usuarioDto.Email,
                Password = _utilidades.encriptarSHA256(usuarioDto.Password),
                RoleId = roleUsuario.RoleId 
            };

            _dbHoruscontext.Usuarios.Add(usuario);
            await _dbHoruscontext.SaveChangesAsync();
            return Ok(new { message = "Usuario registrado correctamente." });
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _dbHoruscontext.Usuarios
                                           .Where(u => 
                                            u.Email == objeto.Email &&
                                            u.Password == _utilidades.encriptarSHA256(objeto.Password)).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new {isSuccess=false, token =""});
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado) });
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = _dbHoruscontext.Usuarios.FirstOrDefault(u => u.UserId.ToString() == userId);

            if (user == null)
            {
                return NotFound();
            }
            string roleName = user.RoleId == 1 ? "admin" : user.RoleId == 2 ? "usuario" : "no tiene rango";

            return Ok(new { userName = user.Name, email = user.Email, role = roleName }); 
        }

        [HttpGet("roluser")]
        public IActionResult GetRolUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = _dbHoruscontext.Usuarios.FirstOrDefault(u => u.UserId == u.UserId);

            if (user == null)
            {
                return NotFound("usuario no encontrado");
            }

            // Buscar el rol asociado al usuario
            var role = _dbHoruscontext.Roles.FirstOrDefault(r => r.RoleId == user.RoleId);

            if (role == null)
            {
                return NotFound("rol no encontrado");
            }

            return Ok(new { roleName = role.RoleName });
        }


        //public IActionResult Index()
        //{
        //  return View();
        //}
    }
}
