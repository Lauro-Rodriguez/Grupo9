using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEFINew.ViewModels
{
    public class StockProductos
    {
        [Display(Name = "Código")]
        public string  COD_ITEM { get; set; }

        [Display(Name = "Producto")]
        public string NOM_ITEM { get; set; }

        [Display(Name = "C. Inicial")]
        public float CINI { get; set; }

        [Display(Name = "Ingresos")]
        public float ING { get; set; }

        [Display(Name = "Egresos")]
        public float EGR { get; set; }

        [Display(Name = "Stock")]
        public float STOCK { get; set; }
    }
}