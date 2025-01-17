using HorusAPI.Models;
using HorusAPI.Repositorios.iRepositorio;

namespace HorusAPI.Repositorios.IRepositorio
{
    public interface IHorusRepositorio: IRepositorio<Horus>
    {
        Task<Horus> Actualizar(Horus entidad);
    }
}
