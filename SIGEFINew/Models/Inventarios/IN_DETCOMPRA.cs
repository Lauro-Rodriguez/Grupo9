using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_DETCOMPRA
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
        public int NUM_COMPRA { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string COD_ITEM { get; set; }
        
        [Key]
        [Column(Order = 5)]
        public int NUM_FILA { get; set; }

        //[Required]
        //[StringLength(10)]
        //public string CAT_CODIGO { get; set; }

        [Required]
        public bool APLICA_IVA { get; set; }

        [Required]
        public int PORC_IVA { get; set; }

        [Required]
        public decimal CANTIDAD { get; set; }

        [Required]
        public decimal COSTO_UNIT { get; set; }

        [Required]
        public decimal SUBTOTAL { get; set; }

        [Required]
        public decimal COSTO_TOTAL { get; set; }

        [Required]
        public decimal VAL_IVA { get; set; }

        public virtual IN_COMPRAS IN_COMPRAS { get; set; }
        public virtual IN_PRODUCTOS IN_PRODUCTOS { get; set; }
    }
}