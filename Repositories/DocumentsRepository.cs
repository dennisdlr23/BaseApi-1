using BaseApi.WebApi.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BaseApi.WebApi.Repositories
{
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly IDbConnection _db;

        public DocumentsRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("dbGestionD"));
        }

        public async Task<int> InsertarDocumento(Documents doc)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@NombreOriginal", doc.NombreOriginal);
            parametros.Add("@NombreAlmacenado", doc.NombreAlmacenado);
            parametros.Add("@RutaArchivo", doc.RutaArchivo);
            parametros.Add("@Categoria", doc.Categoria);
            parametros.Add("@Etiquetas", doc.Etiquetas);
            parametros.Add("@TipoContenido", doc.TipoContenido);
            parametros.Add("@TamanoKB", doc.TamanoKB);
            parametros.Add("@UserId", doc.UserId);

            return await _db.ExecuteAsync("sp_InsertarDocumento", parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> ActualizarDocumento(Documents doc)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Id", doc.Id);
            parametros.Add("@NombreOriginal", doc.NombreOriginal);
            parametros.Add("@NombreAlmacenado", doc.NombreAlmacenado);
            parametros.Add("@RutaArchivo", doc.RutaArchivo);
            parametros.Add("@Categoria", doc.Categoria);
            parametros.Add("@Etiquetas", doc.Etiquetas);
            parametros.Add("@TipoContenido", doc.TipoContenido);
            parametros.Add("@TamanoKB", doc.TamanoKB);

            var rows = await _db.ExecuteAsync("sp_ActualizarDocumento", parametros, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> EliminarDocumento(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Id", id);

            var rows = await _db.ExecuteAsync("sp_EliminarDocumento", parametros, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<IEnumerable<Documents>> ObtenerTodos()
        {
            return await _db.QueryAsync<Documents>("sp_ObtenerTodosDocumentos", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Documents>> ObtenerPorCategoria(string categoria)
        {
            var parametros = new { Categoria = categoria };
            return await _db.QueryAsync<Documents>("sp_ObtenerDocumentosPorCategoria", parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Documents>> ObtenerPorTipoContenido(string tipoContenido)
        {
            var parametros = new { TipoContenido = tipoContenido };
            return await _db.QueryAsync<Documents>("sp_ObtenerDocumentosPorTipoContenido", parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Documents>> ObtenerPorUsuario(int userId, string userName)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@UserId", userId);
            parametros.Add("@UserName", userName);
            return await _db.QueryAsync<Documents>("sp_ObtenerDocumentosPorUsuario", parametros, commandType: CommandType.StoredProcedure);
        }
    }

}