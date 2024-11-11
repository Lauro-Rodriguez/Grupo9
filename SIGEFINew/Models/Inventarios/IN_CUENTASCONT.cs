using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_CUENTASCONT
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
        public int ID_CLASE { get; set; }

        [Key]
        [Column(Order = 4)]
        public int ID_TIPO { get; set; }

        [StringLength(50)]
        [Display(Name = "Inventario")]
        public string CC_INVENT { get; set; }

        [StringLength(50)]
        [Display(Name = "Cuenta por Pagar")]
        public string CC_CXP_CONSUMO { get; set; }

        [StringLength(50)]
        [Display(Name = "Gasto")]
        public string CC_GASTO_CONSUMO { get; set; }

        [StringLength(50)]
        [Display(Name = "Donación")]
        public string CC_DONACION_CONSUMO { get; set; }

        [StringLength(50)]
        [Display(Name = "Acum. Obras")]
        public string CC_ACUMOBRAS_I { get; set; }

        [StringLength(50)]
        [Display(Name = "Acum. Programas")]
        public string CC_ACUMPROGRAM_I { get; set; }

        [StringLength(50)]
        [Display(Name = "Acum. Programas")]
        public string CC_CXP_I { get; set; }

        [StringLength(50)]
        [Display(Name = "Gasto")]
        public string CC_GASTO_I { get; set; }

        [StringLength(50)]
        [Required]
        public string USER_CREA { get; set; }

        [Required]
        public DateTime FECHA_CREA { get; set; }

        [StringLength(50)]
        public string USER_MODIF { get; set; }

        public DateTime FECHA_MODIF { get; set; }

        public virtual Instituciones INSITUCIONES { get; set; }
        public virtual Periodos Periodos { get; set; }
        public virtual IN_BODEGAS IN_BODEGAS { get; set; }
        //public virtual IN_CLASES IN_CLASES { get; set; }
        //public virtual IN_TIPOS IN_TIPOS { get; set; }

    }
}