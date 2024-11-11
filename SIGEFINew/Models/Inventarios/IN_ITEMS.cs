using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_ITEMS
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
        [Required]
        [StringLength(50)]
        [Column(Order = 3)]
        [Display(Name = "Código")]
        public string COD_ITEM { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public int ID_CLASE { get; set; }

        [Required]
        [Display(Name = "Familia")]
        public int ID_TIPO { get; set; }

        [Required]
        [Display(Name = "Sub Familia")]
        public int ID_SUBTIPO { get; set; }

        [Required]
        [StringLength(10)]
        public string COD_CAGEN { get; set; }

        [Required]
        public int COD_PRESEN { get; set; }

        [Required]
        [Display(Name = "Catálogo")]
        [StringLength(10)]
        public string CAT_CODIGO { get; set; }

        [StringLength(90)]
        [Required]
        [Display(Name = "Descripción")]
        public string NOM_ITEM { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Marca")]
        public string MARCA_ITEM { get; set; }

        [StringLength(100)]
        [Display(Name = "Aplicación")]
        public string APLIC_ITEM { get; set; }

        [Required]
        [Display(Name = "Tipo Costeo")]
        public int TIPO_COSTEO { get; set; }

        [StringLength(1)]
        [Required]
        [Display(Name = "Costo del Producto")]
        public string COSTO_PROD { get; set; }

        [Display(Name = "Aplica IVA")]
        public bool APLICA_IVA { get; set; }

        [Display(Name = "Perecible")]
        public bool PROD_PERE { get; set; }

        [Display(Name = "Prod. Desc.")] //Producto Descontinuado
        public bool PROD_DESC { get; set; }

        [Display(Name = "Producto con N° de Serie")]
        public bool PROD_NUM_SERIE { get; set; }

        [Display(Name = "Producto con N° de Garantía")]
        public bool PROD_NUM_GAR { get; set; }

        [StringLength(50)]
        [Display(Name = "Sección")]
        public string SECCION { get; set; }

        [StringLength(50)]
        [Display(Name = "Percha")]
        public string PERCHA { get; set; }

        [StringLength(50)]
        [Display(Name = "Fila")]
        public string FILA { get; set; }

        [StringLength(50)]
        [Display(Name = "Columna")]
        public string COLUMNA { get; set; }

        [Required]
        [Display(Name = "Stock Mímimo")]
        public int STOCK_MIN { get; set; }

        [Required]
        [Display(Name = "Stock Máximo")]
        public int STOCK_MAX { get; set; }

        [Display(Name = "Pto. Reorden")] //Punto de Reorden
        public int PUNTO_REORDEN { get; set; }

        public int PRECIO1 { get; set; }

        public int PRECIO2 { get; set; }

        public DateTime FECHA_ULT_COMPRA { get; set; }

        [StringLength(20)]
        [Display(Name = "Consignante")]
        public string COD_CONSIG { get; set; } //Código del Proveedor para el Consignante

        [Display(Name = "Cta. Orden DB")]
        [StringLength(50)]
        public string CODIGO_CGDB { get; set; } //Cuenta Orden Débito

        [Display(Name = "Cta. Orden CR")]
        [StringLength(50)]
        public string CODIGO_CGCR { get; set; } //Cuenta Orden Crédito

        [Display(Name = "Línea")]
        public int COD_LINEA { get; set; }

        [Display(Name = "Sublínea")]
        public int COD_SUBLINEA { get; set; }

        [StringLength(50)]
        [Required]
        public string USER_CREA { get; set; }

        [Required]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(50)]
        public string USER_MODIF { get; set; }

        public DateTime FECHA_MODIF { get; set; }

        public virtual IN_BODEGAS IN_BODEGAS { get; set; }
        public virtual ICollection<IN_CARGA_INI> IN_CARGA_INI { get; set; }
        public virtual IN_CARACGEN IN_CARACGEN { get; set; }
        public virtual IN_PRESENTA IN_PRESENTA { get; set; }
        public virtual IN_CLASES IN_CLASES { get; set; }
        public virtual IN_TIPOS IN_TIPOS { get; set; }
        public virtual IN_SUBTIPOS IN_SUBTIPOS { get; set; }
        public virtual IN_PRODUCTOS IN_PRODUCTOS { get; set; }
        public virtual ICollection<SIGEFINew.Models.Facturacion.FC_DETALLEFACTURAS> FC_DETALLEFACTURAS { get; set; }
        public virtual ICollection<IN_DETALLETRANSAC> IN_DETALLETRANSAC { get; set; }
        //public virtual ICollection<IN_DETCOMPRA> IN_DETCOMPRA { get; set; }
    }
}