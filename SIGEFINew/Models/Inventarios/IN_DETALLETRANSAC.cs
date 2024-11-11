using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_DETALLETRANSAC
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ID_BODEGA { get; set; }

        [Key]
        [Column(Order = 3)]
        [Display(Name = "N°")]
        public int NUM_TRANSACCION { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(1)]
        public string TIPO_TRAN { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(2)]
        public string TIPO_INGRESO { get; set; }

        [Key]
        [Column(Order = 6)]
        public int NUM_FILA { get; set; }

        public string COD_ITEM { get; set; }

        public decimal CANTIDAD { get; set; }

        public decimal COSTO_UNIT { get; set; }

        public decimal SUBTOTAL { get; set; }

        public int PORC_DESC { get; set; }

        public decimal VALOR_DESC { get; set; }

        public int PORC_IVA { get; set; }

        public decimal TOTAL_IVA { get; set; }

        public decimal TOTAL_GENERAL { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime FECHA_CADUCA { get; set; }

        public virtual IN_TRANSACCIONES IN_TRANSACCIONES { get; set; }
        public virtual IN_ITEMS IN_ITEMS { get; set; }
}
}