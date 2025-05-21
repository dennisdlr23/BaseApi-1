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
        private readonly string _localPath = @"C:\SAANA\Img\";

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

                var relativeUrl = $"http://<IIS_IP>:<PUERTO>/firmas/{fileName}"; // Reemplaza con tus valores reales
                return Ok(relativeUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la firma: {ex.Message}");
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

                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                return File(fileBytes, "image/png");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al recuperar la imagen: {ex.Message}");
            }
        }
    }
}
