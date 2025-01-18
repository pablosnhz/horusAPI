using HorusAPI.Models;
using HorusAPI.Repositorios.iRepositorio;

namespace HorusAPI.Repositorios
{
    public interface INumeroHorusRepositorio: IRepositorio<NumeroHorus>
    {
        Task<NumeroHorus> Actualizar(NumeroHorus entidad);
    }
}
