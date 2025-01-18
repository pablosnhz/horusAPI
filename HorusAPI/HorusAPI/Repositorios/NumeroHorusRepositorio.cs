using HorusAPI.Datos;
using HorusAPI.Models;
using HorusAPI.Repositorios.IRepositorio;

namespace HorusAPI.Repositorios
{
    public class NumeroHorusRepositorio : Repositorio<NumeroHorus>, INumeroHorusRepositorio
    {
        private readonly ApplicationDbContext _db;

        public NumeroHorusRepositorio(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task<NumeroHorus> Actualizar(NumeroHorus entidad)
        {
            entidad.FechaActual = DateTime.Now;
            _db.NumeroHorusDB.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
