using BaseApi.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseApi.WebApi.Repositories
{
    public interface ICategoriasRepository
    {
        Task<int> InsertarCategoria(Categorias categoria);
        Task<bool> ActualizarCategoria(Categorias categoria);
        Task<bool> EliminarCategoria(int id);
        Task<IEnumerable<Categorias>> ObtenerTodas();

    }
}
