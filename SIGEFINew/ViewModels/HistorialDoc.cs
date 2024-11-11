using System;
using System.ComponentModel.DataAnnotations;

namespace SIGEFINew.ViewModels
{
    public class HistorialDoc
    {
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FECHA_GENERA { get; set; }
        public string DETALLE { get; set; }
        public string ORG_NOMBRE { get; set; }
        public string FullName { get; set; }
        public string ORG_NOMENVIO { get; set; }
        public string APELLIDOSNOMBRES { get; set; }

    }
}