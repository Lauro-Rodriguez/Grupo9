using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public enum TipoBodega
    {
        CONSUMO, VENTA, INVERSION, PRODUCCION, VARIOS
    }
    public class IN_BODEGAS
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Column(Order = 2)]
        [Display(Name = "Código")]
        public int ID_BODEGA { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Nombre")]
        public string NOM_BODEGA { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public TipoBodega? TIPO_BODEGA { get; set; }

        //[StringLength(50)]
        //[Display(Name = "Responsable")]
        //public string RESP_BODEGA { get; set; }

        [StringLength(250)]
        [Display(Name = "Dirección")]
        public string DIR_BODEGA { get; set; }

        [StringLength(10)]
        [Display(Name = "Teléfono")]
        public string TELF_BODEGA { get; set; }

        [Required]
        [Display(Name = "Ingreso a Bodega")]
        public float NUM_ING_BODEGA { get; set; }

        [Required]
        [Display(Name = "Egreso de Bodega")]
        public float NUM_EGR_BODEGA { get; set; }

        [Required]
        [Display(Name = "Devolución de Ingreso")]
        public float NUM_DEV_INGRE { get; set; }

        [Required]
        [Display(Name = "Devolución de Egreso")]
        public float NUM_DEV_EGRE { get; set; }

        [Required]
        [Display(Name = "Transferencia Ingreso")]
        public float NUM_TRAN_INGRE { get; set; }

        [Required]
        [Display(Name = "Transferencia Egreso")]
        public float NUM_TRAN_EGRE { get; set; }

        [Required]
        [Display(Name = "Sin Stock Ingreso")]
        public float NUM_SIN_STOCKI { get; set; }

        [Required]
        [Display(Name = "Sin Stock Egreso")]
        public float NUM_SIN_STOCKE { get; set; }

        [Required]
        [Display(Name = "Orden de Compra")]
        public float NUM_ORD_COMPRA { get; set; }

        [Required]
        [Display(Name = "Nota de Entrega")]
        public float NUM_NTA_ENT { get; set; }

        [Required]
        [Display(Name = "Guía de Remisión")]
        public float NUM_GUIA_REMI { get; set; }

        [Required]
        [Display(Name = "Orden de Producción")]
        public float NUM_ORDEN_PROD { get; set; }

        [Required]
        [Display(Name = "Pedido de Materiales")]
        public float NUM_PED_MAT { get; set; }

        [Required]
        public bool NUMAUT_INGBOD { get; set; }

        [Required]
        public bool NUMAUT_EGRBOD { get; set; }

        [Required]
        public bool NUMAUT_ORDCOMP { get; set; }

        [Required]
        public bool NUMAUT_NOTENT { get; set; }

        [Required]
        public bool INCRE1_INGBOD { get; set; }

        [Required]
        public bool INCRE1_EGRBOD { get; set; }

        [Required]
        public bool INCRE1_ORDCOMP { get; set; }

        [Required]
        public bool INCRE1_NOTENT { get; set; }

        [Required]
        [Display(Name = "Código Inicial del Producto")]
        public float COD_INI_PROD { get; set; }

        [Required]
        [Display(Name = "Codificación Automática del Producto")]
        public bool COD_AUTOMAT { get; set; }

        public DateTime? FECHA_ULT_REG { get; set; }

        [Display(Name = "Usa Código de Barras")]
        public bool USA_CODBAR { get; set; }

        [Display(Name = "Añadir IVA al Costo del Producto")]
        public bool IVA_COSTOPROD { get; set; }

        [StringLength(50)]
        [Required]
        public string USER_CREA { get; set; }

        [Required]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(50)]
        public string USER_MODIF { get; set; }

        public DateTime? FECHA_MODIF { get; set; }
        public virtual Periodos Periodos { get; set; }
        public virtual ICollection<Models.Inventarios.IN_ITEMS> IN_ITEMS { get; set; }
        public virtual ICollection<Inventarios.IN_CUENTASCONT> IN_CUENTASCONT { get; set; }
        public virtual ICollection<Inventarios.IN_USERSXBOD> IN_USERSXBOD { get; set; }
    }
}