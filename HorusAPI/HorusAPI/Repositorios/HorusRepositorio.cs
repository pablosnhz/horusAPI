using HorusAPI.Datos;
using HorusAPI.Models;
using HorusAPI.Repositorios.IRepositorio;

namespace HorusAPI.Repositorios
{
    public class HorusRepositorio : Repositorio<Horus>, IHorusRepositorio
    {
        private readonly ApplicationDbContext _db;

        public HorusRepositorio(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task<Horus> Actualizar(Horus entidad)
        {
            entidad.FechaActual = DateTime.Now;
            _db.HorusDB.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
