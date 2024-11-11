using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEFINew.Models.Inventarios
{
    public class IN_TIPOSTRAN
    {
        [Key]
        [Column(Order = 0)]
        public int ID_INSTITUCION { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PERI_CODIGO { get; set; }

        [Key]
        [Required]
        [Column(Order = 2)]
        public int COD_TIPOTRAN { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "Tipo")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        public string TIPO_MOV { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Descripción")]
        public string DESCRIP_TIPOTRAN { get; set; }

        [Required]
        [Display(Name = "Enivar a")]
        [Range(1, 2)]
        public int ENV_CO_CC { get; set; }

        [Required]
        [Display(Name = "Con I.V.A.")]
        public bool CON_IVA { get; set; }

        [Required]
        [Display(Name = "Con Donante")]
        public bool CON_DONANTE { get; set; }

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