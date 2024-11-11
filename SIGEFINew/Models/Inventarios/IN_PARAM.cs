using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_PARAM
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        public int NUM_FILA { get; set; }

        [Required]
        [Display(Name = "Porcentaje IVA")]
        public int PORC_IVA { get; set; }

        [Required]
        [Display(Name = "Amplitud de descripcion")]
        public int AMPLI_DESC { get; set; }

        [Required]
        [Display(Name = "Unidades Operativas")]
        public bool UNID_OPER { get; set; }

        [Required]
        [Display(Name = "Ordenes de producción")]
        public bool ORD_PROD { get; set; }

        [Required]
        [Display(Name = "Num. Automática de producción")]
        public bool NAUT_PROD { get; set; }

        [Required]
        [Display(Name = "Contabilizar por Bodega")]
        public bool CONT_BOD { get; set; }

        public int CONT_TSP { get; set; }

        //Numeraciones independientes
        [Required]
        [Display(Name = "Devoluciones")]
        public bool NSEP_DEVO { get; set; }

        [Required]
        [Display(Name = "Transferencias")]
        public bool NSEP_TRAN { get; set; }

        [Required]
        [Display(Name = "Sin Stock")]
        public bool NSEP_STOCK { get; set; }
        //----------------------------------------

        //Alertas Stock
        [Display(Name = "Ingreso Manual Alertas Stock")]
        public bool ING_MANUAL_ALERTSTOCK { get; set; }

        [Display(Name = "Alertas Stock Min-Máx")]
        public bool ALERTSTOCK_MINMAX { get; set; }
        //-----------------------------------------

        //Cuentas contables
        [Display(Name = "Cuenta Inversión")]
        public int CTA_INVERSION { get; set; }

        [Required]
        [Display(Name = "Dígitos Código del Producto")]
        public int COD_PROD { get; set; }

        [Required]
        [Display(Name = "Usar Código de Barras")]
        public bool ORD_X_COD { get; set; }

        [Required]
        [Display(Name = "Recargo con IVA")]
        public bool REC_CON_IVA { get; set; }

        //Digitos decimales
        [Required]
        [Display(Name = "En Cantidad")]
        public int UTIL_DEC_CANTI { get; set; }

        [Required]
        [Display(Name = "En costo unitario")]
        public int UTIL_DEC_CUNI { get; set; }

        [Required]
        [Display(Name = "En costo total")]
        public int UTIL_DEC_CTOT { get; set; }
        //---------------------

        //En orden de compra
        [Display(Name = "Permitir Q. Ingresada>Q. Ordenada")]
        public bool PERM_QING_M_QORD { get; set; }

        [Display(Name = "Cambiar Precio Unitario")]
        public bool CAM_P_UNITARIO { get; set; }

        [Display(Name = "Notificar Transacciones sin Contabilizar")]
        public bool NOT_TRANSINCONT { get; set; }

        [StringLength(50)]
        [Required]
        public string USER_CREA { get; set; }

        [Required]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(50)]
        public string USER_MODIF { get; set; }

        public DateTime FECHA_MODIF { get; set; }

        public virtual Periodos Periodos { get; set; }
    }
}