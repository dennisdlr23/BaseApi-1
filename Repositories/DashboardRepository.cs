using GestionDocumental.WebApi.Infraestructure;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using BaseApi.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BaseApi.WebApi.Repositories
{
    public class DashboardRepository
    {
        private readonly GestionDocumentalDbContext _context;
        private readonly ILogger<DashboardRepository> _logger;

        public DashboardRepository(GestionDocumentalDbContext context, ILogger<DashboardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<DocumentosPorTipoContenid> GetDocumentosPorTipoContenido()
        {
            try
            {
                var result = _context.DocumentosPorTipoContenid
                    .FromSqlRaw("SELECT * FROM [dbo].[vw_DocumentosPorTipoContenido] ORDER BY CantidadDocumentos DESC")
                    .ToList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener frecuencia de fallas por máquina.");
                throw;
            }
        }
    }
}
