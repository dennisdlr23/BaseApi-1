using BaseApi.WebApi.Models;
using BaseApi.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BaseApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsRepository _documentsRepository;

        public DocumentsController(IDocumentsRepository documentsRepository)
        {
            _documentsRepository = documentsRepository;
        }

        // POST api/documents
        [HttpPost]
        public async Task<IActionResult> CrearDocumento([FromBody] Documents doc)
        {
            if (doc == null)
                return BadRequest("Documento inválido.");

            var resultado = await _documentsRepository.InsertarDocumento(doc);

            if (resultado > 0)
                return Ok(new { mensaje = "Documento insertado correctamente" });

            return StatusCode(500, "Error al insertar documento");
        }

        // PUT api/documents/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDocumento(int id, [FromBody] Documents doc)
        {
            if (doc == null || doc.Id != id)
                return BadRequest("Documento inválido.");

            var actualizado = await _documentsRepository.ActualizarDocumento(doc);

            if (actualizado)
                return Ok(new { mensaje = "Documento actualizado correctamente" });

            return NotFound("Documento no encontrado.");
        }

        // DELETE api/documents/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDocumento(int id)
        {
            var eliminado = await _documentsRepository.EliminarDocumento(id);

            if (eliminado)
                return Ok(new { mensaje = "Documento eliminado correctamente" });

            return NotFound("Documento no encontrado.");
        }

   


        // GET api/documents/categoria/{categoria}
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var documentos = await _documentsRepository.ObtenerTodos();
            return Ok(documentos);
        }


        // GET api/documents/tipo/{tipoContenido}
        [HttpGet("tipo/{tipoContenido}")]
        public async Task<IActionResult> ObtenerPorTipoContenido(string tipoContenido)
        {
            var documentos = await _documentsRepository.ObtenerPorTipoContenido(tipoContenido);
            return Ok(documentos);
        }
    }
}
