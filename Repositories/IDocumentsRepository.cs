using System.Collections.Generic;
using System.Threading.Tasks;
using BaseApi.WebApi.Models;

namespace BaseApi.WebApi.Repositories
{
    public interface IDocumentsRepository
    {
        Task<int> InsertarDocumento(Documents doc);
        Task<bool> ActualizarDocumento(Documents doc);
        Task<bool> EliminarDocumento(int id);
        Task<IEnumerable<Documents>> ObtenerTodos();
        Task<IEnumerable<Documents>> ObtenerPorCategoria(string categoria);
        Task<IEnumerable<Documents>> ObtenerPorTipoContenido(string tipoContenido);
    }
}
