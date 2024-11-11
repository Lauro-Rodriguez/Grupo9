using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_COMPRAS
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
        public int NUM_COMPRA { get; set; }

        [Display(Name = "N° Comp.")]
        public int NUM_COMPROMISO { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime FECHA_COMPRA { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Código")]
        public string COD_TIPO_DOC { get; set; }

        [Required]
        [Display(Name = "Proveedor")]
        [StringLength(20)]
        public string COD_PROV { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Detalle")]
        public string DETALLE { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "Tipo SRI")]
        public string TIPO_SRI { get; set; }

        [Required]
        [Display(Name = "Fecha Doc.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FECHA_DOC { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "N° Doc.")]
        public string NUM_DOC { get; set; }

        [Required]
        [StringLength(49)]
        [Display(Name = "Autorización")]
        public string AUTORIZACION { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Caduca")]
        public DateTime FECHACAD_DOC { get; set; }

        //int Est = 1;
        //public int ESTADO { get=>Est; set { Est = value; } } //1=INGRESADO, 2=APROBADO, 3=AUTORIZADO
        
        [StringLength(2)]
        public string COD_SUSTENTO { get; set; }

        [StringLength(2)]
        public string FORMA_PAGO { get; set; }

        public int? NUM_INV { get; set; } //Campo que controla el número de ingreso a bodega

        public int? NUM_AF { get; set; } //Campo que controla el número de ingreso a control de bienes

        public int? NUM_CXP { get; set; } //Campo que controla el número de transacción de la cuenta por pagar

        [Display(Name = "Unidad")]
        public int? ID_UNIDOPERATIVA { get; set; }

        [Required]
        [StringLength(15)]
        public string USER_CREA { get; set; }

        [Required]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(15)]
        public string USER_MODIF { get; set; }

        public DateTime FECHA_MODIF { get; set; }

        public virtual Proveedores Proveedores { get; set; }
        public virtual Periodos Periodos { get; set; }
        public virtual ICollection<IN_DETCOMPRA> IN_DETCOMPRA { get; set; }
    }
}