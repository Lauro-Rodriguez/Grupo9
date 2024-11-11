using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEFINew.ViewModels
{
    public class AsignaDetCtasXPagar
    {
        public int NUM_TRANSAC { get; set; }

        public string COD_PROV { get; set; }

        public int NUM_FILA { get; set; }

        public string COD_TIPO_DOC { get; set; }

        public string TIPOSRI { get; set; }

        public DateTime FECHA { get; set; }

        public DateTime FECHA_DOC { get; set; }

        public string NUM_DOC { get; set; }

        public Decimal VALOR_DOC { get; set; }

        public int IVA_PORC { get; set; }

        public Decimal TOT_IVA { get; set; }

        public Decimal TOTAL { get; set; }

        public string CTA_X_PAG { get; set; }

        public string CTA_DEBITO { get; set; }

        public bool RFIVA { get; set; } //Si aplica Retención IVA y en la Fuente

        public bool AD { get; set; } //Si es para afectación directa

        public bool IG { get; set; } //Si es para IVA al Gasto

        public string AUTORIZACION { get; set; }

        public DateTime FECHACAD_DOC { get; set; }

        public string REPOSICION { get; set; }

        public int ICE { get; set; }

        //public string COD_SUSTENTO { get; set; }

        //public string FORMA_PAGO { get; set; }

        //public string TIPO_MOV { get; set; }
    }
}