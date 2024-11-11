using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_TRANSACCIONES
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
        public string TIPO_TRAN { get; set; } //Tipo de transacción I,E

        [Key]
        [Column(Order = 5)]
        [StringLength(2)]
        public string TIPO_INGRESO { get; set; } //EV=EGRESOS VARIOS,CP=COMPRA,TR=TRANSFERENCIA,IV=INGRESOS VARIOS

        [Display(Name = "N° Compra")]
        public int NUM_COMPRA { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime FECHA_TRANSAC { get; set; }

        [Display(Name = "Tipo Tran.")]
        [StringLength(10)]
        public string TIPO_MOV { get; set; }

        [StringLength(2)]
        public string TIPO_PAGO { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Tipo Doc.")]
        public string COD_TIPO_DOC { get; set; }

        [Required]
        [Display(Name = "Proveedor")]
        [StringLength(20)]
        public string COD_PROV { get; set; }

        [Display(Name = "Unidad")]
        public int? ID_UNIDOPERATIVA { get; set; }

        [Required]
        [Display(Name = "Autorizado por")]
        [StringLength(20)]
        public string AUTORIZADO_POR { get; set; }

        [Range(0,1)]
        [Display(Name = "Uso")]
        public TIPOUSO? USO { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Detalle")]
        public string DETALLE { get; set; }

        [Display(Name = "Objeto Receptor")]
        public int? ID_OBJETO { get; set; }

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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Caduca")]
        public DateTime? FECHACAD_DOC { get; set; }

        [StringLength(49)]
        [Display(Name = "Autorización")]
        public string AUTORIZACION { get; set; }
        //int Est = 1;
        //public int ESTADO { get=>Est; set { Est = value; } } //1=INGRESADO, 2=APROBADO, 3=AUTORIZADO

        public int NUM_CXP { get; set; } //Campo que controla el número de transacción de la cuenta por pagar

        public bool ANULADO { get; set; }

        public bool CERRADO { get; set; }

        [Required]
        [StringLength(15)]
        public string USER_CREA { get; set; }

        [Required]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(15)]
        public string USER_MODIF { get; set; }

        public DateTime? FECHA_MODIF { get; set; }

        public virtual Proveedores Proveedores { get; set; }
        public virtual Periodos Periodos { get; set; }
        public virtual ICollection<IN_DETALLETRANSAC> IN_DETALLETRANSAC { get; set; }
    }
}