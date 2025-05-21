using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApi.WebApi.Models
{
    public class DocumentosPorTipoContenid
    {
        public string TipoContenido { get; set; }
        public int CantidadDocumentos { get; set; }

        public class Map
        {
            public Map(EntityTypeBuilder<DocumentosPorTipoContenid> builder)
            {
                builder.HasKey(x => x.TipoContenido);
                builder.Property(x => x.CantidadDocumentos).HasColumnName("CantidadDocumentos");
               
                builder.ToTable("[dbo].[vw_DocumentosPorTipoContenido]");
            }
        }
    }
}
