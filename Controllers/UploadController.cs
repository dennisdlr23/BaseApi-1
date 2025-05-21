using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BaseApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly string _localPath = @"C:\SAANAA";

        [HttpPost("")]
        public async Task<IActionResult> uploadImg(IFormFile signature)
        {
            if (signature == null || signature.Length == 0)
            {
                return BadRequest("No se ha subido ningún archivo.");
            }

            try
            {
                if (!Directory.Exists(_localPath))
                {
                    Directory.CreateDirectory(_localPath);
                }

                var fileName = Path.GetFileName(signature.FileName); // evita path traversal
                var destinationPath = Path.Combine(_localPath, fileName);

                using (var stream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                {
                    await signature.CopyToAsync(stream);
                }

                // Devolver una ruta relativa en lugar de una URL absoluta
                var relativePath = $"/Uploads/{fileName}";
                return Ok(relativePath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error al guardar la firma: {ex.Message}" });
            }
        }

        [HttpGet("Imagenes/{fileName}")]
        public IActionResult GetImg(string fileName)
        {
            try
            {
                var sanitizedFileName = Path.GetFileName(fileName); // evita path traversal
                var filePath = Path.Combine(_localPath, sanitizedFileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Archivo no encontrado.");
                }

                // Determinar el Content-Type según la extensión del archivo
                string contentType;
                var extension = Path.GetExtension(sanitizedFileName).ToLowerInvariant();
                switch (extension)
                {
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".jpeg":
                    case ".jpg":
                        contentType = "image/jpeg";
                        break;
                    case ".gif":
                        contentType = "image/gif";
                        break;
                    default:
                        contentType = "application/octet-stream"; // Tipo genérico si no se reconoce
                        break;
                }

                // Usar PhysicalFile para streaming eficiente
                return PhysicalFile(filePath, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error al recuperar la imagen: {ex.Message}" });
            }
        }
    }
}