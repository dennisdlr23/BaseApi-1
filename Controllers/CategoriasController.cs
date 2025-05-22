using BaseApi.WebApi.Models;
using BaseApi.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BaseApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasRepository _repo;

        public CategoriasController(ICategoriasRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Categorias>> Get()
        {
            return await _repo.ObtenerTodas();
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categorias categoria)
        {
            var id = await _repo.InsertarCategoria(categoria);
            return CreatedAtAction(nameof(Get), new { id }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Categorias categoria)
        {
            if (id != categoria.Id)
                return BadRequest();

            var actualizado = await _repo.ActualizarCategoria(categoria);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _repo.EliminarCategoria(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
