using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEFINew.ViewModels
{
    public class ASIGNA_CARGAINI
    {
        public int ID_INSTITUCION { get; set; }
        public int PERI_CODIGO { get; set; }
        public int ID_BODEGA { get; set; }
        public string COD_ITEM { get; set; }
        public string NOM_ITEM { get; set; }
        public float CANTIDAD { get; set; }
        public float COSTO { get; set; }
        public float VALOR_TOTAL { get; set; }
    }
}