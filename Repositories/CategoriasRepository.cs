using BaseApi.WebApi.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BaseApi.WebApi.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly IDbConnection _db;

        public CategoriasRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("dbGestionD"));
        }

        public async Task<int> InsertarCategoria(Categorias categoria)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Nombre", categoria.Nombre);
            parametros.Add("@Activa", categoria.Activa);

            return await _db.ExecuteAsync("[dbo].[spAgregarCategoria]", parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> ActualizarCategoria(Categorias categoria)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Id", categoria.Id);
            parametros.Add("@Nombre", categoria.Nombre);
            parametros.Add("@Activa", categoria.Activa);

            var rows = await _db.ExecuteAsync("[dbo].[sp_ActualizarCategoria]", parametros, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> EliminarCategoria(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Id", id);

            var rows = await _db.ExecuteAsync("[dbo].[spEliminarCategoria]", parametros, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<IEnumerable<Categorias>> ObtenerTodas()
        {
            return await _db.QueryAsync<Categorias>("[dbo].[spObtenerCategoriasActivas]", commandType: CommandType.StoredProcedure);
        }
        
    }
}
