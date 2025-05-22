using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.WebApi.Models
{
    public class DocumentosPorUsuario
    {
        public string UserName { get; set; }
        public int CantidadDocumentos { get; set; }
        public int TamanoTotalKB { get; set; }

        public class Map
        {
            public Map(EntityTypeBuilder<DocumentosPorUsuario> builder)
            {
                builder.HasKey(x => x.UserName);
                builder.Property(x => x.CantidadDocumentos).HasColumnName("CantidadDocumentos");
                builder.Property(x => x.TamanoTotalKB).HasColumnName("TamanoTotalKB");
                builder.ToTable("[dbo].[vw_DocumentosPorUsuario]");
            }
        }
    }
}
