using SIGEFINew.Models.POA;
using System.ComponentModel.DataAnnotations;

namespace SIGEFINew.ViewModels
{
    public class POA_DetalProys
    {
        public int COD_PLAN { get; set; }

        [Display(Name = "Dirección")]
        public int ORG_CODIGO { get; set; }
        public string COD_PLANOBJTVO { get; set; }
        public string APELLIDOSNOMBRES { get; set; }

        [Display(Name = "Descripción")]
        public string DESC_OBJETIVO { get; set; }

        [Display(Name = "Observación")]
        public string OBS_OBJETIVO { get; set; }

        [Display(Name = "Estado")]
        public EstadoTP? ESTADO { get; set; }

        [Display(Name = "G.A.PR")]
        public bool ES_GAPR { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal TOTAL { get; set; }

        public string ESTADO_PY { get; set; }
    }
}