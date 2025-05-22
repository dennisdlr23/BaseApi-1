using BaseApi.WebApi.Models;
using BaseApi.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BaseApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize] // Requiere autenticación
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsRepository _documentsRepository;

        public DocumentsController(IDocumentsRepository documentsRepository)
        {
            _documentsRepository = documentsRepository;
        }

        // GET api/documents?userId={userId}
        [HttpGet]
        public async Task<IActionResult> ObtenerDocumentos([FromQuery] int userId)
        {
            // Obtener el UserName del token JWT (claim email)
            var userName = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("No se pudo obtener el nombre de usuario.");
            }

            // Validar que el userId sea válido
            if (userId <= 0)
            {
                return BadRequest("El userId es requerido.");
            }

            // Obtener documentos pasando userId y userName
            var documentos = await _documentsRepository.ObtenerPorUsuario(userId, userName);
            return Ok(documentos);
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

        // GET api/documents/tipo/{tipoContenido}
        [HttpGet("tipo/{tipoContenido}")]
        public async Task<IActionResult> ObtenerPorTipoContenido(string tipoContenido)
        {
            var documentos = await _documentsRepository.ObtenerPorTipoContenido(tipoContenido);
            return Ok(documentos);
        }

        // GET api/documents/categoria/{categoria}]
        [HttpGet("categoria/{categoria}")]
        public async Task<IActionResult> ObtenerPorCategoria(string categoria)
        {
            var documentos = await _documentsRepository.ObtenerPorCategoria(categoria);
            return Ok(documentos);
        }
    }
}