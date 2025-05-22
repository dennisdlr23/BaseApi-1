using System;

namespace BaseApi.WebApi.Models
{
    public class Documents
    {
        public int Id { get; set; }
        public string NombreOriginal { get; set; }
        public string NombreAlmacenado { get; set; }
        public string RutaArchivo { get; set; }
        public DateTime FechaSubida { get; set; }
        public string Categoria { get; set; }
        public string Etiquetas { get; set; }
        public string TipoContenido { get; set; }
        public int TamanoKB { get; set; }
        public int UserId {  get; set; }
    }
}
